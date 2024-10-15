using UnityEngine;
using UnityEngine.UI;

public class ProgressBarManager : MonoBehaviour
{
    public static ProgressBarManager instance;
    public Slider progressBar; // Or Image if you prefer using an Image UI

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // This ensures the object is not destroyed when changing scenes
        }
        else
        {
            Destroy(gameObject); // If another instance exists, destroy this one
        }
    }

    // Call this method to update the progress bar
    public void UpdateProgress(float progress)
    {
        if (progressBar != null)
        {
            progressBar.value = progress; // Set the value between 0 and 1 for sliders
            // For an Image: progressBar.fillAmount = progress;
        }
    }
}
