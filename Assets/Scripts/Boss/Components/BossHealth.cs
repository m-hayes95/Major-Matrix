using System.Collections;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(BossStatsComponent), typeof(Shield))]
public class BossHealth : MonoBehaviour
{
    // Events (Add in inspector)
    public UnityEvent OnDeath;
    // Referenecs
    [SerializeField, Tooltip("Add reference for the boss viusals to this field")] 
    private GameObject bossVisuals;
    [SerializeField, Tooltip("Add reference for the particle system that plays on boss death, to this field")] 
    private ParticleSystem deathEffect;
    [SerializeField, Tooltip("Add reference for the game manager (in scene) to this field")] 
    private GameManager gameManager;
    [SerializeField, Tooltip("Add reference for the player hud (in scene) to this field")] 
    private UIController uIController;
    [SerializeField, Tooltip("Add reference for the player hud (in scene) to this field")] 
    private PlayerHUD playerHUD;
    [SerializeField, Tooltip("Add reference for the corrosponding audio source under the Sounds gameoject (In scene), to this field")] 
    private AudioSource hitSound, sheildHitSound, deathSound;
    [SerializeField, Tooltip("Add a reference to the dead enemy game object that spawns when the boss dies, to this field")] 
    private GameObject enemyDeathState;

    public BossStatsScriptableObject stats;
    private Shield shield;
    private Animator animator;
    private BossType bossType;

    private float currentHP;
    private bool isDead;

    private void Awake()
    {
        stats = GetComponent<BossStatsComponent>().bossStats;
        shield = GetComponent<Shield>();    
        animator = GetComponent<Animator>();
        bossType = GetComponent<BossType>();    
    }
    private void Start()
    {
        currentHP = stats.maxHP;
        playerHUD.SetBossMaxHealthBar(stats.maxHP);
        if (OnDeath == null)
            OnDeath = new UnityEvent();
        OnDeath.AddListener(BossDead);
        isDead = false;
    }

    public float GetBossCurrentHP()
    {
        return currentHP;
    }

    public void DamageBoss(float damageAmount)
    {
        if (!shield.GetShieldStatus() && currentHP > 0)
        {
            //Debug.Log($"Boss Health: took {damageAmount} - damage, current HP: {currentHP}");
            animator.SetTrigger("BossTookDamage");
            currentHP -= damageAmount;
            hitSound.Play();
            playerHUD.SetBossHealthBar(currentHP);
            if (currentHP <= 0)
                OnDeath.Invoke();
        }
        if (shield.GetShieldStatus())
        {
            sheildHitSound.Play();
            //Debug.Log($"Shield is up, boss took 0  damage");
        }
    }

    private void BossDead()
    {
        // Call all boss death logic
        Instantiate(enemyDeathState, transform.position, transform.rotation);
        deathSound.Play();
        isDead = true;
        Debug.Log("Boss Health: current HP reached 0, Boss Died");
        DeathEffect();
        StartCoroutine(DestroyBoss());
    }
    private IEnumerator DestroyBoss()
    {
        yield return new WaitForSeconds(.01f);
        gameObject.SetActive(false);
    }

    private void DeathEffect()
    {
        ParticleSystem newEffect = Instantiate(deathEffect, transform.position - Vector3.up, Quaternion.identity);
    }

    public bool GetIsDead()
    {
        return isDead;
    }
}
