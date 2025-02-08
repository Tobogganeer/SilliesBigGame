using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Tobo.Util;

/// <summary>
/// Represents a position for the camera to be in. Contains multiple rotations and transitions.
/// </summary>
public class CameraPosition : MonoBehaviour
{
    public List<CameraRotation> rotations;

    public Vector3 position => transform.position;

    private void Start()
    {
        InitTransitions();
    }

    // Initializes from and to positions
    private void InitTransitions()
    {
        foreach (CameraRotation rotation in rotations)
        {
            rotation.position = this;

            foreach (Transition transition in rotation.transitions)
            {
                transition.fromPosition = this;
                transition.fromRotation = rotation;
                // If we are just rotating, the end position is just this
                if (transition.mode == Transition.Mode.AnotherRotation)
                    transition.leadsToPosition = this;
            }
        }
    }


    public bool TryGetRotation(CameraDirection facing, out CameraRotation outRotation, Transform customTarget = null)
    {
        // Search through for matching direction
        if (facing != CameraDirection.Custom)
        {
            foreach (CameraRotation rotation in rotations)
            {
                if (rotation.facing == facing)
                {
                    outRotation = rotation;
                    return true;
                }
            }
        }
        else
        {
            // Search for matching custom target
            if (customTarget == null)
            {
                outRotation = null;
                return false;
            }
            foreach (CameraRotation rotation in rotations)
            {
                // 'facing' should always be Custom for both anyways
                if (rotation.facing == facing && rotation.customFacingTarget == customTarget)
                {
                    outRotation = rotation;
                    return true;
                }
            }
        }

        outRotation = null;
        return false;
    }


    [Serializable]
    public class CameraRotation
    {
        public CameraDirection facing;
        public Transform customFacingTarget;
        public List<Transition> transitions;
        public CameraPosition position;

        public Vector3 GetForwardVector(Vector3 from)
        {
            if (facing != CameraDirection.Custom)
                return facing.GetOffset();
            else if (customFacingTarget != null)
                return from.Dir(customFacingTarget.position);
            return Vector3.up; // This would look real funny in game - staring straight up
        }
    }

    [Serializable]
    public class Transition
    {
        public CameraPosition fromPosition;
        public CameraRotation fromRotation;
        public MoveDirection directionToClick; // What direction we click on-screen to transition
        public CustomMoveTrigger moveTrigger; // If using a custom trigger (e.g. click on a doorway)
        public Mode mode;

        public CameraDirection leadsToRotation;
        public Transform customFacingTarget;
        public CameraPosition leadsToPosition;
        //public bool moveSmoothly = true; // We might want to snap in some cases?
        public float moveTime = 1f;
        public float rotateTime = 0.5f;

        [HideInInspector]
        public bool _foldout = true;

        public Vector3 GetTargetForwardVector()
        {
            if (leadsToRotation == CameraDirection.Custom)
                return customFacingTarget == null ? Vector3.zero :
                    leadsToPosition.position.Dir(customFacingTarget.position);
            else
                return leadsToRotation.GetOffset();
        }

        public enum Mode
        {
            AnotherRotation,
            AnotherPosition
        }
    }

    /// <summary>
    /// Represents a direction the player can click to travel
    /// </summary>
    public enum MoveDirection
    {
        None,
        Left,
        Right,
        Up,
        Down,
        Custom
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.2f);

        const float Offset = 1f;
        const float CustomLookTargetLength = 0.5f;
        const float SmallLocalOffset = 0.02f;

        int stateNum = 0;
        foreach (CameraRotation state in rotations)
        {
            stateNum++;
            UnityEngine.Random.InitState(stateNum);
            // Draw direction itself
            Gizmos.color = state.facing.GetColour();
            Vector3 pos = GetPosition(state.facing, state.customFacingTarget);
            Gizmos.DrawSphere(pos, 0.1f);
            if (state.facing == CameraDirection.Custom && state.customFacingTarget != null)
                Gizmos.DrawLine(pos, pos + pos.Dir(state.customFacingTarget.position) * CustomLookTargetLength);

            // Draw connections
            //Gizmos.color = Color.white;
            for (int i = 0; i < state.transitions.Count; i++)
            {
                Transition trans = state.transitions[i];

                // Small offset to see different connections
                Vector3 locOff = UnityEngine.Random.insideUnitSphere * SmallLocalOffset;

                // Another rotation on this object
                if (trans.mode == Transition.Mode.AnotherRotation)
                {
                    Gizmos.DrawLine(GetPosition(state.facing, state.customFacingTarget) + locOff,
                        GetPosition(trans.leadsToRotation, trans.customFacingTarget) + locOff);
                    Gizmos.DrawWireSphere(GetPosition(trans.leadsToRotation, trans.customFacingTarget) + locOff, 0.13f);
                }
                // Check if the other object has been assigned
                else if (trans.leadsToPosition != null)
                {
                    Vector3 targetPosition = trans.leadsToPosition.transform.position;
                    Vector3 targetOffset = GetOffset(trans.leadsToRotation, trans.customFacingTarget);

                    Gizmos.DrawLine(GetPosition(state.facing, state.customFacingTarget) + locOff, targetPosition + targetOffset);
                    Gizmos.DrawWireSphere(targetPosition + targetOffset, 0.13f);
                }
            }
        }

        Vector3 GetOffset(CameraDirection dir, Transform customFacing)
        {
            if (dir != CameraDirection.Custom)
                return dir.GetOffset() * Offset;
            else if (customFacing != null)
                return transform.position.Dir(customFacing.position) * Offset;
            return Vector3.zero;
        }

        Vector3 GetPosition(CameraDirection dir, Transform customFacing)
        {
            return transform.position + GetOffset(dir, customFacing); 
        }
    }
}

/// <summary>
/// A direction the camera can be facing
/// </summary>
public enum CameraDirection
{
    /// <summary>
    /// Z+
    /// </summary>
    North,
    /// <summary>
    /// X+
    /// </summary>
    East,
    /// <summary>
    /// Z-
    /// </summary>
    South,
    /// <summary>
    /// X-
    /// </summary>
    West,
    /// <summary>
    /// Y+
    /// </summary>
    Up,
    /// <summary>
    /// Y-
    /// </summary>
    Down,
    /// <summary>
    /// Towards a specified object
    /// </summary>
    Custom
}

static class CameraDirectionExtensions
{
    public static Vector3 GetOffset(this CameraDirection direction) => direction switch
    {
        CameraDirection.North => Vector3.forward,
        CameraDirection.East => Vector3.right,
        CameraDirection.South => Vector3.back,
        CameraDirection.West => Vector3.left,
        CameraDirection.Up => Vector3.up,
        CameraDirection.Down => Vector3.down,
        CameraDirection.Custom => Vector3.zero,
        _ => throw new NotImplementedException(),
    };

    public static Color GetColour(this CameraDirection direction) => direction switch
    {
        CameraDirection.North => new Color(0.3f, 0.3f, 0.7f),
        CameraDirection.East => new Color(0.7f, 0.3f, 0.3f),
        CameraDirection.South => new Color(0.2f, 0.2f, 0.8f),
        CameraDirection.West => new Color(0.8f, 0.2f, 0.2f),
        CameraDirection.Up => new Color(0.3f, 0.7f, 0.3f),
        CameraDirection.Down => new Color(0.2f, 0.8f, 0.2f),
        CameraDirection.Custom => new Color(0.8f, 0.2f, 0.8f),
        _ => throw new NotImplementedException(),
    };
}