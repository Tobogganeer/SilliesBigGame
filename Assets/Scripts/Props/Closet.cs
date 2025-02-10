using System.Collections;
using System.Collections.Generic;
using Tobo.Audio;
using UnityEngine;

public class Closet : MonoBehaviour, IInteractable
{
    bool gaveKeys;

    public void OnClicked()
    {
        if (Inventory.HasItem("Charged Flashlight"))
        {
            if (!gaveKeys)
            {
                Inventory.GiveItem("Door Keys", true, Sound.GetKey);
                gaveKeys = true;
            }
            else
                PopUp.Show("Nothing else in here...", 1.5f);
        }
        else
            PopUp.Show("Lights are dead. Of course. Can’t find shit without a flashlight.\nI should have one in my locker.", 6f);
    }
}
