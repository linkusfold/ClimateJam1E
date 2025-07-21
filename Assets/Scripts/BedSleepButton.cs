/*
 * BedSleepButton.cs
 * Author: Lauren Thoman
 * Date: July 7, 2025
 *
 * Controls the bed icon in the top corner of the game.
 * Shows tooltip on hover, makes it follow the mouse,
 * and triggers "sleep to next day" logic on click.
 */

using UnityEngine;
using UnityEngine.EventSystems;

public class BedSleepButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("Assign the Tooltip Text UI element here")]
    public GameObject tooltipText;

    [Tooltip("Text to display when hovered")]
    public string tooltipMessage = "Sleep until next day";

    private RectTransform tooltipRect;
    private bool isHovering = false;
    private Vector2 offset = new Vector2(15f, -15f); // Offset from cursor

    void Start()
    {
        if (tooltipText != null)
            tooltipRect = tooltipText.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (isHovering && tooltipText.activeSelf)
        {
            // Follow the mouse with slight offset
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                tooltipRect.parent as RectTransform,
                Input.mousePosition + (Vector3)offset,
                null,
                out pos
            );
            tooltipRect.anchoredPosition = pos;
        }
    }

    // Show tooltip when mouse hovers over the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        tooltipText.SetActive(true);
        tooltipText.GetComponent<TMPro.TextMeshProUGUI>().text = tooltipMessage;
    }

    // Hide tooltip when mouse leaves
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        tooltipText.SetActive(false);
    }

    // Called when the bed is clicked
    public void OnSleepClicked()
    {
        Debug.Log("Sleep triggered. Advance to next day here.");
        // Example placeholder:
        // GameManager.Instance.AdvanceDay();  <-- to be implemented
    }
}


//  IMPORTANT
//  Trigger next day button using:
//  GameManager.Instance.AdvanceDay();
