using System.Collections;
using System.Collections.Generic;
using Tobo.Util;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public SerializableDictionary<string, ItemSO> items;

    public Dictionary<string, Dictionary<string, string>> itemData = new Dictionary<string, Dictionary<string, string>>();

    private void Start()
    {

        itemData.Add("Flashlight", new Dictionary<string, string> { 
            { "itemDescription", "To venture in the dark" },
            {"itemImage", "ItemSprites/flashlightTest"}
        });
        itemData.Add("Battery", new Dictionary<string, string> {
            { "itemDescription", "To power electronic objects" },
            {"itemImage", "ItemSprites/flashlightTest"}
        });
        itemData.Add("Key", new Dictionary<string, string> {
            { "itemDescription", "Now you can leave" },
            {"itemImage", "ItemSprites/flashlightTest"}
        });

    }

}
