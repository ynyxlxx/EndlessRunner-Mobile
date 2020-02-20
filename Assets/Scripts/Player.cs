using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public ParticleSystem deathParticles;
    public float startEnergy;
    public float energyDecreasement;
    public float slowMotionEnergyConsumeFactor;
    public float currentEnergy{ get; private set; }

    public static event System.Action OnDeath;

    private TouchController touchController;
    private GameManager gameManager;
    private PlayerController playerController;

    private bool isGameOver;

    private void Start() {
        isGameOver = false;
        currentEnergy = startEnergy;
        touchController = GetComponent<TouchController>();
        playerController = GetComponent<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();

        Coin.OnPickUpCoin += AddEnergy;
        
        OnDeath += ShowDeathParticle;
    }

    private void Update() {
        DecreaseEnergyByTime();

        if (OnDeath != null && isGameOver){
            SaveHighScore();
            AudioManager.Instance.Play("PlayerDeath");
            OnDeath();
            OnDeath -= ShowDeathParticle;
            Destroy(gameObject);
        }

    }

    private void ShowDeathParticle(){
        ParticleSystem particles = Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(particles, 2f);
    }

    private void SaveHighScore(){
        if (ScoreKeeper.score > PlayerPrefs.GetInt("HighScore", 0)){
            PlayerPrefs.SetInt("HighScore", ScoreKeeper.score);
        }
    }

    private void DecreaseEnergyByTime(){
        if (playerController.slowMotion == true){
            currentEnergy -= slowMotionEnergyConsumeFactor * energyDecreasement * Time.unscaledDeltaTime;
        }else{
            currentEnergy -= energyDecreasement * Time.unscaledDeltaTime;
        }

        if (currentEnergy <= 0){
            isGameOver = true;
        }
    }

    private void AddEnergy(){
        currentEnergy += 10;
        if (currentEnergy > 100){
            currentEnergy = 100f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Obstacle")){
            isGameOver = true;
        }
    }
}