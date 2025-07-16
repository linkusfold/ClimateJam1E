/*
 * RoundStarterUI.cs
 * Author: Lauren Thoman
 * Date: July 7, 2025
 *
 * Controls the "Start Round" button that appears after dialogue.
 * Calls a placeholder method for gameplay round start logic.
 */

using UnityEngine;

public class RoundStarterUI : MonoBehaviour
{
    public static RoundStarterUI Instance;  // Allows global access

    public GameObject startRoundButton;

    private void Awake()
    {
        Instance = this;  // Set the static reference when the script awakens
    }

    // Called by the button itself
    public void OnStartRoundPressed()
    {
        // Hide the button once clicked
        startRoundButton.SetActive(false);

        // Placeholder to insert round-start logic
        Debug.Log("Round start triggered! Insert gameplay logic here.");

        // Example stub that can be replaced
        // GameManager.Instance.BeginDisasterWave();  <-- imaginary method
    }

    // Optional: show this via dialogue or cutscene trigger
    public void ShowStartRoundButton()
    {
        startRoundButton.SetActive(true);
    }
}

//  IMPORTANT
//  Trigger the Button From Anywhere Using
//  RoundStarterUI.Instance.ShowStartRoundButton();