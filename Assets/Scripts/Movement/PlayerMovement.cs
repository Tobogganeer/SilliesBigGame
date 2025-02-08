using System;
using System.Collections;
using System.Collections.Generic;
using Tobo.Util;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    private void Awake()
    {
        instance = this;
    }

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

    public void Travel(CameraPosition.Transition transition, bool interruptCurrentTravel = false)
    {
        Travel(transition.leadsToPosition.position, Quaternion.LookRotation(transition.GetTargetForwardVector()));
    }

    public void Travel(Vector3 position, Vector3 lookTarget, float moveTime = 1.0f, float rotateTime = 0.5f, bool interruptCurrentTravel = false)
    {
        Travel(position, Quaternion.LookRotation(position.Dir(lookTarget)), moveTime, rotateTime, interruptCurrentTravel);
    }

    public void Travel(Vector3 position, Quaternion rotation, float moveTime = 1.0f, float rotateTime = 0.5f, bool interruptCurrentTravel = false)
    {
        // Don't interupt if we are travelling currently
        if (currentTransition != null && !interruptCurrentTravel)
            return;

        // Assume our current position is fine
        currentTransition = new CameraPosition.Transition(moveTime, rotateTime);
        travelProgress = 0f;
        moveTimer = 0f;
        rotateTimer = 0f;

        // Where we are and where we are going
        fromPosition = transform.position;
        fromRotation = transform.rotation;
        targetPosition = position;
        targetRotation = rotation;
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

        float easedMoveFac = Ease.SmoothStep3(moveFac);
        float easedRotateFac= Ease.SmoothStop3(rotateFac);

        transform.position = Vector3.Lerp(fromPosition, targetPosition, easedMoveFac);
        transform.rotation = Quaternion.Slerp(fromRotation, targetRotation, easedRotateFac);
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
        transform.position = targetPosition;
        transform.rotation = targetRotation;

        currentTransition = null;
    }

    public void TeleportTo(CameraPosition position, CameraPosition.CameraRotation rotation)
    {
        TeleportTo(position.position, Quaternion.LookRotation(rotation.GetForwardVector(position.position)));
    }

    public void TeleportTo(Vector3 position, Vector3 lookTarget)
    {
        TeleportTo(position, Quaternion.LookRotation(position.Dir(lookTarget)));
    }

    public void TeleportTo(Vector3 position, Quaternion rotation)
    {
        targetPosition = position;
        targetRotation = rotation;

        FinishTravelling();
    }
}
