using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingScript : MonoBehaviour
{
    public ItemSlot craftingSlot1;
    public ItemSlot craftingSlot2;
    public ItemSlot resultSlot;

    void Update()
    {
        string firstItem = craftingSlot1.itemDataKey;
        string secondItem = craftingSlot2.itemDataKey;

        // Check if both slots are full
        if (firstItem != string.Empty && secondItem != string.Empty)
        {
            // Check if the items combine with each other
            if (ItemData.GetData(firstItem)["combination"] == secondItem && ItemData.GetData(secondItem)["combination"] == firstItem)
            {
                resultSlot.itemDataKey = ItemData.GetData(secondItem)["combinationResult"];
                resultSlot.UpdateGraphics();
            }
        }
        else
        {
            // No valid combo - clear result
            resultSlot.Clear();
        }
    }
}
