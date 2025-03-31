using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tobo.DevConsole;
using UnityEngine;

public class Commands : MonoBehaviour
{
    ConCommand give;
    ConCommand room1code;
    ConCommand room2code;

    private void Awake()
    {
        give = new ConCommand("give", Give, "Gives an item", CVarFlags.None, GetGiveArgOptions);
        room1code = new ConCommand("room1code", (args) => Debug.Log("6327"), "Prints room 1 code (6327)");
        room1code = new ConCommand("room2code", (args) => Debug.Log("7361"), "Prints room 1 code (7361)");
    }

    #region Give
    void Give(CmdArgs args)
    {
        if (args.ArgC > 1 && ItemData.instance.itemData.ContainsKey(args.Args[1])) // Check if they put in an item
            Inventory.GiveItem(args.Args[1]);
    }

    List<string> GetGiveArgOptions(string partialFirstArg)
    {
        //return new List<string>() { "flashlight" };
        return ItemData.instance.itemData.Keys.ToList();
    }
    #endregion
}
