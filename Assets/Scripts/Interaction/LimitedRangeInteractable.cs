using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Only allows interaction when the player is standing at a certain <see cref="CameraPosition"/>
/// </summary>
public class LimitedRangeInteractable : MonoBehaviour
{
    public CameraPosition position;
    public Collider interactable;

    private void Update()
    {
        interactable.enabled = PlayerMovement.instance.CurrentPosRot.position == position.position;
    }
}
