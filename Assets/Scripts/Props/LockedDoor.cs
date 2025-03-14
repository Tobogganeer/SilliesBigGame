using System.Collections;
using System.Collections.Generic;
using Tobo.Audio;
using UnityEngine;

public class LockedDoor : MonoBehaviour, IInteractable
{
    public GameObject setActiveWhenUnlocked;
    public bool enableObject = true;
    public string itemIDToOpen;

    bool locked = true;

    public void OnClicked()
    {
        if (!locked)
            return;

        if (Inventory.HasItem(itemIDToOpen))
        {
            locked = false;
            Inventory.ConsumeItem(itemIDToOpen);
            setActiveWhenUnlocked.SetActive(enableObject);
            Sound.KeyUse.PlayDirect();
        }
        else
        {
            PopUp.Show("Locked...", 1f);
            Sound.DoorLocked.PlayAtPosition(transform.position);
        }
    }
}
