using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor; // Add this namespace to use SceneAsset

public class OpenMiniGame : MonoBehaviour
{
    // Variable to hold the scene asset
    public SceneAsset minigame;

    // Update is called once per frame
    void Update()
    {
        // We can also check for input here if needed.
    }

    // Function to test if in the hitbox or not
    void OnTriggerStay(Collider other)
    {
        // Check if the player presses the "E" key
        if (Input.GetKeyDown(KeyCode.E))
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
}
