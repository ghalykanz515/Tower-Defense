using System.Collections;
using UnityEngine;
using Assets.Master.Scripts.Enemy.Base;
using Assets.Master.Scripts.Enemy.States;

namespace Assets.Master.Scripts.Enemy.EnemyTypes
{
    public class EnemyShooter : EnemyBase
    {
        protected override void Awake()
        {
            base.Awake();
            stateMachine.SetState(new ShooterMoveState());
        }
    }
}