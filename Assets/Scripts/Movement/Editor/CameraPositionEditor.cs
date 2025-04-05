// This seems to break the custom InspectorTransition inspector - only enable it when you need to
// TODO: make this into a ContextMenu item or something?
//#define ENABLED

#if ENABLED
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraPosition))]
public class CameraPositionEditor : Editor
{
    SerializedObject serTarget;
    CameraPosition targetPos;

    private void OnEnable()
    {
        serTarget = new SerializedObject(target);
        targetPos = (CameraPosition)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (targetPos.rotations.Count == 0 && GUILayout.Button("Add default rotations"))
        {
            targetPos.rotations.Add(Create(CameraDirection.North, CameraDirection.West, CameraDirection.East));
            targetPos.rotations.Add(Create(CameraDirection.East, CameraDirection.North, CameraDirection.South));
            targetPos.rotations.Add(Create(CameraDirection.South, CameraDirection.East, CameraDirection.West));
            targetPos.rotations.Add(Create(CameraDirection.West, CameraDirection.South, CameraDirection.North));
            serTarget.ApplyModifiedProperties();
        }
    }

    CameraRotation Create(CameraDirection facing, CameraDirection left, CameraDirection right)
    {
        CameraRotation rot = new CameraRotation { facing = facing };
        rot.inspectorTransitions = new List<CameraPosition.InspectorTransition>
        {
            new CameraPosition.InspectorTransition { directionToClick = MoveDirection.Left, leadsToRotation = left },
            new CameraPosition.InspectorTransition { directionToClick = MoveDirection.Right, leadsToRotation = right }
        };
        return rot;
    }
}
#endif
