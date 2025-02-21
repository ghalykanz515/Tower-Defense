using Assets.Master.Scripts.Projectile;
using Assets.Master.Scripts.Enemy.Base;
using System.Collections;
using UnityEngine;
using UnityObjectPooling;

namespace Assets.Master.Scripts.Turret
{
    public class Turret : MonoBehaviour
    {
        private Transform target;
        private EnemyBase targetEnemy;

        [Header("General")]

        public float range = 15f;

        [Header("Use Bullets (default)")]
        public GameObject bulletPrefab;
        public float fireRate = 1f;
        public Transform bulletPoolParent;
        private float fireCountdown = 0f;

        [Header("Use Laser")]
        public bool useLaser = false;

        public int damageOverTime = 30;
        public float slowAmount = .5f;

        public LineRenderer lineRenderer;
        public ParticleSystem impactEffect;
        public Light impactLight;

        [Header("Unity Setup Fields")]

        public string enemyTag = "Enemy";

        public Transform partToRotate;
        public float turnSpeed = 10f;

        public Transform firePoint;

        // Use this for initialization
        void Start()
        {
            InvokeRepeating("UpdateTarget", 0f, 0.5f);
        }

        void UpdateTarget()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;
            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null && shortestDistance <= range)
            {
                target = nearestEnemy.transform;
                targetEnemy = nearestEnemy.GetComponent<EnemyBase>();
            }
            else
            {
                target = null;
            }

        }

        // Update is called once per frame
        void Update()
        {
            if (target == null)
            {
                if (useLaser)
                {
                    if (lineRenderer.enabled)
                    {
                        lineRenderer.enabled = false;
                        impactEffect.Stop();
                        impactLight.enabled = false;
                    }
                }

                return;
            }

            LockOnTarget();

            if (useLaser)
            {
                Laser();
            }
            else
            {
                if (fireCountdown <= 0f)
                {
                    Shoot();
                    fireCountdown = 1f / fireRate;
                }

                fireCountdown -= Time.deltaTime;
            }

        }

        void LockOnTarget()
        {
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }

        void Laser()
        {
            //targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
            //targetEnemy.Slow(slowAmount);

            if (!lineRenderer.enabled)
            {
                lineRenderer.enabled = true;
                impactEffect.Play();
                impactLight.enabled = true;
            }

            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, target.position);

            Vector3 dir = firePoint.position - target.position;

            impactEffect.transform.position = target.position + dir.normalized;

            impactEffect.transform.rotation = Quaternion.LookRotation(dir);
        }

        void Shoot()
        {
            ObjectPool<ProjectileBase> bulletPool = new ObjectPool<ProjectileBase>(bulletPrefab.GetComponent<ProjectileBase>(), 10, bulletPoolParent);

            ProjectileBase bullet = bulletPool.Get();
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;

            bullet.SetPool(bulletPool);

            if (bullet != null)
                bullet.Seek(target);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}