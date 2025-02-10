using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static CameraPosition.InspectorTransition;

[CustomPropertyDrawer(typeof(CameraPosition.InspectorTransition))]
public class InspectorTransitionPropertyDrawer : PropertyDrawer
{
    // This function is terrible
    public override void OnGUI(Rect pos, SerializedProperty property, GUIContent label)
    {
        //base.OnGUI(position, property, label);
        EditorGUI.BeginProperty(pos, label, property);

        SerializedProperty dirProp = Prop("directionToClick");
        SerializedProperty modeProp = Prop("mode");
        SerializedProperty toPosition = Prop("leadsToPosition");
        SerializedProperty toRotation = Prop("leadsToRotation");
        SerializedProperty foldout = Prop("_foldout");
        SerializedProperty customTarget = Prop("customFacingTarget");

        // SpeedMode
        SerializedProperty moveTimeMode = Prop("moveTimeMode");
        SerializedProperty rotateTimeMode = Prop("rotateTimeMode");
        SerializedProperty moveTime = Prop("moveTime");
        SerializedProperty rotateTime = Prop("rotateTime");

        var moveDir = (CameraPosition.MoveDirection)dirProp.intValue;
        var moveMode = (Mode)modeProp.intValue;

        var endCameraDir = (CameraDirection)toRotation.intValue;

        float line = EditorGUIUtility.singleLineHeight;

        float ogLabelWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 100f;

        string labelText;
        string camDirText;
        // Get special string if we are aiming at a custom target
        if (endCameraDir == CameraDirection.Custom)
        {
            Transform target = customTarget.objectReferenceValue as Transform;
            camDirText = "Look at \"" + (target != null ? target.name : "(unset)") + "\"";
        }
        else
            camDirText = endCameraDir.ToString();

        if (moveMode == Mode.AnotherPosition)
        {
            CameraPosition to = toPosition.objectReferenceValue as CameraPosition;
            labelText = (to != null ? to.name : "(unset)") + " > " + camDirText;
        }
        else
            labelText = camDirText;

        foldout.boolValue = EditorGUI.Foldout(new Rect(pos.x, 0, pos.width, line), foldout.boolValue, new GUIContent(labelText));

        var indent = EditorGUI.indentLevel;
        pos.y += line;

        if (foldout.boolValue)
        {
            // Draw label
            //pos = EditorGUI.PrefixLabel(pos, GUIUtility.GetControlID(FocusType.Passive), new GUIContent(labelText));

            // Don't make child fields be indented
            EditorGUI.indentLevel = 0;

            EditorGUIUtility.labelWidth = 150f;
            Rect rect = new Rect(pos.x, line, pos.width, line);

            EditorGUI.PropertyField(Increase(ref rect), dirProp);

            if (moveDir == CameraPosition.MoveDirection.Custom)
                EditorGUI.PropertyField(Increase(ref rect), Prop("moveTrigger"), Label("Custom Move Trigger"));

            EditorGUI.PropertyField(Increase(ref rect), modeProp, Label("Leads to"));

            // Add a space
            rect.y += line;

            if (moveMode == Mode.AnotherPosition)
                EditorGUI.PropertyField(Increase(ref rect), toPosition, Label("New CameraPosition"));
            EditorGUI.PropertyField(Increase(ref rect), toRotation, Label("New Rotation"));
            if (endCameraDir == CameraDirection.Custom)
                EditorGUI.PropertyField(Increase(ref rect), customTarget, Label("Look towards"));
            //EditorGUI.PropertyField(Increase(ref rect), Prop("moveSmoothly"));
            //EditorGUI.PropertyField(Increase(ref rect), Prop("moveTime"));
            //EditorGUI.PropertyField(Increase(ref rect), Prop("rotateTime"));
            EditorGUI.PropertyField(Increase(ref rect), moveTimeMode, Label("Move Time"));
            TimeMode mtMode = (TimeMode)moveTimeMode.intValue;
            switch (mtMode)
            {
                case TimeMode.Default:
                    moveTime.floatValue = CameraPosition.Transition.DefaultMoveTime;
                    break;
                case TimeMode.Instant:
                    moveTime.floatValue = 0f;
                    break;
                case TimeMode.Custom:
                    EditorGUI.PropertyField(Increase(ref rect), moveTime, Label("Custom Move Time"));
                    break;
            }

            EditorGUI.PropertyField(Increase(ref rect), rotateTimeMode, Label("Rotate Time"));
            TimeMode rtMode = (TimeMode)rotateTimeMode.intValue;
            switch (rtMode)
            {
                case TimeMode.Default:
                    rotateTime.floatValue = CameraPosition.Transition.DefaultRotateTime;
                    break;
                case TimeMode.Instant:
                    rotateTime.floatValue = 0f;
                    break;
                case TimeMode.Custom:
                    EditorGUI.PropertyField(Increase(ref rect), rotateTime, Label("Custom Rotate Time"));
                    break;
            }
            //EditorGUI.PropertyField(Increase(ref rect), Prop("moveInstantly"));
            //EditorGUI.PropertyField(Increase(ref rect), Prop("rotateInstantly"));


        }

        EditorGUIUtility.labelWidth = ogLabelWidth;

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();

        SerializedProperty Prop(string name) => property.FindPropertyRelative(name);
        GUIContent Label(string text) => new GUIContent(text);
        Rect Increase(ref Rect rect)
        {
            Rect prev = rect;
            rect.y += EditorGUIUtility.singleLineHeight;
            return prev;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float line = EditorGUIUtility.singleLineHeight;
        if (!property.FindPropertyRelative("_foldout").boolValue)
            return line;

        float height = line * 7; // 5 + a space + foldout

        SerializedProperty facingProp = property.FindPropertyRelative("leadsToRotation");
        var facing = (CameraDirection)facingProp.intValue;
        if (facing == CameraDirection.Custom)
            height += line;

        SerializedProperty dirProp = property.FindPropertyRelative("directionToClick");
        var moveDir = (CameraPosition.MoveDirection)dirProp.intValue;
        if (moveDir == CameraPosition.MoveDirection.Custom)
            height += line;

        SerializedProperty modeProp = property.FindPropertyRelative("mode");
        var moveMode = (Mode)modeProp.intValue;
        if (moveMode == Mode.AnotherPosition)
            height += line;

        SerializedProperty moveTimeModeProp = property.FindPropertyRelative("moveTimeMode");
        var moveTimeMode = (TimeMode)moveTimeModeProp.intValue;
        if (moveTimeMode == TimeMode.Custom)
            height += line;

        SerializedProperty rotateTimeModeProp = property.FindPropertyRelative("rotateTimeMode");
        var rotateTimeMode = (TimeMode)rotateTimeModeProp.intValue;
        if (rotateTimeMode == TimeMode.Custom)
            height += line;

        return height;
    }
}
