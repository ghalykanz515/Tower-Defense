using Assets.Master.Scripts.Enemy.Base;
using System.Collections;
using UnityEngine;

namespace Assets.Master.Scripts
{
    public interface IEnemyState
    {
        void Enter(EnemyBase enemy);
        void Update();
        void Exit();
    }
}