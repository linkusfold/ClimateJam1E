using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * -----------------------------------------------
 * TooltipManager.cs
 * Author: Lauren Thoman
 * Date: July 8, 2025
 *
 * Singleton that drives the tooltip UI:
 * follows the mouse, shows/hides the panel,
 * and updates the TextMeshProUGUI content.
 * -----------------------------------------------
 */

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager _instance;
    public TextMeshProUGUI textComponent;
    RectTransform rt;
    Canvas canvas;

    void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = this;

        rt = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    void Start()
    {
        Cursor.visible = true;
        gameObject.SetActive(false);
    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;

        // get the size of the tooltip in screen pixels
        Vector2 tooltipSize = rt.sizeDelta * canvas.scaleFactor;

        // clamp x and y so that the entire rect stays on screen
        float clampedX = Mathf.Clamp(
            mousePos.x,
            tooltipSize.x * rt.pivot.x,
            Screen.width  - tooltipSize.x * (1 - rt.pivot.x)
        );
        float clampedY = Mathf.Clamp(
            mousePos.y,
            tooltipSize.y * rt.pivot.y,
            Screen.height - tooltipSize.y * (1 - rt.pivot.y)
        );

        // apply the clamped position
        rt.position = new Vector3(clampedX, clampedY, 0);
    }

    public void SetAndShowTooltip(string msg)
    {
        gameObject.SetActive(true);
        textComponent.text = msg;
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
        textComponent.text = string.Empty;
    }
}
