using UnityEngine;
using UnityEngine.Events;

public class BossHealth : BossAI
{
    private float currentHP;
    private UnityEvent OnDeath;
  
    private void Start()
    {
        currentHP = stats.maxHP;
        if (OnDeath == null)
            OnDeath = new UnityEvent();
        OnDeath.AddListener(BossDead);
    }

    public float GetBossCurrentHP()
    {
        return currentHP;
    }

    public void DamageBoss(float damageAmount)
    {
        if (!shield.GetShieldStatus() && currentHP > 0)
        {
            Debug.Log($"Boss took {damageAmount} - damage, current HP: {currentHP}");
            currentHP -= damageAmount;
            if (currentHP <= 0)
                OnDeath.Invoke();
        }
        if (shield.GetShieldStatus())Debug.Log($"Shield is up, boss took 0  damage");
    }

    private void BossDead()
    {
        Debug.Log("Boss' current HP reached 0, Boss Died");
    }
}
