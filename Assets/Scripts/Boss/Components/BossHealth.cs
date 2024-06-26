using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(BossStatsComponent), typeof(Shield))]
public class BossHealth : MonoBehaviour
{
    public UnityEvent OnDeath;

    [SerializeField] private GameObject bossVisuals;
    [SerializeField] private ParticleSystem deathEffect;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIController uIController;
    [SerializeField] private PlayerHUD playerHUD;
    [SerializeField] private AudioSource hitSound, sheildHitSound, deathSound;

    private float currentHP;
    private BossStatsScriptableObject stats;
    private Shield shield;
    private Animator animator;
    private bool isDead;

    private void Awake()
    {
        stats = GetComponent<BossStatsComponent>().bossStats;
        shield = GetComponent<Shield>();    
        animator = GetComponent<Animator>();
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
        animator.SetTrigger("BossDied");
        deathSound.Play();
        isDead = true;
        Debug.Log("Boss Health: current HP reached 0, Boss Died");
        DeathEffect();
        StartCoroutine(DestroyBoss());
    }
    private IEnumerator DestroyBoss()
    {
        yield return new WaitForSeconds(.1f);
        gameObject.SetActive(false);
    }

    private void DeathEffect()
    {
        ParticleSystem newEffect = Instantiate(deathEffect, transform.position, Quaternion.identity);
    }

    public bool GetIsDead()
    {
        return isDead;
    }
}
