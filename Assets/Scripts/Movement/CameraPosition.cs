using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Represents a position for the camera to be in. Contains multiple rotations and transitions.
/// </summary>
public class CameraPosition : MonoBehaviour
{
    public List<CameraRotation> rotations;

    [Serializable]
    public class CameraRotation
    {
        public CameraDirection facing;
        public List<Transition> transitions;
    }

    [Serializable]
    public class Transition
    {
        public MoveDirection directionToClick; // What direction we click on-screen to transition
        public CustomMoveTrigger moveTrigger; // If using a custom trigger (e.g. click on a doorway)
        public Mode mode;

        public CameraDirection leadsToRotation;
        public CameraPosition leadsToPosition;
        public bool moveSmoothly = true; // We might want to snap in some cases?
        public float moveSpeedMultiplier = 1f;

        [HideInInspector]
        public bool _foldout = true;

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

        foreach (CameraRotation state in rotations)
        {
            // Draw direction itself
            Gizmos.color = state.facing.GetColour();
            Gizmos.DrawSphere(GetPosition(state.facing), 0.1f);

            // Draw connections
            //Gizmos.color = Color.white;
            foreach (Transition trans in state.transitions)
            {
                // Another rotation on this object
                if (trans.mode == Transition.Mode.AnotherRotation)
                {
                    Gizmos.DrawLine(GetPosition(state.facing), GetPosition(trans.leadsToRotation));
                    Gizmos.DrawWireSphere(GetPosition(trans.leadsToRotation), 0.13f);
                }
                // Check if the other object has been assigned
                else if (trans.leadsToPosition != null)
                {
                    Vector3 targetPosition = trans.leadsToPosition.transform.position;
                    Vector3 targetOffset = trans.leadsToRotation.GetOffset() * Offset;

                    Gizmos.DrawLine(GetPosition(state.facing), targetPosition + targetOffset);
                    Gizmos.DrawWireSphere(targetPosition + targetOffset, 0.13f);
                }
            }
        }

        Vector3 GetPosition(CameraDirection dir) => transform.position + dir.GetOffset() * Offset;
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
    Down
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
        _ => throw new NotImplementedException(),
    };
}