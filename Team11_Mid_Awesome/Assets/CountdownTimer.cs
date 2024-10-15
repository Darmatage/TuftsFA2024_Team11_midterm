using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float timeRemaining = 45f; // 45 seconds
    public Text timerText; // Reference to the UI Text element
    public string mainMenuSceneName = "Classroom Scence"; // Name of the Main Menu scene

    private bool _timerIsRunning = false;

    void Start()
    {
        _timerIsRunning = true;
    }

    void Update()
    {
        if (_timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                // Decrease the time remaining
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);
            }
            else
            {
                // Time has run out
                timeRemaining = 0;
                _timerIsRunning = false;
                HandleTimerEnd();
            }
        }
    }

    private void UpdateTimerDisplay(float timeToDisplay)
    {
        timeToDisplay = Mathf.Clamp(timeToDisplay, 0, Mathf.Infinity); // Prevents negative time

        // Convert time to minutes and seconds format
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // Update the timer UI Text
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void HandleTimerEnd()
    {
        // Load the main menu scene
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void StopTimer()
    {
        _timerIsRunning = false;
    }

}
