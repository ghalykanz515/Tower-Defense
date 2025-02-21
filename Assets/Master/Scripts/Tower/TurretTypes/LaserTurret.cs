using System.Collections;
using UnityEngine;
using Assets.Master.Scripts.Tower.States.ConcreteStates;
using Assets.Master.Scripts.Tower.Base;
using Assets.Master.Scripts.Enemy.Base;

namespace Assets.Master.Scripts.Tower.TurretTypes
{
    public class LaserTurret : TurretBase
    {
        [Header("Laser Settings")]
        public LineRenderer lineRenderer;
        public int damageOverTime = 30;
        public float slowAmount = 0.5f;
        public ParticleSystem impactEffect;
        public Light impactLight;
        public Transform firePoint; // Add firePoint for laser origin

        private EnemyBase targetEnemy;

        protected override void Start()
        {
            base.Start();
            stateMachine.Initialize(new TurretIdleState(this, stateMachine));

            // Initialize laser components
            if (lineRenderer != null)
            {
                lineRenderer.enabled = false;
                lineRenderer.positionCount = 2; // Ensure LineRenderer has two positions
            }

            if (impactEffect != null)
            {
                impactEffect.Stop();
            }

            if (impactLight != null)
            {
                impactLight.enabled = false;
            }
        }

        public override bool CanFire() => Target != null && Vector3.Distance(transform.position, Target.position) <= range;

        public override void LockOnTarget()
        {
            if (Target == null) return;

            // Update target enemy reference
            targetEnemy = Target.GetComponent<EnemyBase>();
            Debug.Log(targetEnemy);

            // Rotate turret towards the target
            Vector3 dir = Target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }

        public override void Shoot()
        {
            if (lineRenderer == null || Target == null || firePoint == null) return;

            // Enable laser visuals
            if (!lineRenderer.enabled)
            {
                lineRenderer.enabled = true;
                if (impactEffect != null) impactEffect.Play();
                if (impactLight != null) impactLight.enabled = true;
            }

            // Set laser positions
            lineRenderer.SetPosition(0, firePoint.position); // Start from firePoint
            lineRenderer.SetPosition(1, Target.position);    // End at target

            // Apply damage over time and slow effect
            if (targetEnemy != null)
            {
                targetEnemy.TakeDamage(damageOverTime * (int)Time.deltaTime);
                targetEnemy.Slow(slowAmount);
            }

            // Update impact effect position and rotation
            if (impactEffect != null)
            {
                Vector3 dir = firePoint.position - Target.position;
                impactEffect.transform.position = Target.position + dir.normalized * 0.5f; // Adjust offset as needed
                impactEffect.transform.rotation = Quaternion.LookRotation(dir);
            }
        }

        protected override void Update()
        {
            base.Update();

            // Disable laser if no target or target is out of range
            if (Target == null || Vector3.Distance(transform.position, Target.position) > range)
            {
                if (lineRenderer != null && lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    if (impactEffect != null) impactEffect.Stop();
                    if (impactLight != null) impactLight.enabled = false;
                }

                // Clear the target reference
                Target = null;
                targetEnemy = null;
            }
        }
    }
}