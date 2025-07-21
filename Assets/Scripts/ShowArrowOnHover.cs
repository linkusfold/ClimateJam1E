using UnityEngine;
using UnityEngine.EventSystems;

/*
 * -----------------------------------------------
 * ShowArrowOnHover.cs
 * Author: Lauren Thoman
 * Date: July 11, 2025
 *
 * Shows or hides the arrow tooltip image when the user
 * hovers on/off this button in the main menu.
 * -----------------------------------------------
 */

public class ShowArrowOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("Drag the child Arrow Image here")]
    public GameObject arrow;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (arrow != null)
            arrow.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (arrow != null)
            arrow.SetActive(false);
    }
}
