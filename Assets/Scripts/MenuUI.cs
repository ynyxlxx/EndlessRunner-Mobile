using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour {

    public Button startButton;
    public Button quitButton;
    public Text scoreText;

    private void Start() {
        scoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void StartGame(){
        SceneManager.LoadScene("GameScene");
    }

}