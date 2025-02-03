#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Tobo.Audio
{
    public class AudioClipPostProcessor : AssetPostprocessor
    {
        public void OnPostprocessAudio(AudioClip clip)
        {

        }

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            
        }
    }
}
#endif