using Assets.Master.Scripts.Enemy.Base;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Assets.Master.Scripts.Enemy.States
{
    public class BomberMoveState : IEnemyState
    {
        private EnemyBase enemy;

        public void Enter(EnemyBase enemy)
        {
            this.enemy = enemy;
        }

        public void Update()
        {
            if (enemy.target == null) 
                return;

            enemy.agent.SetDestination(enemy.target.position);

            if (AgentNear(enemy.target.position, 2f))
            {
                Explode();
            }
        }

        public void Exit() { }

        private void Explode()
        {
            enemy.Die();
            //Debug.Log("Take some Turret Damage");
        }

        private bool AgentNear(Vector3 target, float threshold)
        {
            Vector2 agentXZ = new Vector2(enemy.transform.position.x, enemy.transform.position.z);
            Vector2 targetXZ = new Vector2(target.x, target.z);

            return Vector2.Distance(agentXZ, targetXZ) <= threshold;
        }
    }
}