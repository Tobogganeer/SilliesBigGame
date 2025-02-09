using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{

    public GameObject Camera;
    public GameObject InventoryPanel;
    public GameObject ItemPanel;

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
            if (itemData.itemDataKey == string.Empty)
            {
                itemData.itemDataKey = heldItemKey;

                heldItemKey = string.Empty;

                itemData.Search(itemData.itemDataKey);
            }
        }
        else
        {
            heldItemKey = itemData.itemDataKey;

            print(heldItemKey);

            itemData.Clear();
        }

        
    }
}
