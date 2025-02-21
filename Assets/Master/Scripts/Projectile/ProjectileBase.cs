using Assets.Master.Scripts.Enemy.Base;
using System.Collections;
using UnityEngine;
using UnityObjectPooling;

namespace Assets.Master.Scripts.Projectile
{
    public class ProjectileBase : MonoBehaviour
    {
        private Transform target;

        public float speed = 70f;

        public int damage = 50;

        public float explosionRadius = 0f;
        public GameObject impactEffect;

        private ObjectPool<ProjectileBase> pool;

        public void SetPool(ObjectPool<ProjectileBase> pool)
        {
            this.pool = pool;
        }

        public void Seek(Transform _target)
        {
            target = _target;
        }

        void Update()
        {

            if (target == null)
            {
                //ObjectPoolManager.Instance.ReturnBullet(gameObject);
                pool.ReturnToPool(this);
                return;
            }

            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
            transform.LookAt(target);

        }

        void HitTarget()
        {
            GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(effectIns, 5f);

            if (explosionRadius > 0f)
            {
                Explode();
            }
            else
            {
                Damage(target);
            }

            pool.ReturnToPool(this);
            //ObjectPoolManager.Instance.ReturnBullet(gameObject);
        }

        void Explode()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider collider in colliders)
            {
                if (collider.tag == "Enemy")
                {
                    Damage(collider.transform);
                }
            }
        }

        void Damage(Transform enemy)
        {
            EnemyBase e = enemy.GetComponent<EnemyBase>();

            if (e != null)
            {
                e.TakeDamage(damage);
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}