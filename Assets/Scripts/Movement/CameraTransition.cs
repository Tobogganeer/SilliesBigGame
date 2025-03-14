using System.Collections;
using System.Collections.Generic;
using Tobo.Util;
using UnityEngine;

public class CameraTransition
{
    public MoveDirection directionToClick; // What direction we click on-screen to transition
    public CustomMoveTrigger moveTrigger; // If using a custom trigger (e.g. click on a doorway)
    public PosRot from;
    public PosRot to;
    public float moveTime = DefaultMoveTime;
    public float rotateTime = DefaultRotateTime;

    public const float DefaultMoveTime = 0.3f;
    public const float DefaultRotateTime = 0.3f;

    public CameraTransition(PosRot from, PosRot to, float moveTime = DefaultMoveTime, float rotateTime = DefaultRotateTime)
        : this(MoveDirection.None, from, to, null, moveTime, rotateTime) { }

    public CameraTransition(MoveDirection direction, PosRot from, PosRot to, CustomMoveTrigger customTrigger = null, float moveTime = DefaultMoveTime, float rotateTime = DefaultRotateTime)
    {
        directionToClick = direction;
        this.from = from;
        this.to = to;
        this.moveTrigger = customTrigger;
        this.moveTime = DefaultMoveTime;
        this.rotateTime = DefaultRotateTime;
    }

    public CameraTransition(MoveDirection direction, Vector3 fromPosition, Quaternion fromRotation, Vector3 targetPosition, Quaternion targetRotation,
        CustomMoveTrigger customTrigger = null, float moveTime = DefaultMoveTime, float rotateTime = DefaultRotateTime)
        : this(direction, new PosRot(fromPosition, fromRotation), new PosRot(targetPosition, targetRotation), customTrigger, moveTime, rotateTime) { }

    public CameraTransition(MoveDirection direction, Vector3 targetPosition, Quaternion targetRotation,
        CustomMoveTrigger customTrigger = null, float moveTime = DefaultMoveTime, float rotateTime = DefaultRotateTime)
    : this(direction, new PosRot(targetPosition, targetRotation), new PosRot(targetPosition, targetRotation), customTrigger, moveTime, rotateTime) { }

    public CameraTransition(MoveDirection direction, Vector3 targetPosition, Vector3 lookAt,
        CustomMoveTrigger customTrigger = null, float moveTime = DefaultMoveTime, float rotateTime = DefaultRotateTime)
        : this(direction, targetPosition, Quaternion.LookRotation(targetPosition.Dir(lookAt)), customTrigger, moveTime, rotateTime) { }
}
