using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : BossAI
{
    private enum StateMachine 
    { Idle, RangeAttack, CloseAttack, SpecialLowAttack, SpecialHighAttack, Shield, ChasePlayer, Dead };
    [SerializeField] private StateMachine sM;
    [SerializeField] private AudioSource closeRangeAttackSound;
    private bool canShield = true;
    private void Start()
    {
        sM = StateMachine.Idle;
    }

    protected override void Update()
    {
        base.Update();
        
        Debug.Log($"Boss State Machine Current State: {sM}");
        if (isGamePaused) return; // Disable state machine if game is paused
        switch (sM)
        {
            case StateMachine.Idle:
                
                if (bossHP.GetIsDead()) sM = StateMachine.Dead;

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

                if (canChase && distanceFromPlayer > stats.closeRangeAttackThreshold &&
                 distanceFromPlayer < stats.longRangeAttackThreshold) 
                    sM = StateMachine.ChasePlayer;
                    break;
            case StateMachine.RangeAttack:
                NormalRangeAttack();
                //StartCoroutine(StateCooldown(4f, StateMachine.Idle));
                sM = StateMachine.Idle;
                break;
            case StateMachine.CloseAttack:
                NormalCloseAttack(closeRangeAttackSound);
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
            case StateMachine.ChasePlayer:
                Debug.Log($"Boss can chase player: {canChase}");
                MoveToPlayer();
                StartCoroutine(StateCooldown(stats.chaseTimer, StateMachine.Idle));
                break;
            case StateMachine.Dead:
                Debug.Log($"FSM: Boss is Dead, current state {sM}");
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

    private IEnumerator StateCooldown(float seconds, StateMachine nextState)
    {
        yield return new WaitForSeconds(seconds);
        Debug.Log($"State Cooldown: Waited for {seconds} seconds before entering the next state: {nextState}");
        sM = nextState;
    }
}
