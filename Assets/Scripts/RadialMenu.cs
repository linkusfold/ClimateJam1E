using UnityEngine;
using UnityEngine.UI;

/*
 * -----------------------------------------------
 * RadialMenu.cs
 * Author: Angel
 * Date: June 25, 2025
 *
 * Displays a radial UI menu with buttons arranged
 * in a circular layout. Connects button actions to
 * tower functionality like upgrading.
 * -----------------------------------------------
 */

public class RadialMenu : MonoBehaviour
{
    public float radius = 100f;         // Distance of buttons from center
    private Tower targetTower;          // Reference to the associated tower
    RectTransform rectTransform;
    RectTransform canvasRect;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        // Connect UpgradeButton to the tower's upgrade logic
        Transform upgradeBtn = transform.Find("UpgradeButton");
        if (upgradeBtn != null)
        {
            Button btn = upgradeBtn.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(() =>
                {
                    if (targetTower != null)
                    {
                        targetTower.Upgrade();
                        RadialMenuManager.Instance.CloseMenu();
                    }
                });
            }
        }
    }

    void Update()
    {
        if (!targetTower) return;
        Vector3 screenPos = Camera.main!.WorldToScreenPoint(targetTower.transform.position);

        // Convert screen position to local position in the canvas
        Vector2 uiPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, null, out uiPos);

        rectTransform.anchoredPosition = uiPos;
    }

    // Links the radial menu to a specific tower
    public void SetTarget(Tower tower)
    {
        targetTower = tower;
        ArrangeButtons();
    }

    // Positions all child buttons in a circular layout
    void ArrangeButtons()
    {
        int count = transform.childCount;
        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            Transform button = transform.GetChild(i);
            float angle = i * angleStep * Mathf.Deg2Rad;

            Vector2 pos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            button.localPosition = pos;
        }
    }
}
