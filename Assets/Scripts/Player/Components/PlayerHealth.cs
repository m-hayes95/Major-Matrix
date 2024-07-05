using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public UnityEvent OnDeath;
    [SerializeField] private GameObject playerVisuals;
    [SerializeField] private ParticleSystem deathEffect;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerHUD playerHUD;
    [SerializeField] private AudioSource hitSound, deathSound;
    private PlayerController playerController;
    private Animator animator;
    private float currentHP;
    private int deathCount;
    private bool isDead;
    private bool bossIsDead;
    private const string DEATH_ANIMATION = "PlayerDied";
    private const string HURT_ANIMATION = "PlayerHurt";



    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
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
        if (currentHP > 0 && !bossIsDead)
        {
            animator.SetTrigger(HURT_ANIMATION);
            currentHP -= damageAmount;
            hitSound.Play();
            playerHUD.SetPlayerHealthBar(currentHP);
            if (currentHP <= 0) 
                OnDeath.Invoke();
        }
    }
    public void OnEnemyDeath() // Called on boss death event
    {
        UpdateDamageDealtSavedStats();
        UpdateBossStatus();
    }

    private void UpdateDamageDealtSavedStats()
    { 
        bool bossHasBT = FindFirstObjectByType<BossType>().CheckIfBossHasBT();
        float remainingHP = playerController.stats.maxHP - currentHP;
        if (bossHasBT) SavedStats.Instance.StoreCurrentDamageDealtBT(remainingHP);
        else SavedStats.Instance.StoreCurrentDamageDealtSM(remainingHP);
    }
    private void UpdateBossStatus() 
    {
        bossIsDead = true;
    }
    
    private void PlayerDead()
    {
        ResetSavedStats();
        animator.SetTrigger(DEATH_ANIMATION);
        deathCount++;
        deathSound.Play();
        isDead = true;
        Debug.Log("Players current HP reached 0, Player Died");
        DeathEffect();
    }
    private void ResetSavedStats()
    {
        bool bossHasBT = FindFirstObjectByType<BossType>().CheckIfBossHasBT();
        if (bossHasBT) SavedStats.Instance.ResetBTCountStats();
        else SavedStats.Instance.ResetSMCountStats();
    }

    private void DeathEffect()
    {
        ParticleSystem newEffect = Instantiate(deathEffect, transform.position, Quaternion.identity);
    }
    public float GetPlayersCurrentHP() { return currentHP; }
    public int GetPlayerDeaths() { return deathCount; }
    public bool GetIsDead() { return isDead; }
}
