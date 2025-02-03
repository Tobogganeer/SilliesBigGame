using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tobo.Audio
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Sound")]
    public partial class Sound : ScriptableObject
    {
        [SerializeField] private ID soundID;
        [SerializeField] private AudioClip[] clips;
        [SerializeField] private float maxDistance = 35f;
        [SerializeField] private AudioCategory category = AudioCategory.SFX;
        [SerializeField] private float volume = 1.0f;
        [SerializeField] private float minPitch = 0.85f;
        [SerializeField] private float maxPitch = 1.1f;
        [SerializeField] private bool is2d = false;

        public ID SoundID => soundID;
        public AudioClip[] Clips => clips;
        public float MaxDistance => maxDistance;
        public AudioCategory Category => category;
        public float Volume => volume;
        public float MinPitch => minPitch;
        public float MaxPitch => maxPitch;
        public bool Is2d => is2d;

        public static Sound From(ID id)
        {
            return AudioManager.GetSound(id);
        }

        public static Audio Override(ID id)
        {
            return From(id).Override();
        }

        public Audio Override()
        {
            return GetAudio();
        }

        public Audio GetAudio()
        {
            return new Audio(this);
        }

        //public static bool Exists(ID id) => AudioManager.SoundExists(id);

        #region Play
        public void Play(Vector3 position)
        {
            AudioManager.Play(this, position);
        }

        public void Play2D()
        {
            AudioManager.Play2D(this);
        }

        public void PlayLocal(Vector3 position)
        {
            AudioManager.PlayLocal(this, position);
        }

        public void PlayLocal2D()
        {
            AudioManager.PlayLocal2D(this);
        }
        #endregion

        internal static Sound CreateInternal(List<AudioClip> clips, bool is2D, AudioCategory category)
        {
            Sound s = CreateInstance<Sound>();

            s.clips = clips.ToArray();
            s.is2d = is2D;
            s.category = category;

            return s;
        }
    }

    public static class SoundIDExtensions
    {
        public static Audio Override(this Sound.ID id)
        {
            return Sound.Override(id);
        }

        public static void Play(this Sound.ID id, Vector3 position)
        {
            AudioManager.Play(id, position);
        }

        public static void Play2D(this Sound.ID id)
        {
            AudioManager.Play2D(id);
        }

        public static void PlayLocal(this Sound.ID id, Vector3 position)
        {
            AudioManager.PlayLocal(id, position);
        }

        public static void PlayLocal2D(this Sound.ID id)
        {
            AudioManager.PlayLocal2D(id);
        }
    }
}
