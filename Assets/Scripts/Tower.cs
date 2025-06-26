using UnityEngine;

/*
 * -----------------------------------------------
 * Tower.cs
 * Author: Angel
 * Date: June 25, 2025
 *
 * Represents a tower that can be interacted with.
 * Clicking the tower opens a radial menu with options.
 * Includes upgrade functionality.
 * -----------------------------------------------
 */

public class Tower : MonoBehaviour
{
    public int level = 1;              // Tracks the tower's level
    public bool isUnlocked = false;   // Whether the tower is unlocked

    // Called automatically when this GameObject is clicked
    void OnMouseDown()
    {
        Debug.Log("Tower clicked!");
        RadialMenuManager.Instance.ShowMenu(this);  // Show the radial menu
    }

    // Upgrades the tower
    public void Upgrade()
    {
        level++;
        isUnlocked = true;
        Debug.Log($"Tower upgraded to level {level}");
    }
}
