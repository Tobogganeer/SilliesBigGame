using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Only allows interaction when the player is standing at a certain <see cref="CameraPosition"/>
/// </summary>
public class LimitedRangeInteractable : MonoBehaviour
{
    public CameraPosition position;
    public List<Collider> interactables;

    private void Update()
    {
        interactables.ForEach((coll) => coll.enabled = PlayerMovement.instance.CurrentPosRot.position == position.position);
    }
}
