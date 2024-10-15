using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour {
     public GameObject textGameObject;
     private int score;
     public int scoreWin = 5;
     public bool isEnd = true; 
     private GameController gameController;  // allows specific settings for end scenes

     void Start () {
        GameObject controllerObject = GameObject.FindWithTag("GameController");
        if (controllerObject != null) {
            gameController = controllerObject.GetComponent<GameController>();
        }
        score = 0;
        UpdateScore();
          
        if (isEnd){
               Cursor.lockState = CursorLockMode.None;
               Cursor.visible = true;
          }
     }

    void Update () {     // always include a way to quit the game:
           if (Input.GetKey("escape")) {
                 QuitGame();
          }
    }

    public void AddScore (int newScoreValue) {
          score += newScoreValue;
          UpdateScore ();
          WinorLose();
     }
    public void WinorLose(){
        if (score >= scoreWin){
            SceneManager.LoadScene("ClassroomBook");
        } else {
            QuitGame();
        }
    }
    void UpdateScore () {
        Text scoreTextB = textGameObject.GetComponent<Text>();
        scoreTextB.text = "Score: " + score;
     }

    public void QuitGame() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}