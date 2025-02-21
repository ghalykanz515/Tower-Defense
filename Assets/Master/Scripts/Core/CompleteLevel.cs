using System.Collections;
using UnityEngine;

namespace Assets.Master.Scripts.Core
{
    public class CompleteLevel : MonoBehaviour
    {
        public string menuSceneName = "MainMenu";

        public string nextLevel = "Level02";
        public int levelToUnlock = 2;

        public SceneFader sceneFader;

        public void Continue()
        {
            PlayerPrefs.SetInt("levelReached", levelToUnlock);
            sceneFader.FadeTo(nextLevel);
        }

        public void Menu()
        {
            sceneFader.FadeTo(menuSceneName);
        }
    }
}