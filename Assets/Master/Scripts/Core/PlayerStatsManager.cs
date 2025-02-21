using System.Collections;
using UnityEngine;

namespace Assets.Master.Scripts.Core
{
    public class PlayerStatsManager : MonoBehaviour
    {
        public static int Money;
        public int startMoney = 400;

        public static int Lives;
        public int startLives = 20;

        public static int Rounds;

        void Start()
        {
            Money = startMoney;
            Lives = startLives;

            Rounds = 0;
        }
    }
}