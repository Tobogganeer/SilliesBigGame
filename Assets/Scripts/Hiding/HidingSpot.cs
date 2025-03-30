using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CameraPosition))]
public class HidingSpot : MonoBehaviour
{
    public static List<HidingSpot> all = new List<HidingSpot>();

    // TODO: Make it so you are considered "hidden" while you are travelling to a hiding spot?
    public static bool IsPlayerHidden => all.Any((spot) => PlayerMovement.instance.CurrentPosRot.position == spot.transform.position);

    private void OnEnable()
    {
        all.Add(this);
    }

    private void OnDisable()
    {
        all.Remove(this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }
}
