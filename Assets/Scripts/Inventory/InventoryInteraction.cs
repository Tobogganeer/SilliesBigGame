using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryInteraction : MonoBehaviour
{
    private static InventoryInteraction instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject InventoryPanel;
    public GameObject ItemPanel;

    [Space]
    public string heldItemKey = string.Empty;

    [Space]
    public DraggedItemGUI draggedItem;

    const int CraftResultSlot = 10;
    const int CraftingSlot1 = 8;
    const int CraftingSlot2 = 9;

    public static string CurrentHeldItem => instance == null ? string.Empty : instance.heldItemKey;

    public void OnToggleButtonClicked()
    {
        InventoryPanel.SetActive(!InventoryPanel.activeInHierarchy);
    }

    public void OnItemSlotClicked(GameObject itemSlot)
    {
        ItemSlot slot = itemSlot.GetComponent<ItemSlot>();

        // Check if we are holding an item right now
        if (heldItemKey != string.Empty)
        {
            if (slot.itemDataKey == string.Empty) // if the slot is empty
            {
                if (slot.inventorySlot == CraftResultSlot) return;
                if (slot.inventorySlot == CraftingSlot1 || slot.inventorySlot == CraftingSlot2)
                {
                    // Can't put an item with no recipes into a crafting slot
                    if (ItemData.GetData(heldItemKey)["combination"] == string.Empty) return;
                }

                // Put held item into slot
                slot.itemDataKey = heldItemKey;
                slot.UpdateGraphics();

                ConsumeHeldItem();
            }
        }
        // Not holding an item - pick it up
        else
        {
            heldItemKey = slot.itemDataKey;

            if (heldItemKey != string.Empty)
                draggedItem.Enable(ItemSprites.Get(heldItemKey));

            slot.Clear();
        }
    }

    public static void ConsumeHeldItem()
    {
        instance.heldItemKey = string.Empty;
        instance.draggedItem.Disable();
    }
}
