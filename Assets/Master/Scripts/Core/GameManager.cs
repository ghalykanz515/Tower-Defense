using UnityEventBus;
using UnityServiceLocator;
using System.Collections;
using UnityEngine;
using Assets.Master.Scripts.Projectile;

namespace Assets.Master.Scripts.Core
{
    public class GameManager : MonoBehaviour
    {
        public static bool GameIsOver;

        public GameObject gameOverUI;
        public GameObject completeLevelUI;

        void Start()
        {
            GameIsOver = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (GameIsOver)
                return;

            if (PlayerStatsManager.Lives <= 0)
            {
                EndGame();
            }
        }

        void EndGame()
        {
            GameIsOver = true;
            gameOverUI.SetActive(true);
        }

        public void WinLevel()
        {
            GameIsOver = true;
            completeLevelUI.SetActive(true);
        }
    }
}