using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : MonoBehaviour
{
    private enum StateMachine 
    { Idle, Combat, RangeAttack, MeleeAttack, SpecialLowAttack, SpecialHighAttack, Shield, ChasePlayer, Dead };

    [SerializeField] private StateMachine sM;
    [SerializeField] private GameObject target;
    private bool facingLeft = true;
    // Components
    private Shoot shoot;
    private ChasePlayer chasePlayer;
    private BossHealth health;
    private Shield shield;
    private MeleeAttack meleeAttack;
    private AttackCooldown attackCooldown;
    private RandomChance randomChance;
    private SpecialAttacks specialAttacks;
    private DistanceToTargetY distanceToTargetY;
    private BossStatsScriptableObject stats;
    private LookAtTarget lookAtTarget;
    
    private void Awake()
    {
        GetComponentReferences();
        sM = StateMachine.Idle;
    }

    private void GetComponentReferences()
    {
        stats = GetComponent<BossStatsComponent>().bossStats;
        shoot = GetComponent<Shoot>();
        chasePlayer = GetComponent<ChasePlayer>();
        health = GetComponent<BossHealth>();
        shield = GetComponent<Shield>();
        meleeAttack = GetComponent<MeleeAttack>();
        attackCooldown = GetComponent<AttackCooldown>();
        randomChance = GetComponent<RandomChance>();
        specialAttacks = GetComponent<SpecialAttacks>();
        distanceToTargetY = GetComponent<DistanceToTargetY>();
        lookAtTarget = GetComponent<LookAtTarget>();
    }

    private void Update()
    {
        lookAtTarget.LookAt(transform, target.transform);
        //CheckTargetsSide();
        switch (sM)
        {
            case StateMachine.Idle:
                // Dead
                if (health.GetIsDead()) sM = StateMachine.Dead;
                if (CheckTargetWithinRange()) sM = StateMachine.Combat;
                break;

            case StateMachine.Combat:
                // Go back to idle
                if (!CheckTargetWithinRange()) sM = StateMachine.Idle;
                // Shield
                if (CheckCanShield()) sM = StateMachine.Shield;
                // Chase
                if (CheckCanChase()) sM = StateMachine.ChasePlayer;
                // Special Attacks

                // check if can use special attack
                // check if chance to use sp true
                // check height to choose low or high sp atk

                // Normal Attacks && CheckNotInSpecialAttack()
                if (CheckCanAttack())
                {
                    if (CheckInMeleeRange()) sM = StateMachine.MeleeAttack;
                    else sM = StateMachine.RangeAttack;
                }

                break;

            case StateMachine.RangeAttack:
                Debug.Log("Used ranged attack");
                shoot.FireWeaponBoss(target.transform, stats.shotFoce, stats.resetNormalAttackTimer);
                sM = StateMachine.Idle;
                break;

            case StateMachine.MeleeAttack:
                Debug.Log("Used ranged attack");
                meleeAttack.UseMeleeAttack(stats.normalAttackDamage, stats.resetNormalAttackTimer);
                sM = StateMachine.Idle;
                break;

            case StateMachine.SpecialLowAttack:
                sM = StateMachine.Idle;
                break;

            case StateMachine.SpecialHighAttack:
                sM = StateMachine.Idle;
                break;

            case StateMachine.Shield:
                shield.UseShield();
                sM = StateMachine.Idle;
                break;

            case StateMachine.ChasePlayer:
                chasePlayer.Chase(target.transform, transform, stats.moveSpeed);
                sM = StateMachine.Idle;
                break;

            case StateMachine.Dead:
                break;

            default:
                break;
        }
    }
    private bool CheckTargetWithinRange()
    {
        float distance = Vector2.Distance(transform.position, target.transform.position);
        if (distance <= stats.bossFOV) 
            return true;
        return false;
    }
    private bool CheckCanShield()
    {
        if (health.GetBossCurrentHP() <= stats.dangerThreshold && shield.GetCurrentShieldsAmount() > 0)
        {
            if (!shield.GetShieldStatus() && shield.GetCanShield())
            {
                return true;
            }
            return false;
        }
        return false;
    }

    private bool CheckCanChase()
    {
        if (target != null)
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);
            if (distance > stats.chaseDistanceThreshold)
                return true;
        }
        return false;
    }

    private bool CheckCanAttack()
    {
        if (attackCooldown.GetCanNormalAttack())
        {
            return true;
        }
        return false;
    }

    private bool CheckNotInSpecialAttack()
    {
        if (!specialAttacks.GetIsInSpecialAttack())
            return true;
        return false;
    }

    private bool CheckInMeleeRange()
    {
        float distance = Vector2.Distance(transform.position, target.transform.position);
        if (distance < stats.meleeAttackRange)
            return true;
        return false;
    }
    /*
    private void CheckTargetsSide()
    {
        // Check which side the player is on
        float xDistanceFromTarget = transform.position.x - target.transform.position.x;
        if (!facingLeft && xDistanceFromTarget > 0)
        {
            FacePlayer();
        }
        if (facingLeft && xDistanceFromTarget < 0)
        {
            FacePlayer();
        }
    }
    private void FacePlayer()
    {
        // Look towards the player depending on X pos
        facingLeft = !facingLeft;
        transform.Rotate(0f, -180f, 0f);
        Debug.Log("Boss Flipped");
    }
    */
}
