using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryInteraction : MonoBehaviour
{

    public GameObject InventoryPanel;
    public GameObject ItemPanel;

    [Space]
    public ItemData ItemData;

    [Space]
    public string heldItemKey;

    [Space]
    public DraggedItemGUI draggedItem;

    public void InventoryToggle()
    {
        InventoryPanel.SetActive(!InventoryPanel.activeInHierarchy);
        TelemetryLogger.Log(this, "Inventory Clicked"); // This logs the inventory opening and closing.
    }

    public void HeldItem(GameObject itemSlot)
    {
        ItemSlot itemData = itemSlot.GetComponent<ItemSlot>();

        if (heldItemKey != string.Empty)
        {
            if (itemData.itemDataKey == string.Empty) // if the slot is empty
            {
                if (itemData.inventorySlot == 10) return;
                if (itemData.inventorySlot == 8 || itemData.inventorySlot == 9)
                {
                    if (ItemData.itemData[heldItemKey]["combination"] == string.Empty) return;
                }
                itemData.itemDataKey = heldItemKey;

                heldItemKey = string.Empty;

                itemData.Search(itemData.itemDataKey);

                draggedItem.Disable();
            }
        }
        else
        {
            heldItemKey = itemData.itemDataKey;

            if (heldItemKey != string.Empty)
            {
                Sprite sprite = Resources.Load<Sprite>(ItemData.itemData[heldItemKey]["itemImage"]);
                draggedItem.Enable(sprite);
            }

            itemData.Clear();
        }

        
    }
}
