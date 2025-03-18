using System;
using System.Collections;
using System.Collections.Generic;
using Tobo.Util;
using UnityEngine;
using static CameraPosition;

[Serializable]
public class CameraRotation
{
    public CameraDirection facing;
    public Transform customFacingTarget;
    public List<InspectorTransition> inspectorTransitions;
    [HideInInspector, NonSerialized]
    public CameraPosition position;
    [HideInInspector, NonSerialized]
    public List<CameraTransition> transitions;

    public Vector3 GetForwardVector(Vector3 from)
    {
        if (facing != CameraDirection.Custom)
            return facing.GetOffset();
        else if (customFacingTarget != null)
            return from.Dir(customFacingTarget.position);
        return Vector3.up; // This would look real funny in game - staring straight up
    }

    public Quaternion GetRotation()
    {
        return Quaternion.LookRotation(GetForwardVector(position.position));
    }

    public PosRot GetPosRot() => new PosRot(position.position, GetRotation());

    /// <summary>
    /// Initializes transitions
    /// </summary>
    /// <param name="position"></param>
    public void Init(CameraPosition position)
    {
        this.position = position;

        transitions = new List<CameraTransition>(inspectorTransitions.Count);
        foreach (InspectorTransition inspTransition in inspectorTransitions)
        {
            Vector3 targetPos = position.position;
            if (inspTransition.mode == InspectorTransition.Mode.AnotherPosition && inspTransition.leadsToPosition != null)
                targetPos = inspTransition.leadsToPosition.position;

            Vector3 facingDirection = inspTransition.leadsToRotation.GetOffset();
            if (inspTransition.leadsToRotation == CameraDirection.Custom && inspTransition.customFacingTarget != null)
                facingDirection = targetPos.Dir(inspTransition.customFacingTarget.position);

            Quaternion targetRot = Quaternion.LookRotation(facingDirection);
            transitions.Add(new CameraTransition(inspTransition.directionToClick, targetPos, targetRot,
                inspTransition.moveTrigger, inspTransition.GetMoveTime(), inspTransition.GetRotateTime()));
        }
    }
}
