using System.Collections;
using UnityEngine;
using Assets.Master.Scripts.Tower.Interface;
using Assets.Master.Scripts.Tower.Base;

namespace Assets.Master.Scripts.Tower.States.ConcreteStates
{
    public class TurretTrackingState : ITurretState
    {
        private TurretBase turret;
        private TurretStateMachine stateMachine;

        public TurretTrackingState(TurretBase turret, TurretStateMachine stateMachine)
        {
            this.turret = turret;
            this.stateMachine = stateMachine;
        }

        public void EnterState() { }

        public void UpdateState()
        {
            turret.FindTarget(); // Ensure the target is updated

            if (turret.Target == null)
            {
                stateMachine.ChangeState(new TurretIdleState(turret, stateMachine));
            }
            else
            {
                turret.LockOnTarget();
                if (turret.CanFire())
                {
                    stateMachine.ChangeState(new TurretFiringState(turret, stateMachine));
                }
            }

            //if (turret.Target == null)
            //    stateMachine.ChangeState(new TurretIdleState(turret, stateMachine));
            //else
            //{
            //    turret.LockOnTarget();
            //    if (turret.CanFire())
            //    {
            //        stateMachine.ChangeState(new TurretFiringState(turret, stateMachine));
            //    }
            //}
        }

        public void ExitState() { }
    }
}