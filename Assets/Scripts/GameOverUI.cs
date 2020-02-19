using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour {

    public void BackToMainMenu(){
        SceneManager.LoadScene("MenuScene");
    }

    public void RestartGame(){
        SceneManager.LoadScene("GameScene");
    }
}