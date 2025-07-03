using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.EventSystems;

/*
 * -----------------------------------------------
 * InventoryUIController.cs
 * Author: Lauren Thoman
 * Date: June 29, 2025 (Updated July 2, 2025)
 *
 * Manages the inventory UI panel behavior, including
 * toggling via keyboard ('B') or UI button, pausing the game,
 * displaying items in a grid, and showing hover tooltips.
 * Ready for NPC-driven item additions.
 * -----------------------------------------------
 */
public class InventoryUIController : MonoBehaviour
{
    [Header("UI References")]
    public GameObject inventoryPanel;      // The panel to show/hide
    public Button inventoryButton;         // Always-visible toggle button
    public Transform itemGrid;             // Grid container (with GridLayoutGroup)
    public GameObject slotPrefab;          // Prefab for each item slot

    [Header("Tooltip")]
    public GameObject tooltipPanel;        // Tooltip background panel
    public TextMeshProUGUI tooltipText;    // Tooltip text field

    [Header("Tooltip Offset")]
    public Vector3 tooltipOffset = new Vector3(10f, -10f, 0f); // Customizable in Inspector

    // In-memory list of items (title + description)
    private List<InventoryItem> items = new List<InventoryItem>();

    void Start()
    {
        if (inventoryPanel != null)
            inventoryPanel.SetActive(false);

        if (inventoryButton != null)
        {
            inventoryButton.onClick.RemoveAllListeners();
            inventoryButton.onClick.AddListener(ToggleInventory);
        }

        items.Add(new InventoryItem("Gift from Mom", "A thoughtful gift from your mom."));
        RefreshUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            ToggleInventory();

        // TEMP: Press T to add test item
        if (Input.GetKeyDown(KeyCode.T))
        {
            AddItem("Shiny Shell", "A smooth, glimmering shell found near the tide pool.");
        }
    }

    public void ToggleInventory()
    {
        if (inventoryPanel == null) return;

        bool open = !inventoryPanel.activeSelf;
        inventoryPanel.SetActive(open);
        Time.timeScale = open ? 0f : 1f;

        if (open)
            RefreshUI();
        else
            HideTooltip();
    }

    public void AddItem(string title, string description)
    {
        items.Add(new InventoryItem(title, description));
        if (inventoryPanel.activeSelf)
            RefreshUI();
    }

    private void RefreshUI()
    {
        foreach (Transform child in itemGrid)
            Destroy(child.gameObject);

        foreach (var it in items)
        {
            GameObject slot = Instantiate(slotPrefab, itemGrid);

            var label = slot.GetComponentInChildren<TextMeshProUGUI>();
            if (label) label.text = it.title;

            var trigger = slot.GetComponent<EventTrigger>();
            if (trigger == null)
                trigger = slot.AddComponent<EventTrigger>();
            trigger.triggers.Clear();

            var entryEnter = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerEnter
            };
            entryEnter.callback.AddListener(evt => ShowTooltip(it));
            trigger.triggers.Add(entryEnter);

            var entryExit = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerExit
            };
            entryExit.callback.AddListener(evt => HideTooltip());
            trigger.triggers.Add(entryExit);
        }
    }

    private void ShowTooltip(InventoryItem item)
    {
        if (tooltipPanel == null || tooltipText == null) return;
        tooltipPanel.SetActive(true);
        tooltipText.text = item.description;

        // Wait one frame before positioning so we don’t flicker
        StartCoroutine(DelayedTooltipPosition());
    }

    private System.Collections.IEnumerator DelayedTooltipPosition()
    {
        yield return null; // Wait 1 frame
        tooltipPanel.transform.position = Input.mousePosition + tooltipOffset;
    }

    private void HideTooltip()
    {
        if (tooltipPanel == null) return;
        tooltipPanel.SetActive(false);
    }
}

/// <summary>
/// Represents a single inventory item (title + description).
/// </summary>
public class InventoryItem
{
    public string title;
    public string description;

    public InventoryItem(string title, string description)
    {
        this.title = title;
        this.description = description;
    }
}