using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Tobo.Util;
using Tobo.Attributes;

/// <summary>
/// Represents a position for the camera to be in. Contains multiple rotations and transitions.
/// </summary>
public class CameraPosition : MonoBehaviour
{
    public List<CameraRotation> rotations;
    static Dictionary<PosRot, CameraRotation> posRotToRotation = new Dictionary<PosRot, CameraRotation>();

    public Vector3 position => transform.position;

    private void Awake()
    {
        foreach (CameraRotation rotation in rotations)
        {
            rotation.Init(this);
            posRotToRotation.Add(rotation.GetPosRot(), rotation);
        }
    }

    public CameraRotation GetRotation(CameraDirection facing, Transform customTarget = null)
    {
        return TryGetRotation(facing, out CameraRotation rotation, customTarget) ? rotation : null;
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

    public static bool TryGetRotation(PosRot location, out CameraRotation outRotation) => posRotToRotation.TryGetValue(location, out outRotation);

    public static bool TryGetTransitions(PosRot location, out List<CameraTransition> transitions)
    {
        if (TryGetRotation(location, out CameraRotation rot))
        {
            transitions = rot.transitions;
            return true;
        }

        transitions = null;
        return false;
    }

    [Serializable]
    public class InspectorTransition
    {
        public MoveDirection directionToClick; // What direction we click on-screen to transition
        public Mode mode;
        public CameraDirection leadsToRotation;
        public Transform customFacingTarget;
        public CameraPosition leadsToPosition;

        public CustomMoveTrigger moveTrigger; // If using a custom trigger (e.g. click on a doorway)
        //public float moveTime = 1f;
        //public float rotateTime = 0.5f;
        public TimeMode moveTimeMode;
        public TimeMode rotateTimeMode;
        public float moveTime;
        public float rotateTime;

        public bool _foldout = true;
        //public bool _overrideTimes

        public float GetMoveTime() => moveTimeMode switch
        {
            TimeMode.Default => CameraTransition.DefaultMoveTime,
            TimeMode.Instant => 0f,
            TimeMode.Custom => moveTime,
            _ => throw new NotImplementedException(),
        };

        public float GetRotateTime() => rotateTimeMode switch
        {
            TimeMode.Default => CameraTransition.DefaultRotateTime,
            TimeMode.Instant => 0f,
            TimeMode.Custom => rotateTime,
            _ => throw new NotImplementedException(),
        };

        public enum Mode
        {
            AnotherRotation,
            AnotherPosition
        }

        public enum TimeMode
        {
            Default,
            Instant,
            Custom,
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.2f);

        const float Offset = 1f;
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
                Gizmos.DrawLine(pos, pos + pos.DirectionTo_NoNormalize(state.customFacingTarget.position));

            // Draw connections
            //Gizmos.color = Color.white;
            for (int i = 0; i < state.inspectorTransitions.Count; i++)
            {
                InspectorTransition trans = state.inspectorTransitions[i];

                // Small offset to see different connections
                Vector3 locOff = UnityEngine.Random.insideUnitSphere * SmallLocalOffset;

                // Another rotation on this object
                if (trans.mode == InspectorTransition.Mode.AnotherRotation)
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

public struct PosRot
{
    public Vector3 position;
    public Quaternion rotation;

    public PosRot(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }

    public PosRot(Vector3 position, Vector3 lookAt)
    {
        this.position = position;
        this.rotation = Quaternion.LookRotation(position.Dir(lookAt));
    }
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