using System.Collections;
using UnityEngine;
using TMPro;

namespace Assets.Master.Scripts.Core
{
    public class MoneyUI : MonoBehaviour
    {
        public TextMeshProUGUI moneyText;

        // Update is called once per frame
        void Update()
        {
            moneyText.text = "$" + PlayerStatsManager.Money.ToString();
        }
    }
}