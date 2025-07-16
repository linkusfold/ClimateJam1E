/*
 * LoseScreenUI.cs
 * Author: Lauren Thoman
 * Date: July 7, 2025
 *
 * Displays the Lose Screen UI when the player loses a round.
 * Includes buttons for retrying the round and quitting the game.
 * Gameplay logic must be added later by the dev team.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreenUI : MonoBehaviour
{
    public static LoseScreenUI Instance;

    public GameObject loseScreenPanel;

    void Awake()
    {
        Instance = this;
    }

    // Called by the game manager when the player loses
    public void ShowLoseScreen()
    {
        Time.timeScale = 0f;  // Optional: pause the game
        loseScreenPanel.SetActive(true);
    }

    // Called by Try Again button
    public void OnRetryButton()
    {
        Time.timeScale = 1f;

        // Placeholder to restart the round
        Debug.Log("Retry triggered. Add round reset logic here.");


        // Example stub to be implemented

        // GameManager.Instance.RestartCurrentRound();
        // ^^ can be uncommented when round restart is implemented to other parts ^^
    }

    // Called by Quit button
    public void OnQuitButton()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

//  IMPORTANT
//  Implementation notes:
//  round retry logic to trigger the lose screen:

//  LoseScreenUI.Instance.ShowLoseScreen();
