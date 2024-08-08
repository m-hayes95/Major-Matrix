using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    // Events
    public UnityEvent OnDeath;
    // Referencs
    [SerializeField, Tooltip("Add reference for the player viusals to this field")] 
    private GameObject playerVisuals;
    [SerializeField, Tooltip("Add reference for the particle system that plays on player death, to this field")] 
    private ParticleSystem deathEffect;
    [SerializeField, Tooltip("Add reference for the game manager (in scene) to this field")] 
    private GameManager gameManager;
    [SerializeField, Tooltip("Add reference for the player hud (in scene) to this field")] 
    private PlayerHUD playerHUD;
    [SerializeField, Tooltip("Add reference for the corrosponding audio source under the Sounds gameoject (In scene), to this field")] 
    private AudioSource hitSound, deathSound;
    private PlayerController playerController;
    private Animator animator;
    // Counts
    private float currentHP;
    private int deathCount;
    // Checks
    private bool isDead;
    private bool bossIsDead;
    // Animation triggers
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
    public void OnEnemyDeath() // Called on boss death event to update stats (Need to move)
    {
        UpdateDamageDealtSavedStats();
        UpdateBossStatus();
    }

    private void UpdateDamageDealtSavedStats() // Update final stats
    { 
        bool bossHasBT = FindFirstObjectByType<BossType>().CheckIfBossHasBT();
        float remainingHP = playerController.stats.maxHP - currentHP;
        if (bossHasBT) SavedStats.Instance.StoreCurrentDamageDealtBT(remainingHP);
        else SavedStats.Instance.StoreCurrentDamageDealtSM(remainingHP);
    }
    private void UpdateBossStatus()
    {   // If true, the player will no longer take any damage (fixes issue where player can die after boss dies)
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
    private void ResetSavedStats() // Clear the saved stats if the player failed the run
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
