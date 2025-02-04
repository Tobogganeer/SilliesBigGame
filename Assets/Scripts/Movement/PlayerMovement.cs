using System;
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

    Camera cam;

    CameraPosition currentPosition;
    CameraPosition.CameraRotation currentRotation;

    float travelProgress;
    CameraPosition.Transition currentTransition;

    Vector3 fromPosition;
    Vector3 targetPosition;
    float moveTimer;

    Quaternion targetRotation;
    Quaternion fromRotation;
    float rotateTimer;

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
        moveTimer = 0f;
        rotateTimer = 0f;

        fromPosition = transform.position;
        fromRotation = transform.rotation;
        targetPosition = transition.leadsToPosition.position;
        targetRotation = Quaternion.LookRotation(transition.GetTargetForwardVector());

        // TODO: May cause bugs later (setting current position + rot before we move)?
        currentPosition = transition.leadsToPosition;
        if (!transition.fromPosition.TryGetRotation(transition.leadsToRotation, out currentRotation, transition.customFacingTarget))
            throw new NullReferenceException($"Tried traveling to {transition.leadsToPosition.name}" +
                $"=> {transition.leadsToRotation}, but that rotation was null!");
    }

    private void Update()
    {
        // TODO: Increment timers, actually move
    }

    public void TeleportTo(CameraPosition position, CameraPosition.CameraRotation rotation)
    {
        transform.position = position.transform.position;
        Vector3 lookTarget = rotation.GetForwardVector(position.transform.position);
        cam.transform.LookAt(lookTarget);

        currentPosition = position;
        currentRotation = rotation;

        travelProgress = 1f;
        currentTransition = null;
    }
}
