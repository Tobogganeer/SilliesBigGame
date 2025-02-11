using System.Collections;
using System.Collections.Generic;
using Tobo.Audio;
using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    public string itemID;
    public bool showPickedUpMessage = true;
    public Sound pickupSound;

    public void OnClicked()
    {
        Inventory.GiveItem(itemID, showPickedUpMessage, pickupSound);

        Destroy(gameObject);
    }
}
