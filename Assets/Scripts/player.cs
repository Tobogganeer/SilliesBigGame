using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{

    public GameObject Camera;
    public GameObject InventoryPanel;
    public GameObject ItemPanel;

    public Dictionary<int, string> Inventory = new() 
    {
    };
    // Start is called before the first frame update
    void Start()
    {
       for (int i = 0; i < ItemPanel.transform.childCount; i++)
        {
            Inventory.Add(i,ItemPanel.transform.GetChild(i).GetComponent<ItemSlot>().itemDataKey);
            Debug.Log(Inventory[i]);
        }


    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void InventoryToggle()
    {
        InventoryPanel.SetActive(!InventoryPanel.activeInHierarchy);
    }
}
