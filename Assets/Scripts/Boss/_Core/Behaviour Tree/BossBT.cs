using UnityEngine;
using BehaviourTree;
using System.Collections.Generic;
[RequireComponent(typeof(BossStatsComponent))]
public class BossBT : BTree
{
    // Components References
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
        SetGetComponents();
    }
    private void SetGetComponents()
    {
        // Get Scripts
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

    // Behaviour Tree
    protected override BTNode SetupTree()
    {
        BTNode root = new BTSelector(new List<BTNode>
        {
            // Combat or Idle
            new BTSequence(new List<BTNode>
            {
                // If has target, move down the tree
                new CheckHasTarget(
                    transform, stats.bossFOV_BT, stats.targetLayerMask, health
                    ),
                new BTSelector(new List<BTNode>
                {
                    // Defend with shield
                    new BTSequence(new List<BTNode>
                    {
                        new CheckCurrentHealth(health, stats.dangerThreshold),
                        new CheckCanShield(shield),
                        new TaskShield(shield),
                    }),
                    // Chase the player
                    new BTSequence(new List<BTNode>
                    {
                        new CheckDistance(transform, stats.chaseDistanceThreshold),
                        new TaskChasePlayer(chasePlayer, transform, stats.moveSpeed),
                    }),
                    
                    // Attack the player
                    new BTSequence(new List<BTNode>
                    {
                        // First Check the boss can attack before trying, then select an attack to use
                        new CheckCanNormalAttack(attackCooldown),
                        new BTSelector(new List<BTNode>
                        {
                         // Choose close range Melee Attack
                            new BTSequence(new List<BTNode>
                            {
                            new CheckNotInSpecialAttack(specialAttacks),
                            new CheckInMeleeRange(transform, stats.meleeAttackRange),
                            new TaskMeleeAttack(meleeAttack, stats.normalAttackDamage, stats.resetNormalAttackTimer),
                            }),
                            // Choose Special Attacks
                            new BTSequence(new List<BTNode>
                            { 
                                new CheckCanSpecialAttack(attackCooldown),
                                new CheckChanceToUseSpecialAttacks(randomChance, stats.percentIncrease, stats.chanceToUseSpecialAttack),
                                // Choose to use special high or low attack
                                new BTSelector(new List<BTNode>
                                { 
                                    // use special low attack
                                    new BTSequence(new List<BTNode>
                                    {
                                        new CheckTargetIsBelowSpecialAttackHeightThreshold(
                                            transform, distanceToTargetY, stats.specialHighAttackMinY
                                            ),
                                        new TaskSpecialAttackLow(specialAttacks, transform),
                                    }),
                                    // use special high attack
                                    new BTSequence(new List<BTNode>
                                    {
                                        new BTInverter(new List<BTNode>()
                                        {
                                            new CheckTargetIsBelowSpecialAttackHeightThreshold(
                                            transform, distanceToTargetY, stats.specialHighAttackMinY
                                            ),
                                        }),
                                        new TaskSpecialAttackHigh(specialAttacks, transform),
                                    }),
                                }),
                            }),
                            // Choose Long Range Normal Attack
                            new BTSequence( new List<BTNode>()
                            {
                                new CheckNotInSpecialAttack(specialAttacks),
                                new TaskRangeAttackNormal(shoot, stats.shotFoce, stats.resetNormalAttackTimer),
                            }),
                        }),
                    }),
                }),
            }),
            new TaskIdle(),
        }) ;
        return root;
    }
}



