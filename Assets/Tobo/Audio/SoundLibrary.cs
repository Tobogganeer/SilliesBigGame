using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using Tobo.Util.Editor;
#endif

namespace Tobo.Audio
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Sound Library")]
    public class SoundLibrary : ScriptableObject
    {
        // Doing this because I was getting some weird serialization bugs when
        //  AudioManager just had a Sound[]
        // Probably because it was in a prefab in resources or smth, idk

        [Header("Fill through Menu")]
        public Sound[] sounds;

#if UNITY_EDITOR
        [MenuItem("Audio/Update current Sounds")]
        public static void FillSounds()
        {
            SoundLibrary lib = LibraryUtil.FillLibrary<SoundLibrary, Sound>(nameof(SoundLibrary.sounds));
            AudioCodegen.Generate(lib);
        }
#endif
    }
}
