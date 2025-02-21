using System.Collections;
using UnityEngine;
using Assets.Master.Scripts.Enemy.Base;

namespace Assets.Master.Scripts.Projectile
{
    public class Projectile : MonoBehaviour
    {
        public ProjectileData data; // Shared data (intrinsic state)
        private Vector3 _direction; // Unique data (extrinsic state)

        public void Initialize(ProjectileData data, Vector3 direction)
        {
            this.data = data;
            this._direction = direction;

            // Apply shared properties
            GetComponent<MeshRenderer>().material = data.material;
        }

        void Update()
        {
            // Move the projectile
            transform.position += _direction * data.speed * Time.deltaTime;
        }

        void OnCollisionEnter(Collision collision)
        {
            // Handle collision (e.g., deal damage, spawn effects)
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<EnemyBase>().TakeDamage(data.damage);
            }

            // Spawn impact effect
            if (data.impactEffect != null)
            {
                Instantiate(data.impactEffect, transform.position, Quaternion.identity);
            }

            // Destroy the projectile
            Destroy(gameObject);
        }
    }
}