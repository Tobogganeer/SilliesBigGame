using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telemetry
{
    public static void Interaction(GameObject clickedObject)
    {
        TelemetryLogger.Log(null, "Interact", clickedObject.name);
    }

    // TODO: Seperate out move vs rotate
    public static void Move(CameraTransition transition)
    {
        TelemetryLogger.Log(PlayerMovement.instance, "Move", new MovePacket(transition));
    }

    [Serializable]
    public struct MovePacket
    {
        public PosRot from;
        public PosRot to;
        public MoveDirection input; // What we pressed

        // May be null if we aren't moving based on set CameraPositions
        public string fromDirection;
        public string fromFacing;

        public string toDirection;
        public string toFacing;

        public MovePacket(CameraTransition transition)
        {
            from = transition.from;
            to = transition.to;
            input = transition.directionToClick;

            // If our start position is a CameraRotation in the scene, pull its direction and custom facing target
            if (CameraPosition.TryGetRotation(from, out var fromRot))
            {
                fromDirection = fromRot.facing.ToString();
                fromFacing = fromRot.facing == CameraDirection.Custom ? fromRot.customFacingTarget.name : string.Empty;
            }
            else
            {
                fromDirection = string.Empty;
                fromFacing = string.Empty;
            }

            // If our end position is a CameraRotation in the scene, pull its direction and custom facing target
            if (CameraPosition.TryGetRotation(to, out var toRot))
            {
                toDirection = toRot.facing.ToString();
                toFacing = toRot.facing == CameraDirection.Custom ? toRot.customFacingTarget.name : string.Empty;
            }
            else
            {
                toDirection = string.Empty;
                toFacing = string.Empty;
            }
        }
    }

}
