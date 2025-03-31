using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tobo.DevConsole;
using UnityEngine;

public class Commands : MonoBehaviour
{
    ConCommand give;

    private void Awake()
    {
        give = new ConCommand("give", Give, "Gives an item", CVarFlags.None, GetGiveArgs);
    }

    void Give(CmdArgs args)
    {
        if (args.ArgC > 1 && ItemData.instance.itemData.ContainsKey(args.Args[1])) // Check if they put in an item
            Inventory.GiveItem(args.Args[1]);
    }

    List<string> GetGiveArgs(string partialFirstArg)
    {
        return new List<string>() { "flashlight" };
        //return ItemData.instance.itemData.Keys.ToList();
    }
}
