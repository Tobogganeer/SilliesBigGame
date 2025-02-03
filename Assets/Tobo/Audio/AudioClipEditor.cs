#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Tobo.Audio.Editor
{
    [CustomEditor(typeof(AudioClip))]
    public class AudioClipEditor : UnityEditor.Editor
    {
        AudioClip targetClip;

        private void OnEnable()
        {
            targetClip = (AudioClip)target;
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            GUILayout.Button("Test");
        }
    }
}
#endif