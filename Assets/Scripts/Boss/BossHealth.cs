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

    protected override void Update()
    {
        base.Update();
        Debug.Log($"Boss' current HP = {currentHP}");
    }

    public float GetBossCurrentHP()
    {
        return currentHP;
    }

    public void DamageBoss(float damageAmount)
    {
        if (currentHP > 0)
        {
            currentHP -= damageAmount;
            if (currentHP <= 0)
                OnDeath.Invoke();
        }
    }

    private void BossDead()
    {
        Debug.Log("Boss' current HP reached 0, Boss Died");
    }
}
