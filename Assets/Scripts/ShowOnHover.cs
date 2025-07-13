using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * -----------------------------------------------
 * ShowOnHover.cs
 * Author: Lauren Thoman
 * Date: July 12, 2025
 *
 * Shows the flipped‐notes tooltip when the user hovers
 * over this button in the pause menu, and hides the
 * button’s normal sprite while the tooltip is visible.
 * -----------------------------------------------
 */

public class ShowOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("Drag the child Image (flipped‐notes) here")]
    public GameObject tooltip;

    [Tooltip("Drag the Image component of the main button here")]
    public Image mainImage;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (mainImage != null)
            mainImage.enabled = false;

        if (tooltip != null)
            tooltip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltip != null)
            tooltip.SetActive(false);

        if (mainImage != null)
            mainImage.enabled = true;
    }
}
