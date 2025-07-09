using UnityEngine;

/*
 * -----------------------------------------------
 * ScrollCamera.cs
 * Author: Lauren Thoman
 * Date: June 22, 2025
 *
 * Allows the player to scroll the camera vertically
 * either by moving the mouse to the screen's edges
 * or by using their mouse scroll wheel.
 * -----------------------------------------------
 */

public class ScrollCamera : MonoBehaviour
{
    
    public bool isLocked = false;
    
    // How fast the camera moves
    public float scrollSpeed = 5f;

    // Margin in pixels from the screen edge that triggers scrolling
    public float edgeMargin = 10f;

    void Update()
    {
        if (isLocked) return;
        
        // Gets the current camera position
        Vector3 pos = transform.position;

        // Gets the vertical mouse position
        float mouseY = Input.mousePosition.y;

        // Gets scroll wheel input (positive = up, negative = down)
        float scrollInput = Input.mouseScrollDelta.y;

        // ----------- Edge of screen movement -----------

        // If mouse is near top edge, scroll up
        if (mouseY >= Screen.height - edgeMargin)
        {
            pos.y += scrollSpeed * Time.deltaTime;
        }
        // If mouse is near bottom edge, scroll down
        else if (mouseY <= edgeMargin)
        {
            pos.y -= scrollSpeed * Time.deltaTime;
        }

        // ----------- Scroll wheel movement -----------

        // Apply scroll wheel input
        pos.y += scrollInput * scrollSpeed;

        // Updates camera position
        transform.position = pos;
    }
}
