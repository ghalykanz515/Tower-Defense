using Assets.Master.Scripts.Enemy.Base;
using System.Collections;
using UnityEngine;

namespace Assets.Master.Scripts.Enemy.States
{
    public class ShooterAttackState : IEnemyState
    {
        private EnemyBase enemy;
        private Transform targetTurret;
        private float shootCooldown = 1f;
        private float lastShotTime;

        public ShooterAttackState(Transform turret)
        {
            this.targetTurret = turret;
        }

        public void Enter(EnemyBase enemy)
        {
            this.enemy = enemy;
        }

        public void Update()
        {
            if (targetTurret == null)
            {
                enemy.stateMachine.SetState(new ShooterMoveState());
                return;
            }

            if (Time.time - lastShotTime > shootCooldown)
            {
                Shoot();
                lastShotTime = Time.time;
            }
        }

        public void Exit() { }

        private void Shoot()
        {
            Debug.Log("Shooter Enemy firing!");
            //targetTurret.GetComponent<Turret>().TakeDamage(10);
        }
    }
}