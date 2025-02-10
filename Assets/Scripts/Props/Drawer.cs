using System;
using System.Collections;
using System.Collections.Generic;
using Tobo.Audio;
using UnityEngine;

public class Drawer : MonoBehaviour, IInteractable
{
    // Code copied from Swapper because I am lazy
    public GameObject[] objectsToSwapBetween;
    public string itemID;

    bool open;
    bool gaveItem;

    private void Start()
    {
        objectsToSwapBetween[1].SetActive(false);
    }

    public void OnClicked()
    {
        if (!open)
            open = true;
        else if (!gaveItem)
        {
            gaveItem = true;
            Inventory.GiveItem(itemID, true, Sound.ItemPickup);
            return;
        }

        objectsToSwapBetween[0].SetActive(objectsToSwapBetween[1].activeSelf); // Set first to second's current state
        objectsToSwapBetween[1].SetActive(!objectsToSwapBetween[1].activeSelf); // Flip second's state
    }
}
