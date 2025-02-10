using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryInteraction : MonoBehaviour
{

    public GameObject InventoryPanel;
    public GameObject ItemPanel;

    public ItemData ItemData;

    public string heldItemKey;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void InventoryToggle()
    {
        InventoryPanel.SetActive(!InventoryPanel.activeInHierarchy);
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
            }
        }
        else
        {
            heldItemKey = itemData.itemDataKey;

            itemData.Clear();
        }

        
    }
}
