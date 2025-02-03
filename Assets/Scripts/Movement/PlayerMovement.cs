using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static PlayerMovement instance;
    private void Awake()
    {
        instance = this;
    }

    public void Travel(CameraPosition.Transition transition)
    {

    }

    public void TeleportTo(CameraPosition position, CameraPosition.CameraRotation rotation)
    {
        transform.position = position.transform.position;
    }
}
