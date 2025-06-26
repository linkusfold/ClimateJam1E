using UnityEngine;

/*
 * -----------------------------------------------
 * RadialMenuManager.cs
 * Author: Angel
 * Date: June 25, 2025
 *
 * Handles the spawning and closing of radial menus
 * when towers are clicked.
 * -----------------------------------------------
 */

public class RadialMenuManager : MonoBehaviour
{
    public static RadialMenuManager Instance;        // Singleton reference
    public GameObject radialMenuPrefab;              // Prefab for the menu UI
    private GameObject currentMenu;                  // Tracks the active menu

    void Awake()
    {
        // Enforce singleton pattern
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Shows the radial menu for the specified tower
    public void ShowMenu(Tower tower)
    {
        // Remove the old menu if one exists
        if (currentMenu != null)
            Destroy(currentMenu);

        // Convert world position to screen position
        Vector3 screenPos = Camera.main.WorldToScreenPoint(tower.transform.position);

        // Spawn new radial menu under Canvas
        currentMenu = Instantiate(radialMenuPrefab, screenPos, Quaternion.identity, GameObject.Find("Canvas").transform);

        // Link the menu to the clicked tower
        currentMenu.GetComponent<RadialMenu>().SetTarget(tower);
    }

    // Closes the active radial menu
    public void CloseMenu()
    {
        if (currentMenu != null)
        {
            Destroy(currentMenu);
            currentMenu = null;
        }
    }
}