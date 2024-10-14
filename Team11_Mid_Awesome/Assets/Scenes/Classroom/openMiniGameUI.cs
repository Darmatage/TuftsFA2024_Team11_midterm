using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor; // Add this namespace to use SceneAsset

public class openMiniGameUI : MonoBehaviour
{
    public SceneAsset minigame;

    // Update is called once per frame
    void Update()
    {
        // We can also check for input here if needed.
    }

    // Function to test if in the hitbox or not
    public void loadMiniGame()
    {
            if (minigame != null)
            {
                string sceneName = minigame.name;
                // Load the desired scene
                SceneManager.LoadScene(sceneName); // Use the string name to load the scene
            }
            else
            {
                Debug.LogWarning("SceneAsset is not assigned!");
            }
    }
}
