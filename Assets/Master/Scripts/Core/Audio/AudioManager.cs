using Assets.Master.Scripts.Misc;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Master.Scripts.Core.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;

        private SoundsCollectionSO soundsCollection;

        public void Initialize(SoundsCollectionSO soundsCollection)
        {
            this.soundsCollection = soundsCollection;
        }

        public void PlayMusic(string musicName)
        {
            SoundSO music = System.Array.Find(soundsCollection.BGMusic, sound => sound.name == musicName);
            if (music == null)
            {
                Debug.LogWarning("Music: " + musicName + " not found!");
                return;
            }

            musicSource.clip = music.Clip;
            musicSource.volume = music.Volume;
            musicSource.pitch = music.Pitch;
            musicSource.loop = music.Loop;
            musicSource.Play();
        }

        public void PlaySFX(string sfxName)
        {
            SoundSO sfx = System.Array.Find(soundsCollection.TurretShoot, sound => sound.name == sfxName);
            if (sfx == null)
            {
                Debug.LogWarning("SFX: " + sfxName + " not found!");
                return;
            }

            sfxSource.clip = sfx.Clip;
            sfxSource.volume = sfx.Volume;
            sfxSource.pitch = sfx.Pitch;
            sfxSource.loop = sfx.Loop;
            sfxSource.Play();
        }

        public void SetMusicVolume(float volume)
        {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        }

        public void SetSFXVolume(float volume)
        {
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        }
    }
}