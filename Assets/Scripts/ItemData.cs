using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public Dictionary<string, Dictionary<string, string>> itemData = new Dictionary<string, Dictionary<string, string>>();


    private void Start()
    {
        itemData.Add("Flashlight", new Dictionary<string, string> { 
            { "ItemDescription", "To venture in the dark" },
            {"itemImage", "ItemSprites/flashlightTest"}
        });
        itemData.Add("Battery", new Dictionary<string, string> {
            { "ItemDescription", "To power electronic objects" },
            {"itemImage", "ItemSprites/flashlightTest"}
        });
        itemData.Add("Key", new Dictionary<string, string> {
            { "ItemDescription", "Now you can leave" },
            {"itemImage", "ItemSprites/flashlightTest"}
        });

        print(itemData.Keys);
    }

}
