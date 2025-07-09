using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.EventSystems;

/*
 * -----------------------------------------------
 * InventoryUIController.cs
 * Author: Lauren Thoman
 * Date: June 29, 2025 (Updated July 8, 2025)
 *
 * Manages the inventory UI panel behavior, including
 * showing/hiding via dedicated open and close buttons,
 * displaying items in a grid, and showing hover tooltips.
 * Ready for NPC-driven item additions. Max 8 items allowed.
 * -----------------------------------------------
 */
public class InventoryUIController : MonoBehaviour
{
    [Header("UI References")]
    public GameObject inventoryPanel;        // The panel to show/hide
    public Button openInventoryButton;       // Button that opens the inventory
    public Button closeInventoryButton;      // Button that closes the inventory
    public Transform itemGrid;               // Grid container (with GridLayoutGroup)
    public GameObject slotPrefab;            // Prefab for each item slot

    [Header("Tooltip")]
    public GameObject tooltipPanel;          // Tooltip background panel
    public TextMeshProUGUI tooltipText;      // Tooltip text field

    [Header("Tooltip Offset")]
    public Vector3 tooltipOffset = new Vector3(10f, -10f, 0f); // Customizable in Inspector

    // In-memory list of items (title + description)
    private List<InventoryItem> items = new List<InventoryItem>();
    private const int maxItems = 8;

    void Start()
    {
        if (inventoryPanel != null)
            inventoryPanel.SetActive(false);

        if (openInventoryButton != null)
        {
            openInventoryButton.onClick.RemoveAllListeners();
            openInventoryButton.onClick.AddListener(OpenInventory);
        }

        if (closeInventoryButton != null)
        {
            closeInventoryButton.onClick.RemoveAllListeners();
            closeInventoryButton.onClick.AddListener(CloseInventory);
        }

        items.Add(new InventoryItem("Gift from Mom", "A thoughtful gift from your mom."));
        RefreshUI();
    }

    // Opens inventory panel and pauses game
    public void OpenInventory()
    {
        if (inventoryPanel == null) return;

        inventoryPanel.SetActive(true);
        Time.timeScale = 0f;
        RefreshUI();
    }

    // Closes inventory panel and resumes game
    public void CloseInventory()
    {
        if (inventoryPanel == null) return;

        inventoryPanel.SetActive(false);
        Time.timeScale = 1f;
        HideTooltip();
    }

    // Adds new item to inventory, up to 8 max
    public void AddItem(string title, string description)
    {
        if (items.Count >= maxItems)
        {
            Debug.LogWarning("Inventory is full. Maximum of 8 items allowed.");
            return;
        }

        items.Add(new InventoryItem(title, description));

        if (inventoryPanel.activeSelf)
            RefreshUI();
    }

    // Updates the visible slots in the inventory UI
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

        StartCoroutine(DelayedTooltipPosition());
    }

    private System.Collections.IEnumerator DelayedTooltipPosition()
    {
        yield return null;
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
