using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingScript : MonoBehaviour
{

    public GameObject Craftingslot1;
    public GameObject Craftingslot2;
    public ItemData ItemData;
    public GameObject ResultSlot;
    public ItemSlot ResultsScript;

    // Start is called before the first frame update
    void Start()
    {
        ResultsScript = ResultSlot.GetComponent<ItemSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Craftingslot1.GetComponent<ItemSlot>().itemDataKey != string.Empty && Craftingslot2.GetComponent<ItemSlot>().itemDataKey != string.Empty)
        {
            string itemKey1 = Craftingslot1.GetComponent<ItemSlot>().itemDataKey;
            string itemKey2 = Craftingslot2.GetComponent<ItemSlot>().itemDataKey;
            if (ItemData.itemData[itemKey1]["combination"] == itemKey2)
            {;

                if (ItemData.itemData[itemKey2]["combination"] == itemKey1)
                {
                    ResultSlot.GetComponent<ItemSlot>().itemDataKey = ItemData.itemData[itemKey2]["combinationResult"];
                    ResultSlot.GetComponent<ItemSlot>().Search(ResultSlot.GetComponent<ItemSlot>().itemDataKey);
                    
                }
            }
        }
        else
        {
            ResultsScript.itemImageSlot.sprite = null;
            ResultsScript.itemImageSlot.color = new Vector4(255, 255, 255, 0);
            ResultsScript.itemDataKey = string.Empty;
            ResultsScript.itemDescription = string.Empty;
        }
    }
}
