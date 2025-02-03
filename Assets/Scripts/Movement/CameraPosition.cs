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
