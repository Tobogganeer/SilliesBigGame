using System.Collections;
using System.Collections.Generic;
using Tobo.Audio;
using UnityEngine;

public class Closet : MonoBehaviour, IInteractable
{
    public void OnClicked()
    {
        if (Inventory.HasItem("Charged Flashlight"))
            Inventory.GiveItem("Door Keys", true, Sound.GetKey);
        else
            PopUp.Show("Lights are dead. Of course. Can’t find shit without a flashlight.\nI should have one in my locker.", 6f);
    }
}
