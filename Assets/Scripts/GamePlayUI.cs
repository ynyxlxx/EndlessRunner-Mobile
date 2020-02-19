using UnityEngine.UI;
using UnityEngine;

public class GamePlayUI : MonoBehaviour {

    public Text scoreText;
    public RectTransform energyBar;
    public Button pauseButton;

    private Player player;

    private void Start() {
        scoreText.text = "0";
        player = FindObjectOfType<Player>();
    }

    private void Update() {
        UpdateTheData();
    }

    private void UpdateTheData(){
        scoreText.text = ScoreKeeper.score.ToString();

        float energyPercent = 0;
        if (player != null){
            energyPercent = player.currentEnergy / player.startEnergy;
        }
        energyBar.localScale = new Vector3(energyPercent, 1, 1);
    }

}