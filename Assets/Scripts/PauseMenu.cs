using UnityEngine;
using UnityEngine.UI;

/*
 * PauseMenu.cs
 * Author: Lauren Thoman
 * Date: June 22, 2025
 *
 * Handles pausing and resuming the game using the Escape key and Resume button.
 */

public class PauseMenu : MonoBehaviour
{
    // Reference to pause panel UI
    public GameObject pausePanel;

    // Tracks whether the game is currently paused
    private bool isPaused = false;

    void Update()
    {
        // Check if Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle pause state
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);       // Shows pause panel
        Time.timeScale = 0f;              // Stops time
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);      // Hides pause panel
        Time.timeScale = 1f;              // Resumes time
        isPaused = false;
    }
}