using System.Collections;
using System.Collections.Generic;
using Tobo.Audio;
using UnityEngine;

public class LockedDoor : MonoBehaviour, IInteractable
{
    public CustomMoveTrigger moveTrigger;
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
            moveTrigger.gameObject.SetActive(true);
            Sound.KeyUse.Play2D();
        }
        else
        {
            PopUp.Show("Locked...", 1f);
            Sound.DoorLocked.Play(transform.position);
        }
    }
}
