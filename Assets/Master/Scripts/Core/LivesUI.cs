using UnityEngine;
using TMPro;
using Assets.Master.Scripts.Core;

public class LivesUI : MonoBehaviour
{

    public TextMeshProUGUI livesText;

    // Update is called once per frame
    void Update()
    {
        livesText.text = PlayerStatsManager.Lives.ToString() + " LIVES";
    }
}
