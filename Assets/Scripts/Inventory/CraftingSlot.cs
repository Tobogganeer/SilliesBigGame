using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingSlot : ItemSlot
{
    public CraftingScript craftingSystem;

    public override void Clear()
    {
        base.Clear();

        // When we take this item, clear/delete the ingredients too
        if (!craftingSystem.craftingSlot1.Empty && !craftingSystem.craftingSlot2.Empty)
        {
            craftingSystem.craftingSlot1.Clear();
            craftingSystem.craftingSlot2.Clear();
        }
    }
}
