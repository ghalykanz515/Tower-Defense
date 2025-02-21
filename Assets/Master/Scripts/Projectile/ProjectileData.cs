using System.Collections;
using UnityEngine;

namespace Assets.Master.Scripts.Projectile
{
    [CreateAssetMenu(fileName = "ProjectileData", menuName = "Projectile/ProjectileData")]
    public class ProjectileData : ScriptableObject
    {
        public string projectileName;
        public GameObject prefab;    
        public float speed;          
        public int damage;          
        public Material material;   
        public ParticleSystem impactEffect;
    }
}