using System.Collections;
using UnityEngine;
using Assets.Master.Scripts.Tower.Interface;
using Assets.Master.Scripts.Tower.Base;
using Assets.Master.Scripts.Tower.TurretTypes;

namespace Assets.Master.Scripts.Tower.States.ConcreteStates
{
    public class TurretFiringState : ITurretState
    {
        private TurretBase turret;
        private TurretStateMachine stateMachine;
        private bool isLaserTurret; // Flag to check if this is a laser turret

        public TurretFiringState(TurretBase turret, TurretStateMachine stateMachine)
        {
            this.turret = turret;
            this.stateMachine = stateMachine;
            this.isLaserTurret = turret is LaserTurret; // Check if the turret is a LaserTurret
        }

        public void EnterState()
        {
            // Only call Shoot for non-laser turrets
            if (!isLaserTurret)
            {
                turret.Shoot();
            }
        }

        public void UpdateState()
        {
            if (turret.Target == null || !turret.CanFire())
            {
                stateMachine.ChangeState(new TurretIdleState(turret, stateMachine));
                return;
            }

            turret.LockOnTarget();

            // Continuously call Shoot for laser turrets
            if (isLaserTurret)
            {
                turret.Shoot();
            }

            // Transition to TrackingState if the turret cannot fire (e.g., cooldown)
            if (!turret.CanFire())
            {
                stateMachine.ChangeState(new TurretTrackingState(turret, stateMachine));
            }
            //if (turret.Target == null)
            //{
            //    stateMachine.ChangeState(new TurretIdleState(turret, stateMachine));
            //    return;
            //}

            //turret.LockOnTarget();

            //// Continuously call Shoot for laser turrets
            //if (isLaserTurret)
            //{
            //    turret.Shoot();
            //}

            //// Transition to TrackingState if the turret cannot fire (e.g., cooldown)
            //if (!turret.CanFire())
            //{
            //    stateMachine.ChangeState(new TurretTrackingState(turret, stateMachine));
            //}
        }

        public void ExitState() { }
    }
}