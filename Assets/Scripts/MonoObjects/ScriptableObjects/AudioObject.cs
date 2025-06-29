using System;
using UnityEngine;

namespace BelowSeaLevel_25
{
    [Serializable]
    public enum AudioMixerType
    {
        SFX,
        Music
    }

    public interface IAudio
    {
        public AudioClip GetAudioClip();
        public AudioMixerType GetAudioMixerType();
    }

    [CreateAssetMenu(fileName = "AudioObject", menuName = "Scriptable Objects/AudioObject")]
    public class AudioObject : ScriptableObject, IAudio
    {
        [SerializeField] private AudioClip m_AudioClip;
        [SerializeField] private AudioMixerType m_AudioMixerType;

        public AudioClip GetAudioClip() => m_AudioClip;
        public AudioMixerType GetAudioMixerType() => m_AudioMixerType;
    }
}
