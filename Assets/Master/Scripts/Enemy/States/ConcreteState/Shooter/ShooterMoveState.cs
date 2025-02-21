using Assets.Master.Scripts.Enemy.Base;
using System.Collections;
using UnityEngine;
using System.Linq;

namespace Assets.Master.Scripts.Enemy.States
{
    public class ShooterMoveState : IEnemyState
    {
        private EnemyBase enemy;
        private Transform targetTurret;

        public void Enter(EnemyBase enemy)
        {
            this.enemy = enemy;
            FindNearestTurret();
        }

        public void Update()
        {
            if (targetTurret != null)
            {
                enemy.agent.isStopped = true;
                enemy.stateMachine.SetState(new ShooterAttackState(targetTurret));
            }
            else
            {
                enemy.agent.isStopped = false;
                enemy.agent.SetDestination(enemy.target.position);
            }
        }

        public void Exit() { }

        private void FindNearestTurret()
        {
            //var turrets = Object.FindObjectsOfType<Turret>();
            //targetTurret = turrets.OrderBy(t => Vector3.Distance(enemy.transform.position, t.transform.position)).FirstOrDefault();
        }
    }
}