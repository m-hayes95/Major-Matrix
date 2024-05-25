using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : BossAI
{
    private enum StateMachine 
    { Idle, RangeAttack, CloseAttack, SpecialLowAttack, SpecialHighAttack, Shield };
    [SerializeField] private StateMachine sM;
    private bool canShield = true;
    private void Start()
    {
        sM = StateMachine.Idle;
    }

    protected override void Update()
    {
        base.Update();
        Debug.Log($"Boss State Machine Current State: {sM}");
        switch (sM)
        {
            case StateMachine.Idle:

                if (!shield.GetShieldStatus() && shield.GetCurrentShieldsAmount() > 0 && canShield && 
                    bossHP.GetBossCurrentHP() <= stats.maxHPtoAllowShield && RandomChance(stats.chanceToShield))
                        sM = StateMachine.Shield;

                if (distanceFromPlayer > stats.longRangeAttackThreshold && canAttack)
                {
                    // Randomise if boss should use a special or normal attack
                    if (!RandomChance(stats.chanceToUseSpecialAttack)) sM = StateMachine.RangeAttack;
                    else if (RandomChance(stats.chanceToUseSpecialAttack) && DistanceY() >= stats.specialHighAttackMinY)
                    {
                        sM = StateMachine.SpecialHighAttack;    
                    }
                    else
                    {
                        sM = StateMachine.SpecialLowAttack;
                    }
                }
                    
                if (distanceFromPlayer < stats.closeRangeAttackThreshold && canAttack) 
                    sM = StateMachine.CloseAttack;
                break;
            case StateMachine.RangeAttack:
                NormalRangeAttack();
                sM = StateMachine.Idle;
                break;
            case StateMachine.CloseAttack:
                NormalCloseAttack();
                sM = StateMachine.Idle;
                break;
            case StateMachine.SpecialLowAttack:
                SpecialLowAttack();
                sM = StateMachine.Idle;
                break;
            case StateMachine.SpecialHighAttack:
                SpecialHighAttack();
                sM = StateMachine.Idle;
                break;
            case StateMachine.Shield:
                canShield = false;
                Shield();
                StartCoroutine(ShieldCooldown(stats.shieldCooldownTime));
                sM = StateMachine.Idle;
                break;
            default:
                break;
        }
    }

    private IEnumerator ShieldCooldown(float seconds)
    {
        Debug.Log("Shield cooldown started");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Shield cooldown reset");
        canShield = !canShield;
    }
}
