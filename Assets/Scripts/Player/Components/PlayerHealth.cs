
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject playerVisuals;
    [SerializeField] private ParticleSystem deathEffect;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIController uIController;
    [SerializeField] private PlayerHUD playerHUD;
    [SerializeField] private AudioSource hitSound, deathSound;
    private PlayerController playerController;
    private float currentHP;
    [SerializeField]private int deathCount;
    private UnityEvent OnDeath;
    private bool isDead;
    

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        currentHP = playerController.stats.maxHP;
        if (playerHUD != null )
            playerHUD.SetPlayerMaxHealthBar(currentHP);
        if (OnDeath == null)
            OnDeath = new UnityEvent();
        OnDeath.AddListener(PlayerDead);
        isDead = false; 
        deathCount = 0;
        
    }

    public void DamagePlayer(float damageAmount)
    {
        if (currentHP > 0)
        {
            currentHP -= damageAmount;
            hitSound.Play();
            playerHUD.SetPlayerHealthBar(currentHP);
            if (currentHP <= 0) 
                OnDeath.Invoke();
        }
    }

    private void PlayerDead()
    {
        deathCount++;
        deathSound.Play();
        isDead = true;
        Debug.Log("Players current HP reached 0, Player Died");
        DeathEffect();
        playerVisuals.SetActive(false);
        StartCoroutine(uIController.DisplayEndGameMenu(3f));
    }

    private void DeathEffect()
    {
        ParticleSystem newEffect = Instantiate(deathEffect, transform.position, Quaternion.identity);
    }
    public float GetPlayersCurrentHP() { return currentHP; }
    public int GetPlayerDeaths() { return deathCount; }
    public bool GetIsDead() { return isDead; }
}
