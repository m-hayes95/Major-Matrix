using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private BossHealth bossHealth;

    [SerializeField] private TextMeshProUGUI encounterTimer;

    private void Update()
    {
        DisplayPlayEncounterTimer();
    }

    private void DisplayPlayEncounterTimer()
    {
        encounterTimer.text = gameManager.GetEncounterTimer().ToString("00:00");
    }
    private void DisplayPlayerHealth()
    {

    }
    private void DisplayBossHealth()
    {

    }
}
