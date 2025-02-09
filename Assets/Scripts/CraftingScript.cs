using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingScript : MonoBehaviour
{

    public GameObject Craftingslot1;
    public GameObject Craftingslot2;
    public ItemData ItemData;
    public GameObject ResultSlot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Craftingslot1.GetComponent<ItemSlot>().itemDataKey != string.Empty && Craftingslot1.GetComponent<ItemSlot>().itemDataKey != string.Empty)
        {
            string itemKey1 = Craftingslot1.GetComponent<ItemSlot>().itemDataKey;
            string itemKey2 = Craftingslot2.GetComponent<ItemSlot>().itemDataKey;
            if (ItemData.itemData[itemKey1]["combination"] == itemKey2)
            {
                if (ItemData.itemData[itemKey2]["combination"] == itemKey1)
                {
                    ResultSlot.GetComponent<ItemSlot>().itemDataKey = ItemData.itemData[itemKey2]["combinationResult"];
                    ResultSlot.GetComponent<ItemSlot>().Search(ResultSlot.GetComponent<ItemSlot>().itemDataKey);
                }
            }
        }
    }
}
