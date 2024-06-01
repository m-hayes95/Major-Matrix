using System;
using UnityEngine;
using UnityEngine.Events;

public class BossHealth : BossAI
{
    [SerializeField] private GameObject bossVisuals;
    [SerializeField] private ParticleSystem deathEffect;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIController uIController;
    private float currentHP;
    private UnityEvent OnDeath;
    private bool isDead;
    

    private void Start()
    {
        currentHP = stats.maxHP;
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
            Debug.Log($"Boss Health: took {damageAmount} - damage, current HP: {currentHP}");
            currentHP -= damageAmount;
            if (currentHP <= 0)
                OnDeath.Invoke();
        }
        if (shield.GetShieldStatus())Debug.Log($"Shield is up, boss took 0  damage");
    }

    private void BossDead()
    {
        isDead = true;
        Debug.Log("Boss Health: current HP reached 0, Boss Died");
        DeathEffect();
        bossVisuals.SetActive(false);
        StartCoroutine(uIController.DisplayEndGameMenu(3f));
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
