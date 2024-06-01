using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameMenuStats : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private TextMeshProUGUI deathCount, gameTimer;
    private float savedTime, savedPlayerDeathCount;
    private bool timeStored = false;

    private void Update()
    {
        DisplayPlayerDeathCount();
        DisplayEncounterTime();
    }

    public void SaveEncounterTimer (float timer)
    {
        if (!timeStored)
        {
            timeStored = true;
            savedTime = timer;
        }
    }

    private void DisplayPlayerDeathCount()
    {
        deathCount.text = playerHealth.GetPlayerDeaths().ToString();
    }

    private void DisplayEncounterTime()
    {
        gameTimer.text = savedTime.ToString("00:00");
    }
}
