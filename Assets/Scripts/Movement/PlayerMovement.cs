using System;
using System.Collections;
using System.Collections.Generic;
using Tobo.Audio;
using Tobo.Util;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    private void Awake()
    {
        instance = this;
    }

    public CameraPosition startingPosition;
    public CameraDirection startingDirection;

    private void Start()
    {
        TeleportTo(startingPosition.GetRotation(startingDirection));
    }


    float travelProgress;
    CameraPosition.Transition currentTransition;

    float moveTimer;
    float rotateTimer;

    // Factor from 0 to 1 - returns 1 if we are supposed to move/rotate instantly
    float moveFac => currentTransition.moveTime == 0 ? 1f : Mathf.Clamp01(moveTimer / currentTransition.moveTime);
    float rotateFac => currentTransition.rotateTime == 0 ? 1f : Mathf.Clamp01(rotateTimer / currentTransition.rotateTime);

    Vector3 fromPosition => currentTransition == null ? transform.position : currentTransition.from.position;
    Vector3 targetPosition => currentTransition == null ? transform.position : currentTransition.to.position;
    Quaternion fromRotation => currentTransition == null ? transform.rotation : currentTransition.from.rotation;
    Quaternion targetRotation => currentTransition == null ? transform.rotation : currentTransition.to.rotation;

    public bool Travelling => currentTransition != null;
    public float TravelProgress => Mathf.Clamp01(travelProgress);
    public PosRot CurrentPosRot => new PosRot(transform.position, transform.rotation);


    public void Travel(CameraPosition.Transition transition, bool interruptCurrentTravel = false)
    {
        // Don't interupt if we are travelling currently
        if (Travelling && !interruptCurrentTravel)
            return;

        // Assume our current position is fine
        currentTransition = transition;
        currentTransition.from = CurrentPosRot; // Manually override "from"
        if (currentTransition.from.position == currentTransition.to.position)
            currentTransition.moveTime = 0f; // Start and end at the same place - we aren't moving
        travelProgress = 0f;
        moveTimer = 0f;
        rotateTimer = 0f;

        // Disable buttons
        MovementUI.ClearUI();
        Sound.TurnStep_moveCamIDK_.PlayDirect();
    }

    public void Travel(Vector3 position, Vector3 lookTarget, float moveTime = 1.0f, float rotateTime = 0.5f, bool interruptCurrentTravel = false)
    {
        Travel(new CameraPosition.Transition(new PosRot(transform.position, transform.rotation), new PosRot(position, lookTarget), moveTime, rotateTime), interruptCurrentTravel);
    }

    public void Travel(Vector3 position, Quaternion rotation, float moveTime = 1.0f, float rotateTime = 0.5f, bool interruptCurrentTravel = false)
    {
        Travel(new CameraPosition.Transition(new PosRot(transform.position, transform.rotation), new PosRot(position, rotation), moveTime, rotateTime), interruptCurrentTravel);
    }

    public void Travel(CameraPosition position, CameraDirection direction, Transform customTarget = null, float moveTime = 1.0f, float rotateTime = 0.5f, bool interruptCurrentTravel = false)
    {
        Travel(new CameraPosition.Transition(new PosRot(transform.position, transform.rotation),
            new PosRot(position.position, position.GetRotation(direction, customTarget).GetRotation()),
            moveTime, rotateTime), interruptCurrentTravel);
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
        float easedRotateFac= Ease.SmoothStep3(rotateFac);

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

        // Update the current UI buttons
        MovementUI.SetUI(CurrentPosRot);
    }

    public void TeleportTo(CameraPosition.CameraRotation rotation)
    {
        TeleportTo(rotation.position.position, Quaternion.LookRotation(rotation.GetForwardVector(rotation.position.position)));
    }

    public void TeleportTo(Vector3 position, Vector3 lookTarget)
    {
        TeleportTo(position, Quaternion.LookRotation(position.Dir(lookTarget)));
    }

    public void TeleportTo(Vector3 position, Quaternion rotation)
    {
        currentTransition = new CameraPosition.Transition(new PosRot(position, rotation), new PosRot(position, rotation));

        FinishTravelling();
    }
}
