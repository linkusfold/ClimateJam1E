using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

/*
 * -----------------------------------------------
 * InventoryUIController.cs
 * Author: Lauren Thoman
 * Date: June 29, 2025 (Updated July 14, 2025)
 *
 * Manages the inventory UI panel behavior, including
 * showing/hiding via dedicated open and close buttons,
 * displaying items in a grid, showing hover tooltips,
 * updating a detail text box, and adding placeholder items.
 * Ready for NPC-driven item additions. Max 8 items allowed.
 * -----------------------------------------------
 */
public class InventoryRevealController : MonoBehaviour
{
    [Header("Backpack Panel & Buttons")]
    [Tooltip("The UI panel (e.g. Canvas Group or GameObject) that contains your backpack icons")]
    public GameObject backpackPanel;
    [Tooltip("Button that opens the backpack panel")]
    public Button openBackpackButton;
    [Tooltip("Button that closes the backpack panel")]
    public Button closeBackpackButton;

    [Serializable]
    public struct ItemSlot
    {
        public string key;       // the identifier you use in your dialogue script
        public GameObject icon;  // the GameObject (or Image) to show/hide
    }

    [Header("Assign each item key + its icon GameObject here")]
    public List<ItemSlot> items = new List<ItemSlot>();

    // fast lookup at runtime
    private Dictionary<string, GameObject> _lookup;

    void Awake()
    {
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

        // ensure backpack starts hidden
        if (backpackPanel != null)
            backpackPanel.SetActive(false);
    }

    void Start()
    {
        // wire up your open/close buttons
        if (openBackpackButton != null)
            openBackpackButton.onClick.AddListener(ShowBackpack);

        if (closeBackpackButton != null)
            closeBackpackButton.onClick.AddListener(HideBackpack);
    }

    /// <summary>
    /// Reveal (enable) the backpack UI.
    /// </summary>
    private void ShowBackpack()
    {
        if (backpackPanel == null) return;
        backpackPanel.SetActive(true);
        // optionally pause game: Time.timeScale = 0f;
    }

    /// <summary>
    /// Hide (disable) the backpack UI.
    /// </summary>
    private void HideBackpack()
    {
        if (backpackPanel == null) return;
        backpackPanel.SetActive(false);
        // optionally resume game: Time.timeScale = 1f;
    }

    /// <summary>
    /// Call this from your dialogue/NPC code to reveal an item.
    /// e.g. InventoryRevealController controller = FindObjectOfType<InventoryRevealController>();
    ///      controller.RevealItem("SilverSword");
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


//  Triggering from dialogue
//  Wherever NPC says you earned and item add:

//  InventoryRevealController controller = FindObjectOfType<InventoryRevealController>();
//  controller.RevealItem("SilverSword");

//  cache a reference
//  InventoryRevealController.Instance.RevealItem("SilverSword");

