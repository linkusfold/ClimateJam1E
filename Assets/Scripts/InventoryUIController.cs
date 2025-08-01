using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

/*
 * InventoryRevealController.cs
 * Author: Lauren Thoman
 * Date: June 29, 2025 (Updated July 14, 2025; CanvasGroup added July 29, 2025)
 *
 * Manages the inventory UI panel behavior, including:
 *  - showing/hiding via open/close buttons
 *  - blocking clicks outside the panel via CanvasGroup.blocksRaycasts
 *  - displaying items in a grid
 *  - showing hover tooltips
 *  - updating detail text
 *  - adding placeholder items
 * Max 8 items allowed.
 */

public class InventoryRevealController : MonoBehaviour
{
    [Header("Backpack Panel & Buttons")]
    [Tooltip("The UI panel that contains your backpack icons (must have a CanvasGroup)")]
    public GameObject backpackPanel;
    public Button openBackpackButton;
    public Button closeBackpackButton;

    [Serializable]
    public struct ItemSlot
    {
        public string key;       // identifier used by dialogue
        public GameObject icon;  // the icon GameObject to show/hide
    }

    [Header("Assign each item key + its icon GameObject here")]
    public List<ItemSlot> items = new List<ItemSlot>();

    private Dictionary<string, GameObject> _lookup;
    private CanvasGroup _panelCanvasGroup;

    void Awake()
    {
        // cache or add CanvasGroup
        _panelCanvasGroup = backpackPanel.GetComponent<CanvasGroup>();
        if (_panelCanvasGroup == null)
            _panelCanvasGroup = backpackPanel.AddComponent<CanvasGroup>();

        // start hidden & non-interactable
        backpackPanel.SetActive(false);
        _panelCanvasGroup.blocksRaycasts = false;
        _panelCanvasGroup.interactable     = false;

        // build lookup and hide all icons
        _lookup = new Dictionary<string, GameObject>(items.Count);
        foreach (var slot in items)
        {
            if (slot.icon != null && !_lookup.ContainsKey(slot.key))
            {
                _lookup[slot.key] = slot.icon;
                slot.icon.SetActive(false);
            }
        }
    }

    void Start()
    {
        openBackpackButton?.onClick.AddListener(ShowBackpack);
        closeBackpackButton?.onClick.AddListener(HideBackpack);
    }

    private void ShowBackpack()
    {
        backpackPanel.SetActive(true);

        // allow the panel (and its children) to receive clicks,
        // but block everything else behind it
        _panelCanvasGroup.blocksRaycasts = true;
        _panelCanvasGroup.interactable     = true;

        // optional: Time.timeScale = 0f;
    }

    private void HideBackpack()
    {
        // stop panel from intercepting clicks
        _panelCanvasGroup.blocksRaycasts = false;
        _panelCanvasGroup.interactable     = false;

        backpackPanel.SetActive(false);

        // optional: Time.timeScale = 1f;
    }

    /// <summary>
    /// Reveal an item by key (e.g. from NPC/dialogue code).
    /// </summary>
    public void RevealItem(string key)
    {
        if (_lookup.TryGetValue(key, out var icon))
        {
            icon.SetActive(true);
            Debug.Log($"[Inventory] Revealed item: {key}");
        }
        else
        {
            Debug.LogWarning($"[Inventory] No item with key '{key}' found.");
        }
    }
}
