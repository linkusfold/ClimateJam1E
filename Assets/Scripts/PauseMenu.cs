using UnityEngine;
using UnityEngine.UI;

/*
 * PauseMenu.cs
 * Author: Lauren Thoman
 * Date: June 22, 2025 (Updated July 8, 2025)
 *
 * Handles pausing and resuming the game using the Escape key and Resume button.
 * Also provides access to Settings and Quit from the pause menu.
 */

public class PauseMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject pausePanel;
    public GameObject settingsPanel;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false); // Make sure settings closes too
        Time.timeScale = 1f;
        isPaused = false;
    }

    // Called by Pause Menu Settings button
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    // Called by Settings panel Back button
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    // Called by Pause Menu Quit button
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
