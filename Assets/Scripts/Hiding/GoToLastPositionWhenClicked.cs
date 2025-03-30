using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLastPositionWhenClicked : MonoBehaviour, IInteractable
{
    public void OnClicked()
    {
        PlayerMovement.instance.Travel(PlayerMovement.instance.PreviousPosRot.position, PlayerMovement.instance.PreviousPosRot.rotation);
    }
}
