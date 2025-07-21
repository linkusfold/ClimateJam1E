using UnityEngine;
using UnityEngine.EventSystems;

/*
 * -----------------------------------------------
 * TooltipTrigger.cs
 * Author: Lauren Thoman
 * Date: July 8, 2025
 *
 * Attach to any UI element or world-space object
 * to forward hover enter/exit events into the
 * TooltipManager with a custom message.
 * Supports both IPointer interfaces for UI and
 * OnMouse callbacks for physics objects.
 * -----------------------------------------------
 */

public class TooltipTrigger : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    [TextArea] public string message;

    // UI hover
    public void OnPointerEnter(PointerEventData e)
        => TooltipManager._instance.SetAndShowTooltip(message);

    public void OnPointerExit(PointerEventData e)
        => TooltipManager._instance.HideTooltip();

    // World-space hover (3D or 2D)
    void OnMouseEnter()
        => TooltipManager._instance.SetAndShowTooltip(message);

    void OnMouseExit()
        => TooltipManager._instance.HideTooltip();
}
