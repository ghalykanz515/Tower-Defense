using System.Collections;
using UnityEngine;
using Assets.Master.Scripts.Tower.Interface;
using Assets.Master.Scripts.Tower.Base;

namespace Assets.Master.Scripts.Tower.States.ConcreteStates
{
    public class TurretIdleState : ITurretState
    {
        private TurretBase turret;
        private TurretStateMachine stateMachine;

        public TurretIdleState(TurretBase turret, TurretStateMachine stateMachine)
        {
            this.turret = turret;
            this.stateMachine = stateMachine;
        }

        public void EnterState() { }

        public void UpdateState()
        {
            turret.FindTarget();
            if (turret.Target != null)
                stateMachine.ChangeState(new TurretTrackingState(turret, stateMachine));
        }

        public void ExitState() { }
    }
}