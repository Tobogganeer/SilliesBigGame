using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToPositionWhenClicked : MonoBehaviour, IInteractable
{
    public CameraPosition camPos;

    public void OnClicked()
    {
        PlayerMovement.instance.Travel(camPos);
    }
}
