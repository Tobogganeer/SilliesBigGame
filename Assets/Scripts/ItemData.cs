using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public Dictionary<string, Dictionary<string, string>> itemData = new Dictionary<string, Dictionary<string, string>>();


    private void Start()
    {
        itemData["Flashlight"] = new Dictionary<string, string> 
        {
            {"itemDescription", "To venture in the dark"},
            {"itemImage", "Assets/Textures/ItemSprites/flashlightTest.png"}
        };
    }

}
