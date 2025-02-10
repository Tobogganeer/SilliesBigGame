using System.Collections;
using System.Collections.Generic;
using Tobo.Audio;
using UnityEngine;

public class LockedDoor : MonoBehaviour, IInteractable
{
    public CustomMoveTrigger moveTrigger;

    bool locked = true;

    public void OnClicked()
    {
        if (!locked)
            return;

        if (Inventory.HasItem("Door Keys"))
        {
            locked = false;
            Inventory.ConsumeItem("Door Keys");
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
