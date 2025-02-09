using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingSlot : ItemSlot
{
    public GameObject CraftingPanel;

    public override void Clear()
    {
        CraftingScript craftingScript = CraftingPanel.GetComponent<CraftingScript>();

        itemImageSlot.sprite = null;
        itemImageSlot.color = new Vector4(255, 255, 255, 0);
        itemDataKey = string.Empty;
        itemDescription = string.Empty;

        craftingScript.Craftingslot1.GetComponent<ItemSlot>().Clear();
        craftingScript.Craftingslot2.GetComponent<ItemSlot>().Clear();
    }
}
