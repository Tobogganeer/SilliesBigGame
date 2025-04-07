using System.Collections;
using System.Collections.Generic;
using Tobo.Audio;
using UnityEngine;

public class LockedDoor : MonoBehaviour, IInteractable
{
    public GameObject setActiveWhenUnlocked;
    public bool enableObject = true;
    public string itemIDToOpen;
    public bool disableThisObjectWhenUnlocked = false;

    [Space]
    public Sound lockedSound;
    public Sound unlockSound;
    public string lockedMessage = "Locked...";
    public float lockedMessageTime = 1f;

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
            if (unlockSound != null)
                unlockSound.PlayDirect();
            if (disableThisObjectWhenUnlocked)
                gameObject.SetActive(false);
            //Sound.KeyUse.PlayDirect();
        }
        else
        {
            PopUp.Show(lockedMessage, lockedMessageTime);
            if (lockedSound != null)
                lockedSound.PlayAtPosition(transform.position);
            //Sound.DoorLocked.PlayAtPosition(transform.position);
        }
    }
}
