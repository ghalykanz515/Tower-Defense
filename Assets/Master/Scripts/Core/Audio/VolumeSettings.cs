using System.Collections;
using UnityEngine;
using UnityServiceLocator;

namespace Assets.Master.Scripts.Core.Audio
{
    public class VolumeSettings : MonoBehaviour
    {
        private AudioManager audioManager;

        private void Start()
        {
            audioManager = ServiceLocator.For(this).Get<AudioManager>();
        }

        public void SetMusicVolume(float volume)
        {
            audioManager.SetMusicVolume(volume);
        }

        public void SetSFXVolume(float volume)
        {
            audioManager.SetSFXVolume(volume);
        }
    }
}