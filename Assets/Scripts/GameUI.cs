using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
    
    public GameObject gamePlayUI;
    public GameObject gameOverUI;

    public Text scoreText;

    private void Start () {
        gamePlayUI.SetActive(true);
        gameOverUI.SetActive(false);

        Player.OnDeath += ShowGameOverUI;
    }

    private void ShowGameOverUI () {
        gamePlayUI.SetActive (false);
        gameOverUI.SetActive (true);

        scoreText.text = ScoreKeeper.score.ToString();

        Player.OnDeath -= ShowGameOverUI;
    }
}