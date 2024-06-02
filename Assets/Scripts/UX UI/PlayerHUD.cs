using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private TextMeshProUGUI encounterTimer;

    [SerializeField] private Slider bossHP, playerHP;
    [SerializeField] private GameObject playerHUD;

    private bool isPlayerHUDOn = true;

    private void OnEnable() { GameManager.OnPaused += DisplayPlayerHUD; } 
    private void OnDisable() { GameManager.OnPaused -= DisplayPlayerHUD; } 
    private void Update()
    {
        DisplayPlayEncounterTimer();
    }

    private void DisplayPlayEncounterTimer()
    {
        encounterTimer.text = gameManager.GetEncounterTimer().ToString("00:00");
    }
    public void SetPlayerHealthBar(float currentHealth)
    {
        playerHP.value = currentHealth;
    }

    public void SetPlayerMaxHealthBar(float maxHealth)
    {
        playerHP.maxValue = maxHealth;
        playerHP.value = maxHealth;
    }
    public void SetBossHealthBar(float currentHealth)
    {
        bossHP.value = currentHealth;
    }
    public void SetBossMaxHealthBar(float maxHealth)
    {
        bossHP.maxValue = maxHealth;    
        bossHP.value = maxHealth;
    }
    private void DisplayPlayerHUD()
    {
        isPlayerHUDOn = !isPlayerHUDOn;
        playerHUD.SetActive(isPlayerHUDOn);
    }
    
}
