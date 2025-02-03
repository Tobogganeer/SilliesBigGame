using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(CameraPosition.Transition))]
public class TransitionPropertyDrawer : PropertyDrawer
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

        var moveDir = (CameraPosition.MoveDirection)dirProp.intValue;
        var moveMode = (CameraPosition.Transition.Mode)modeProp.intValue;

        var endCameraDir = (CameraDirection)toRotation.intValue;

        float line = EditorGUIUtility.singleLineHeight;

        float ogLabelWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 100f;

        string labelText;
        if (moveMode == CameraPosition.Transition.Mode.AnotherPosition)
        {
            CameraPosition to = toPosition.objectReferenceValue as CameraPosition;
            labelText = (to != null ? to.name : "(unset)") + " > " + endCameraDir;
        }
        else
            labelText = endCameraDir.ToString();

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

            if (moveMode == CameraPosition.Transition.Mode.AnotherPosition)
                EditorGUI.PropertyField(Increase(ref rect), toPosition, Label("New CameraPosition"));
            EditorGUI.PropertyField(Increase(ref rect), toRotation, Label("New Rotation"));
            EditorGUI.PropertyField(Increase(ref rect), Prop("moveSmoothly"));
            EditorGUI.PropertyField(Increase(ref rect), Prop("moveSpeedMultiplier"));

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

        SerializedProperty dirProp = property.FindPropertyRelative("directionToClick");
        var moveDir = (CameraPosition.MoveDirection)dirProp.intValue;
        if (moveDir == CameraPosition.MoveDirection.Custom)
            height += line;

        SerializedProperty modeProp = property.FindPropertyRelative("mode");
        var moveMode = (CameraPosition.Transition.Mode)modeProp.intValue;
        if (moveMode == CameraPosition.Transition.Mode.AnotherPosition)
            height += line;

        return height;
    }
}
