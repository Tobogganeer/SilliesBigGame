using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        return instance.slots.Any((slot) => slot.itemDataKey == itemID);
    }

    public static void ConsumeItem(string itemID)
    {
        ItemSlot slot = instance.slots.First((slot) => slot.itemDataKey == itemID);
        slot.Clear();
    }

    public static void GiveItem(string itemID)
    {
        ItemSlot slot = instance.slots.First((slot) => slot.itemDataKey == string.Empty);
        slot.itemDataKey = itemID;
        slot.Search(itemID);
    }
}
