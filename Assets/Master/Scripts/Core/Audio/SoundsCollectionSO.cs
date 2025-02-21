using System.Collections;
using UnityEngine;

namespace Assets.Master.Scripts.Core.Audio
{
    [CreateAssetMenu()]
    public class SoundsCollectionSO : ScriptableObject
    {
        [Header("Music")]
        public SoundSO[] BGMusic;

        [Space]
        [Header("SFX")]
        public SoundSO[] TurretShoot;
        public SoundSO[] Explode;
        public SoundSO[] EnemyHit;
    }
}