using System;
using System.Collections;
using System.Collections.Generic;
using Tobo.Audio;
using UnityEngine;

[RequireComponent(typeof(SwapObjects))]
public class Drawer : MonoBehaviour
{
    public string itemID;

    bool gaveItem;

    private void Start()
    {
        //objectsToSwapBetween[1].SetActive(false);
        GetComponent<SwapObjects>().onTrySwap = OnTrySwap;
    }

    bool OnTrySwap(SwapObjects s)
    {
        if (!s.inDefaultState && !gaveItem)
        {
            gaveItem = true;
            Inventory.GiveItem(itemID, true, Sound.ItemPickup);
            return false; // Don't close the drawer when we get the item
        }

        return true;
    }
}
