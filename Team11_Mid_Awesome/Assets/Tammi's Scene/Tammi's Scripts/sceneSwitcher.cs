using UnityEngine;
using UnityEngine.SceneManagement;  // Required for scene management

public class SceneSwitcher : MonoBehaviour
{
    // Function to switch to the specified scene
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Optional: function to quit the application (for desktop builds)
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}