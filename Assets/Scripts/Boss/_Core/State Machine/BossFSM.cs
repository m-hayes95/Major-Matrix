using UnityEngine;

public class BossFSM : MonoBehaviour
{
    private enum StateMachine 
    { Idle, Combat, RangeAttack, MeleeAttack, SpecialLowAttack, SpecialHighAttack, Shield, ChasePlayer, Dead };

    [SerializeField] private StateMachine sM;
    [SerializeField] private GameObject target;
    // Type of Special Attacks
    private int lowAttackIndex = 0;
    private int highAttackIndex = 1;
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
    }

    private void Update()
    {
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
                // Chase (when not currently in a special attack)
                if (CheckCanChase() && CheckNotInSpecialAttack()) sM = StateMachine.ChasePlayer;
                // Normal Attacks && CheckNotInSpecialAttack()
                if (CheckCanAttack() && CheckNotInSpecialAttack())
                {
                    if (CheckCanUseSpecialAttack()) // There is an issue with the boss not doing anything before using a special attack
                    {
                        if (CheckChanceToUseSpecialAttack())
                        {
                            if (UseSpecialLowAttack()) sM = StateMachine.SpecialLowAttack;
                            else sM = StateMachine.SpecialHighAttack;
                        }
                    }
                    else if (CheckInMeleeRange())
                    {
                        sM = StateMachine.MeleeAttack;
                    }
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
                Debug.Log("Used special low attack");
                specialAttacks.CallSpecialAttackLowOrHigh(lowAttackIndex, transform);
                sM = StateMachine.Idle;
                break;

            case StateMachine.SpecialHighAttack:
                Debug.Log("Used special high attack");
                specialAttacks.CallSpecialAttackLowOrHigh(highAttackIndex, transform);
                sM = StateMachine.Idle;
                break;

            case StateMachine.Shield:
                Debug.Log("Used shield");
                shield.UseShield();
                sM = StateMachine.Idle;
                break;

            case StateMachine.ChasePlayer:
                Debug.Log("Chase target");
                chasePlayer.Chase(target.transform, transform, stats.moveSpeed);
                sM = StateMachine.Idle;
                break;

            case StateMachine.Dead:
                Debug.Log("Boss died");
                break;

            default:
                break;
        }
    }
    private float DistanceToTarget()
    {
        float distance = Vector2.Distance(transform.position, target.transform.position);
        return distance;
    }
    private bool CheckTargetWithinRange() // Check to see if the target is within the enemies field of view
    {
        if (DistanceToTarget() <= stats.bossFOV) 
            return true;
        return false;
    }
    private bool CheckCanShield() // If we have low enough health, have shields available and are not currently shield, then allow shielding
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

    private bool CheckCanChase() // If there is a target in view and the distance is more than the min amount, chase the target
    {
        if (target != null)
        {
            if (DistanceToTarget() > stats.chaseDistanceThreshold)
                return true;
        }
        return false;
    }

    private bool CheckCanAttack() // Can attack if not in a an attack currently
    {
        if (attackCooldown.GetCanNormalAttack()) return true;
        return false;
    }

    private bool CheckNotInSpecialAttack() // Don't allow other actions, whilst using a special attack
    {
        if (!specialAttacks.GetIsInSpecialAttack()) return true;
        return false;
    }

    private bool CheckInMeleeRange() // Is the player within the boss' reach
    {
        if (DistanceToTarget() < stats.meleeAttackRange)
            return true;
        return false;
    }

    private bool CheckCanUseSpecialAttack() // Can use special attack if not in a an attack currently
    {
        if (attackCooldown.GetCanSpecialAttack()) return true;
        return false;
    }
    private bool CheckChanceToUseSpecialAttack() // Every second, a random number is generated. If its more than the threshold then we can special attack
    {
        randomChance.ApplyRandomChanceOutOf100Percent(stats.percentIncrease);
        float randomNumber = randomChance.GetRandomNumber();
        if (randomNumber <= stats.chanceToUseSpecialAttack) return true;
        return false;
    }
    private bool UseSpecialLowAttack() // Check the height of the player, if the target is closer to the ground, use low attacks
    {
        if (distanceToTargetY.GetDistanceY(target.transform, transform) < stats.specialHighAttackMinY)
        {
            return true;
        }
        return false;
    }
}
