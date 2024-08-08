using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField, Tooltip("Add reference for the game manager to this field")] 
    private GameManager gameManager;
    [SerializeField, Tooltip("Add reference for the encounter timer to this field")] 
    private EncounterTimer encounterTimer;

    [SerializeField, Tooltip("Add reference for the time display update text to this field")] 
    private TextMeshProUGUI timeDisplay;

    [SerializeField, Tooltip("Add reference for the health display update text to this field")] 
    private Slider bossHP, playerHP;
    [SerializeField, Tooltip("Add reference for the player hud display gameobject to this field")] 
    private GameObject playerHUD;

    private bool isPlayerHUDOn = true;

    private void OnEnable() { GameManager.OnPaused += DisplayPlayerHUD; } 
    private void OnDisable() { GameManager.OnPaused -= DisplayPlayerHUD; } 
    private void Update()
    {
        DisplayPlayEncounterTimer();
    }
    // Update player HUD infomation
    private void DisplayPlayEncounterTimer()
    {
        timeDisplay.text = encounterTimer.GetEncounterTimer().ToString("00:00");
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
    private void DisplayPlayerHUD() // Hide HUD when the game is paused
    {
        isPlayerHUDOn = !isPlayerHUDOn;
        playerHUD.SetActive(isPlayerHUDOn);
    }
    
}
