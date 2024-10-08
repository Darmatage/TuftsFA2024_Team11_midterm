using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour {
     public GameObject textGameObject;
     private int score;
     public int scoreWin = 5;
     public bool isEnd = true;   // allows specific settings for end scenes

     void Start () {
          score =0;
          UpdateScore ();

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
           if (score >= scoreWin){
              SceneManager.LoadScene("yssies_scene"); // uses level name
             //SceneManager.LoadScene(1); // uses build index
             //SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
                                                               // restart same level
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