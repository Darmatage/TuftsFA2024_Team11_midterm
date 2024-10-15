using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTriggerScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Load the Classroom scene when the player reaches the exit
            SceneManager.LoadScene("yssies_scene");
        }
    }
}
