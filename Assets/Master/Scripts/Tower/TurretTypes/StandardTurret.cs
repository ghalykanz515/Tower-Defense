using System.Collections;
using UnityEngine;
using UnityObjectPooling;
using Assets.Master.Scripts.Tower.Base;
using Assets.Master.Scripts.Tower.States.ConcreteStates;
using Assets.Master.Scripts.Projectile;

namespace Assets.Master.Scripts.Tower.TurretTypes
{
    public class StandardTurret : TurretBase
    {
        [Header("Bullet Settings")]
        public GameObject bulletPrefab;
        public float fireRate = 1f;
        public Transform firePoint;

        private float fireCountdown = 0f;

        protected override void Update()
        {
            base.Update();

            if (fireCountdown > 0f)
            {
                fireCountdown -= Time.deltaTime;
            }
        }

        protected override void Start()
        {
            base.Start();
            stateMachine.Initialize(new TurretIdleState(this, stateMachine));
        }

        public override bool CanFire() => fireCountdown <= 0f;

        public override void LockOnTarget()
        {
            if (Target == null) return;

            Vector3 dir = Target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }

        public override void Shoot()
        {
            if (bulletPrefab == null || firePoint == null) return;

            ObjectPool<ProjectileBase> bulletPool = new ObjectPool<ProjectileBase>(bulletPrefab.GetComponent<ProjectileBase>(), 10, bulletPoolParent);

            ProjectileBase bullet = bulletPool.Get();
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;

            bullet.SetPool(bulletPool);

            if (bullet != null)
                bullet.Seek(Target);

            fireCountdown = 1f / fireRate;

            audioManager.PlaySFX("Shoot_Standard");
        }
    }
}