using System.Collections;
using UnityEngine;
using Assets.Master.Scripts.Tower.Interface;

namespace Assets.Master.Scripts.Tower.States
{
    public class TurretStateMachine
    {
        private ITurretState currentState;

        public void Initialize(ITurretState startingState)
        {
            currentState = startingState;
            currentState.EnterState();
        }

        public void ChangeState(ITurretState newState)
        {
            currentState.ExitState();
            currentState = newState;
            currentState.EnterState();
        }

        public void Update()
        {
            currentState?.UpdateState();
        }
    }
}