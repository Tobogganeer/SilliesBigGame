using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorButton : MonoBehaviour, IInteractable
{
    public ExitDoor exitDoor;


    void IInteractable.OnClicked()
    {
        exitDoor.UnlockButtonPressed();
    }
}
