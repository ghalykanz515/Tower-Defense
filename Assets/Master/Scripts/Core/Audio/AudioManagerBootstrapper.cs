using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityServiceLocator;

namespace Assets.Master.Scripts.Core.Audio
{
    public class AudioManagerBootstrapper : Bootstrapper
    {
        [SerializeField] private SoundsCollectionSO soundsCollection;
        [SerializeField] private AudioMixer audioMixer;

        protected override void Bootstrap()
        {
            var audioManager = GetComponent<AudioManager>();
            audioManager.Initialize(soundsCollection);

            ServiceLocator.For(this).Register<AudioManager>(audioManager);
        }
    }
}