using Assets.Master.Scripts.Enemy.Base;
using System.Collections;
using UnityEngine;

namespace Assets.Master.Scripts.Enemy.States
{
    public class EnemyStateMachine
    {
        private EnemyBase enemy;
        private IEnemyState currentState;

        public EnemyStateMachine(EnemyBase enemy)
        {
            this.enemy = enemy;
        }

        public void SetState(IEnemyState newState)
        {
            if (currentState != null)
                currentState.Exit();

            currentState = newState;
            currentState.Enter(enemy);
        }

        public void Update()
        {
            currentState?.Update();
        }
    }
}