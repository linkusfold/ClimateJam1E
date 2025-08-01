/*
 * PauseMenu.cs
 * Author: Lauren Thoman
 * Date: June 22, 2025 (Updated Aug 1, 2025)
 *
 * Handles pausing and resuming the game using the Escape key and Resume button.
 * Also provides access to Settings, Info, and Quit from the pause menu.
 * Disables gameplay scripts on pause so nothing behind the UI responds.
 */

using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject pausePanel;
    public GameObject settingsPanel;
    public GameObject infoPanel;    // <-- New Info panel reference

    [Header("Gameplay Scripts to Disable")]
    public MonoBehaviour[] gameplayScripts;

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
        // Show pause UI
        pausePanel.SetActive(true);
        // Stop time
        Time.timeScale = 0f;
        isPaused = true;

        // Disable all gameplay scripts
        foreach (var script in gameplayScripts)
            script.enabled = false;
    }

    public void ResumeGame()
    {
        // Hide all panels
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        infoPanel.SetActive(false);
        // Resume time
        Time.timeScale = 1f;
        isPaused = false;

        // Re-enable gameplay scripts
        foreach (var script in gameplayScripts)
            script.enabled = true;
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

    // Called by Pause Menu Info button
    public void OpenInfo()
    {
        infoPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    // Called by Info panel Close button
    public void CloseInfo()
    {
        infoPanel.SetActive(false);
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
