using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float totalTime = 20f; // Total time in seconds
    private float timeRemaining;
    public GameObject playerDeadUI;
    public GameObject levelCompleteUI;
    public Text timerText; // UI Text element to display the time
    public GameObject timeUpPanel; // "Time's Up" panel (UI element)

    public bool isGameOver = false; // Flag for game over state

    void Start()
    {
        timeRemaining = totalTime;
        UpdateTimerText(); // Show the initial time on the screen
        timeUpPanel.SetActive(false); // Hide the "Time's Up" panel at the start
    }

    void Update()
    {
        // Update the timer only if the game is not over
        if (!isGameOver)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime; // Decrease time by frame time
                UpdateTimerText(); // Update timer display
            }
            else
            {
                TimeUp(); // Trigger "Time's Up" logic when the timer reaches 0
            }
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // Display time in MM:SS
    }

    void TimeUp()
    {
        isGameOver = true; // Stop the timer
        timeRemaining = 0;
        UpdateTimerText(); // Show "00:00"
        if (playerDeadUI != null)
        {
            playerDeadUI.SetActive(false);
        }
        if (levelCompleteUI != null)
        {
            levelCompleteUI.SetActive(false);
        }
        timeUpPanel.SetActive(true); // Show "Time's Up" panel
    }
}
