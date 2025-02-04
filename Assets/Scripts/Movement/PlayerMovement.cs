using System.Collections;
using System.Collections.Generic;
using Tobo.Util;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static PlayerMovement instance;
    private void Awake()
    {
        instance = this;
    }

    public float rotateTime = 0.5f;
    public float moveTime = 1f;

    Camera cam;

    CameraPosition currentPosition;
    CameraPosition.CameraRotation currentRotation;

    float travelProgress;
    float travelSpeedMultiplier;
    CameraPosition.Transition currentTransition;

    Vector3 fromPosition;
    Quaternion fromRotation;
    
    Vector3 targetPosition;
    Quaternion targetRotation;

    public bool Travelling => currentTransition != null;
    public float TravelProgress => travelProgress;

    private void Start()
    {
        cam = Camera.main;
    }

    public void Travel(CameraPosition.Transition transition, bool interruptCurrentTravel = false)
    {
        // Don't interupt if we are travelling currently
        if (currentTransition != null && !interruptCurrentTravel)
            return;

        // Assume our current position is fine
        currentTransition = transition;
        travelProgress = 0f;
        travelSpeedMultiplier = transition.moveSpeedMultiplier;

        fromPosition = transform.position;
        fromRotation = transform.rotation;
        targetPosition = transition.leadsToPosition.position;
        targetRotation = Quaternion.LookRotation(transition.GetTargetForwardVector());
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
