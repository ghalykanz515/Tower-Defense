using Assets.Master.Scripts.Tower.States;
using System.Collections;
using UnityEngine;
using Assets.Master.Scripts.Core.Audio;
using UnityServiceLocator;
using Unity.VisualScripting;

namespace Assets.Master.Scripts.Tower.Base
{
    public abstract class TurretBase : MonoBehaviour
    {
        public Transform Target { get; protected set; }
        protected TurretStateMachine stateMachine;

        [Header("General Settings")]
        public float range = 15f;
        public Transform bulletPoolParent;

        [Header("Rotation Settings")]
        public Transform partToRotate;
        public float turnSpeed = 10f;

        public AudioManager audioManager;

        protected virtual void Start()
        {
            audioManager = GameObject.Find("GameManager").GetComponent<AudioManager>();

            audioManager = ServiceLocator.For(this).Get<AudioManager>();

            stateMachine = new TurretStateMachine();
        }

        protected virtual void Update()
        {
            stateMachine.Update();
        }

        public abstract bool CanFire();
        public abstract void Shoot();
        public abstract void LockOnTarget();

        public void FindTarget()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;

            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance && distanceToEnemy <= range)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }

            Target = nearestEnemy != null ? nearestEnemy.transform : null;

            //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            //float shortestDistance = Mathf.Infinity;
            //GameObject nearestEnemy = null;

            //foreach (GameObject enemy in enemies)
            //{
            //    float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            //    if (distanceToEnemy < shortestDistance)
            //    {
            //        shortestDistance = distanceToEnemy;
            //        nearestEnemy = enemy;
            //    }
            //}

            //Target = (nearestEnemy != null && shortestDistance <= range) ? nearestEnemy.transform : null;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}