using System.Collections;
using System.Collections.Generic;
using Tobo.Util;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public static ItemData instance;

    //public SerializableDictionary<string, ItemSO> items;

    public Dictionary<string, Dictionary<string, string>> itemData = new Dictionary<string, Dictionary<string, string>>();

    private void Awake()
    {
        instance = this;

        itemData.Add("Flashlight", new Dictionary<string, string> {
            {"itemDescription", "To venture in the dark, except it has no power." },
            {"itemImage", "ItemSprites/flashlight"},
            {"combination", "Battery"},
            {"combinationResult", "Charged Flashlight"}
        });
        itemData.Add("Battery", new Dictionary<string, string> {
            {"itemDescription", "To power electronic objects." },
            {"itemImage", "ItemSprites/Battery"},
            {"combination", "Flashlight"},
            {"combinationResult", "Charged Flashlight"}
        });
        itemData.Add("Cabinet Key", new Dictionary<string, string> {
            {"itemDescription", "Now you can open a Cabinet." },
            {"itemImage", "ItemSprites/CabinetKey"},
            {"combination", string.Empty},
        });
        itemData.Add("Charged Flashlight", new Dictionary<string, string>
        {
            {"itemDescription", "Now you can See."},
            {"itemImage", "ItemSprites/flashlight"},
            {"combination", string.Empty},
        });
        itemData.Add("Door Keys", new Dictionary<string, string>
        {
            {"itemDescription", "Now you can open a locked door."},
            {"itemImage", "ItemSprites/DoorKeys"},
            {"combination", string.Empty},
        });
        itemData.Add("Syringe", new Dictionary<string, string>
        {
            {"itemDescription", "A Syringe."},
            {"itemImage", "ItemSprites/Syringe"},
            {"combination", string.Empty},
        });


        itemData.Add("Red Flask", new Dictionary<string, string>
        {
            {"itemDescription", "A Red Flask"},
            {"itemImage", "ItemSprites/Flask1A"},
            {"combination", "Teal Flask"},
            {"combinationResult", "Syringe"},
        });
        itemData.Add("Green Flask", new Dictionary<string, string>
        {
            {"itemDescription", "A Green Flask"},
            {"itemImage", "ItemSprites/Flask1B"},
            {"combination", string.Empty},
        });
        itemData.Add("Blue Flask", new Dictionary<string, string>
        {
            {"itemDescription", "A Blue Flask"},
            {"itemImage", "ItemSprites/Flask1C"},
            {"combination", string.Empty},
        });
        itemData.Add("Pink Flask", new Dictionary<string, string>
        {
            {"itemDescription", "A Pink Flask"},
            {"itemImage", "ItemSprites/Flask2A"},
            {"combination", string.Empty},
        });
        itemData.Add("Teal Flask", new Dictionary<string, string>
        {
            {"itemDescription", "A Teal Flask"},
            {"itemImage", "ItemSprites/Flask2B"},
            {"combination", "Red Flask"},
            {"combinationResult", "Syringe"},
        });
        itemData.Add("Yellow Flask", new Dictionary<string, string>
        {
            {"itemDescription", "A Yellow Flask"},
            {"itemImage", "ItemSprites/Flask2C"},
            {"combination", string.Empty},
        });
        itemData.Add("Small Orange Flask", new Dictionary<string, string>
        {
            {"itemDescription", "A small Orange Flask"},
            {"itemImage", "ItemSprites/Flask3A"},
            {"combination", string.Empty},
        });
        itemData.Add("Small Teal Flask", new Dictionary<string, string>
        {
            {"itemDescription", "A small Red Flask"},
            {"itemImage", "ItemSprites/Flask3B"},
            {"combination", string.Empty},
        });
        itemData.Add("Small Purple Flask", new Dictionary<string, string>
        {
            {"itemDescription", "A small Purple Flask"},
            {"itemImage", "ItemSprites/Flask3C"},
            {"combination", string.Empty},
        });
        itemData.Add("Solution 1", new Dictionary<string, string>
        {
            {"itemDescription", "The first solution from the cabinet. It's colourless, so you can't tell what it is."},
            {"itemImage", "ItemSprites/Syringe"},
            {"combination", string.Empty},
        });
        itemData.Add("Solution 2", new Dictionary<string, string>
        {
            {"itemDescription", "The second solution from the cabinet. It's colourless, so you can't tell what it is."},
            {"itemImage", "ItemSprites/Syringe"},
            {"combination", "Solution 6"},
            {"combinationResult", "Removal Solvent"},
        });
        itemData.Add("Solution 3", new Dictionary<string, string>
        {
            {"itemDescription", "The third solution from the cabinet. It's colourless, so you can't tell what it is."},
            {"itemImage", "ItemSprites/Syringe"},
            {"combination", string.Empty},
        });
        itemData.Add("Solution 4", new Dictionary<string, string>
        {
            {"itemDescription", "The fourth solution from the cabinet. It's colourless, so you can't tell what it is."},
            {"itemImage", "ItemSprites/Syringe"},
            {"combination", string.Empty},
        });
        itemData.Add("Solution 5", new Dictionary<string, string>
        {
            {"itemDescription", "The fifth solution from the cabinet. It's colourless, so you can't tell what it is."},
            {"itemImage", "ItemSprites/Syringe"},
            {"combination", string.Empty},
        });
        itemData.Add("Solution 6", new Dictionary<string, string>
        {
            {"itemDescription", "The sixth solution from the cabinet. It's colourless, so you can't tell what it is."},
            {"itemImage", "ItemSprites/Syringe"},
            {"combination", "Solution 2"},
            {"combinationResult", "Removal Solvent"},
        });
        itemData.Add("Removal Solvent", new Dictionary<string, string>
        {
            {"itemDescription", "Hard-earned caulk removal solvent."},
            {"itemImage", "ItemSprites/Flask1B"},
            {"combination", string.Empty},
        });
    }

    public static Dictionary<string, string> GetData(string item) => instance.itemData[item];

    // Usage (from anywhere): 'ItemData.GetItem("Battery").icon' etc
    /*
    public static ItemSO GetItem(string id)
    {
        return instance.items[id];
    }
    */
}
