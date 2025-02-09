using System.Collections;
using System.Collections.Generic;
using Tobo.Util;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public static ItemData instance;
    /*private void Awake()
    {
        instance = this;
    }
    */

    public SerializableDictionary<string, ItemSO> items;

    public Dictionary<string, Dictionary<string, string>> itemData = new Dictionary<string, Dictionary<string, string>>();

    private void Awake()
    {
        itemData.Add("Flashlight", new Dictionary<string, string> {
            {"itemDescription", "To venture in the dark, except it has no power." },
            {"itemImage", "ItemSprites/flashlightTest"},
            {"combination", "Battery"},
            {"combinationResult", "Charged Flashlight"}
        });
        itemData.Add("Battery", new Dictionary<string, string> {
            {"itemDescription", "To power electronic objects." },
            {"itemImage", "ItemSprites/flashlightTest"},
            {"combination", "Flashlight"},
            {"combinationResult", "Charged Flashlight"}
        });
        itemData.Add("Key", new Dictionary<string, string> {
            {"itemDescription", "Now you can leave." },
            {"itemImage", "ItemSprites/flashlightTest"},
            {"combination", string.Empty},
        });
        itemData.Add("Charged Flashlight", new Dictionary<string, string>
        {
            {"itemDescription", "Now you can See."},
            {"itemImage", "ItemSprites/flashlightTest"},
            {"combination", string.Empty},
        });
    }

    // Usage (from anywhere): 'ItemData.GetItem("Battery").icon' etc
    public static ItemSO GetItem(string id)
    {
        return instance.items[id];
    }

}
