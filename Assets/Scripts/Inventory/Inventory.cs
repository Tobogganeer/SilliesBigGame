using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Tobo.Audio;

public class Inventory : MonoBehaviour
{
    private static Inventory instance;
    private void Awake()
    {
        instance = this;
    }

    public List<ItemSlot> slots;

    public static bool HasItem(string itemID)
    {
        return instance.slots.Any((slot) => slot.itemDataKey == itemID) || InventoryInteraction.CurrentHeldItem == itemID;
    }

    public static void ConsumeItem(string itemID)
    {
        // Check if we are holding the item we want to consume
        if (InventoryInteraction.CurrentHeldItem == itemID)
        {
            InventoryInteraction.ConsumeHeldItem();
            return;
        }

        ItemSlot slot = instance.slots.First((slot) => slot.itemDataKey == itemID);
        slot.Clear();
    }

    public static void GiveItem(string itemID, bool notify = true, Sound sound = null)
    {
        ItemSlot slot = instance.slots.First((slot) => slot.itemDataKey == string.Empty);
        slot.itemDataKey = itemID;
        slot.UpdateGraphics();

        if (notify)
            PopUp.Show("Picked up " + itemID, 3f);

        if (sound != null)
            sound.PlayDirect();
    }
}
