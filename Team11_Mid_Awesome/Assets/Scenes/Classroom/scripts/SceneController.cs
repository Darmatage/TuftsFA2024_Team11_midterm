using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    // Array of scene names
    public string[] scenes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ProgressBarManager.progressAmount >= 1){
            SceneManager.LoadScene("WinScreen");
        }
    }

    // Method to quit the game
    public void QuitGame(){
        Application.Quit();
    }

    // Method to load a random scene from the array
    public void LoadRandomScene(){
        if (scenes.Length > 0)
        {
            // Get a random index from the array
            int randomIndex = Random.Range(0, scenes.Length);
            // Load the scene at the random index
            SceneManager.LoadScene(scenes[randomIndex]);
        }
        else
        {
            Debug.LogError("No scenes available to load.");
        }
    }
}
