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

    Camera cam;

    CameraPosition currentPosition;
    CameraPosition.CameraRotation currentRotation;

    private void Start()
    {
        cam = Camera.main;
    }

    public void Travel(CameraPosition.Transition transition)
    {

    }

    public void TeleportTo(CameraPosition position, CameraPosition.CameraRotation rotation)
    {
        transform.position = position.transform.position;
        Vector3 lookTarget = rotation.GetForwardVector(position.transform.position);
        cam.transform.LookAt(lookTarget);

        currentPosition = position;
        currentRotation = rotation;
    }
}
