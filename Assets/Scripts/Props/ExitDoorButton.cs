using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorButton : MonoBehaviour, IInteractable
{
    public GameObject ExitDoor;


    void IInteractable.OnClicked()
    {
        ExitDoor.GetComponent<ExitDoor>().buttonPress();
    }
}
