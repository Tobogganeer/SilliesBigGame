using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(CameraPosition.Transition))]
public class TransitionPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect pos, SerializedProperty property, GUIContent label)
    {
        //base.OnGUI(position, property, label);
        EditorGUI.BeginProperty(pos, label, property);

        // Draw label
        //pos = EditorGUI.PrefixLabel(pos, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        float line = EditorGUIUtility.singleLineHeight;

        float ogLabelWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 150f;
        Rect rect = new Rect(pos.x, 0, pos.width, line);

        SerializedProperty dirProp = Prop("directionToClick");
        EditorGUI.PropertyField(Increase(ref rect), dirProp);

        var moveDir = (CameraPosition.MoveDirection)dirProp.intValue;
        if (moveDir == CameraPosition.MoveDirection.Custom)
            EditorGUI.PropertyField(Increase(ref rect), Prop("moveTrigger"), Label("Custom Move Trigger"));

        SerializedProperty modeProp = Prop("mode");
        EditorGUI.PropertyField(Increase(ref rect), modeProp, Label("Leads to"));

        // Add a space
        rect.y += line;

        var moveMode = (CameraPosition.Transition.Mode)modeProp.intValue;
        if (moveMode == CameraPosition.Transition.Mode.AnotherPosition)
            EditorGUI.PropertyField(Increase(ref rect), Prop("leadsToPosition"), Label("New CameraPosition"));
        EditorGUI.PropertyField(Increase(ref rect), Prop("leadsToRotation"), Label("New Rotation"));
        EditorGUI.PropertyField(Increase(ref rect), Prop("moveSmoothly"));
        EditorGUI.PropertyField(Increase(ref rect), Prop("moveSpeedMultiplier"));

        //if ()


        EditorGUIUtility.labelWidth = ogLabelWidth;

        /*
        
        public MoveDirection directionToClick; // What direction we click on-screen to transition
        public CustomMoveTrigger moveTrigger; // If using a custom trigger (e.g. click on a doorway)
        public Mode mode;

        public CameraDirection leadsToRotation;
        public CameraPosition leadsToPosition;
        public bool moveSmoothly = true; // We might want to snap in some cases?
        public float moveSpeedMultiplier = 1f;

        */

        // Calculate rects
        /*
        var amountRect = new Rect(position.x, position.y, 30, position.height);
        var unitRect = new Rect(position.x + 35, position.y, 50, position.height);
        var nameRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);

        // Draw fields - pass GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("amount"), GUIContent.none);
        EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("unit"), GUIContent.none);
        EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);
        */

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();

        SerializedProperty Prop(string name) => property.FindPropertyRelative("directionToClick");
        GUIContent Label(string text) => new GUIContent(text);
        Rect Increase(ref Rect rect)
        {
            Rect prev = rect;
            rect.y += EditorGUIUtility.singleLineHeight;
            return prev;
        }
    }
}
