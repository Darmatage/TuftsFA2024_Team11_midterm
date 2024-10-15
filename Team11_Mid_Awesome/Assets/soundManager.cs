using UnityEngine;
using UnityEngine.SceneManagement;  // Required for scene management

public class soundManager : MonoBehaviour
{
    // Reference to the AudioSource
    public AudioSource dialogueAudio;

    private void Start()
    {
        // Optionally, you can automatically play the audio when the scene starts
        PlayDialogueAudio();
    }

    public void PlayDialogueAudio()
    {
        if (dialogueAudio != null)
        {
            dialogueAudio.Play();
        }
    }

    public void StopDialogueAudio()
    {
        if (dialogueAudio != null)
        {
            dialogueAudio.Stop();
        }
    }

    public void ContinueToNextScene(string sceneName)
    {
        // Stop audio before changing scenes
        StopDialogueAudio();

        // Switch to the specified scene
        SceneManager.LoadScene(sceneName);
    }
}
