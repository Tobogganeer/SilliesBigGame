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
        Inventory.GiveItem(itemID);
        if (showPickedUpMessage)
            PopUp.Show("Picked up " + itemID, 3f);

        if (pickupSound != null)
            pickupSound.Play2D();

        Destroy(gameObject);
    }
}
