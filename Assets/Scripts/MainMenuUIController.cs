/*
 * MainMenuUIController.cs
 * Author: Lauren Thoman
 * Date: July 7, 2025
 *
 * Handles the functionality of the Main Menu UI, including:
 * - Starting the game
 * - Hiding the main menu panel when the game begins
 * - Showing/hiding the Settings panel
 * - Showing/hiding the Credits & Info panel
 * - Quitting the game
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject creditsPanel;

    [Header("Scene Settings")]
    public string gameplaySceneName = "GameScene"; // Leave empty if game is in same scene

    [Header("Gameplay Root (for same scene setup)")]
    public GameObject gameplayManager; // Optional: assign gameplay root to activate here

    // Called by Start Game button
    public void StartGame()
    {
        // If using scene-based transition
        if (!string.IsNullOrEmpty(gameplaySceneName))
        {
            SceneManager.LoadScene(gameplaySceneName);
        }
        else
        {
            // If game starts in the same scene
            mainMenuPanel.SetActive(false);

            if (gameplayManager != null)
            {
                gameplayManager.SetActive(true); // Optional: enable gameplay logic
            }
        }
    }

    // Called by Quit Game button
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // For testing inside Unity
#endif
    }

    // SETTINGS PANEL
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    // CREDITS PANEL
    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
