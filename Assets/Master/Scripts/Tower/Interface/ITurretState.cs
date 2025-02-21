using System.Collections;
using UnityEngine;

namespace Assets.Master.Scripts.Tower.Interface
{
    public interface ITurretState
    {
        void EnterState();
        void UpdateState();
        void ExitState();
    }
}