using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace BelowSeaLevel_25
{
    /// <summary>
    /// This initalizes and sets up mixers and audio sources for the Unity Audio system.
    /// </summary>
    internal class AudioManager : MonoManager<AudioManager>
    {
        public InitalizationTable<AudioObject> audioMap;
        public GameObject AudioSourcePrefab;
        public AudioMixerGroup MasterMixer;
        public AudioMixerGroup SFXMixer;
        public AudioMixerGroup MusicMixer;

        public int NumberOfSFXSources;

        private AudioSource m_MusicSource;
        private List<AudioSource> m_AudioSources;
        private List<AudioSource> ActiveAudioSources => m_AudioSources
            .Where(x => !x.isPlaying)
            .ToList();

        public override void Init()
        {
            base.Init();

            m_AudioSources = new List<AudioSource>();

            for (int i = 0; i < NumberOfSFXSources; i++)
            {
                AudioSource audioSource = Instantiate(AudioSourcePrefab).GetComponent<AudioSource>();
                audioSource.outputAudioMixerGroup = SFXMixer;

                m_AudioSources.Add(audioSource);
            }

            m_MusicSource = Instantiate(AudioSourcePrefab).GetComponent<AudioSource>();
            m_MusicSource.loop = true;
            m_MusicSource.outputAudioMixerGroup = MusicMixer;
        }

        public static void PlayMusic()
        {
            const string MUSIC_TRACK = "MusicTrack";

            IAudio audio = Instance.audioMap.Get(MUSIC_TRACK) as IAudio;

            Instance.m_MusicSource.clip = audio.GetAudioClip();
            Instance.m_MusicSource.Play();
        }

        public static void PlaySFXClip(string clipName)
        {
            IAudio audio = Instance.audioMap.Get(clipName) as IAudio;

            AudioSource source = Instance.ActiveAudioSources.FirstOrDefault();

            if (source == null)
            {
                return;
            }

            source.clip = audio.GetAudioClip();
            source.loop = false;
            source.Play();
        }
    }
}
