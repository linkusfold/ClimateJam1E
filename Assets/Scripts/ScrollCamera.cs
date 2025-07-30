using UnityEngine;

/*
 * -----------------------------------------------
 * ScrollCamera.cs
 * Author: (you)
 * Date: July 2025
 *
 * Allows the player to scroll the camera vertically
 * only by using their mouse scroll wheel, clamped
 * to a small extra vertical range.
 * -----------------------------------------------
 */

public class ScrollCamera : MonoBehaviour
{
    [Header("Scroll Settings")]
    [Tooltip("How fast one notch of the scroll wheel moves the camera")]
    public float scrollSpeed = 1f;

    [Header("Vertical Bounds (world units)")]
    [Tooltip("Lowest Y the camera can go")]
    public float minY = 0f;
    [Tooltip("Highest Y the camera can go")]
    public float maxY = 2f;  // tweak this to just a bit above your default

    void Update()
    {
        // get scroll wheel delta
        float scrollInput = Input.mouseScrollDelta.y;
        if (Mathf.Approximately(scrollInput, 0f)) return;

        // calculate new position
        Vector3 pos = transform.position;
        pos.y += scrollInput * scrollSpeed * Time.deltaTime;

        // clamp to your little extra range
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }
}
