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
    // Factor from 0 to 1 - returns 1 if we are supposed to move instantly
    float moveFac => currentTransition.moveTime == 0 ? 1f : Mathf.Clamp01(moveTimer / currentTransition.moveTime);

    Quaternion targetRotation;
    Quaternion fromRotation;
    float rotateTimer;
    // Factor from 0 to 1 - returns 1 if we are supposed to rotate instantly
    float rotateFac => currentTransition.rotateTime == 0 ? 1f : Mathf.Clamp01(rotateTimer / currentTransition.rotateTime);

    public bool Travelling => currentTransition != null;
    public float TravelProgress => Mathf.Clamp01(travelProgress);

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

        // Where we are and where we are going
        fromPosition = transform.position;
        fromRotation = transform.rotation;
        targetPosition = transition.leadsToPosition.position;
        targetRotation = Quaternion.LookRotation(transition.GetTargetForwardVector());

        // TODO: May cause bugs later (setting current position + rot before we move)?
        currentPosition = transition.leadsToPosition;
        if (!transition.fromPosition.TryGetRotation(transition.leadsToRotation, out currentRotation, transition.customFacingTarget))
            throw new NullReferenceException($"Tried travelling to {transition.leadsToPosition.name}" +
                $"=> {transition.leadsToRotation}, but that rotation was null!");
    }

    private void Update()
    {
        if (!Travelling)
            return;

        UpdateTimers();

        if (travelProgress >= 1f)
        {
            FinishTravelling();
            return;
        }


    }

    void UpdateTimers()
    {
        moveTimer += Time.deltaTime;
        rotateTimer += Time.deltaTime;

        // Travel progress is the slower of the two
        travelProgress = Mathf.Min(moveFac, rotateFac);
    }

    void FinishTravelling()
    {
        travelProgress = 1f;
        moveTimer = 1e5f; // Set these to be massive so we are definitely done moving
        rotateTimer = 1e5f;

        // Lock to final position
        transform.position = currentPosition.transform.position;
        Vector3 lookTarget = currentRotation.GetForwardVector(currentPosition.position);
        cam.transform.LookAt(lookTarget);

        currentTransition = null;
    }

    public void TeleportTo(CameraPosition position, CameraPosition.CameraRotation rotation)
    {
        currentPosition = position;
        currentRotation = rotation;

        FinishTravelling();
    }
}
