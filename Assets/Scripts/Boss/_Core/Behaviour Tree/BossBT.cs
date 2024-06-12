using UnityEngine;
using BehaviourTree;
using System.Collections.Generic;

public class BossBT : BTree
{
    [SerializeField] float speed, chaseDistanceThreshold, shotForce, dangerThreshold, meleeDistanceThreshold;
    [SerializeField] float percentIncrease;
    [SerializeField] Transform player;
    [SerializeField] GameObject target;
    [SerializeField] float bossFOV;
    [SerializeField] LayerMask targetLayer;
    // Script Components
    [SerializeField] Shoot shoot;
    [SerializeField] ChasePlayer chasePlayer;
    [SerializeField] BossHealth health;
    [SerializeField] Shield shield;
    [SerializeField] MeleeAttack meleeAttack;
    [SerializeField] AttackCooldown attackCooldown;
    [SerializeField] RandomChance randomChance;
    [SerializeField] SpecialAttacks specialAttacks;
    [SerializeField] DistanceToTargetY distanceToTargetY;
    private BossStatsScriptableObject stats;

    private void Awake()
    {
        SetComponents();
    }
    private void SetComponents()
    {
        stats = GetComponent<BossStatsComponent>().bossStats;
    }

    protected override BTNode SetupTree()
    {
        BTNode root = new BTSelector(new List<BTNode>
        {
            // Combat or Idle
            new BTSequence(new List<BTNode>
            {
                // If has target, move down the tree
                new CheckHasTarget(transform, bossFOV, targetLayer),
                new BTSelector(new List<BTNode>
                {
                    // Defend with shield
                    new BTSequence(new List<BTNode>
                    {
                        new CheckCurrentHealth(health, dangerThreshold),
                        new CheckCanShield(shield),
                        new TaskShield(shield),
                    }),
                    // Chase the player
                    new BTSequence(new List<BTNode>
                    {
                        new CheckDistance(transform, chaseDistanceThreshold),
                        new TaskChasePlayer(chasePlayer, transform, speed),
                    }),
                    
                    // Attack the player
                    new BTSequence(new List<BTNode>
                    {
                        // First Check the boss can attack before trying, then select an attack to use
                        new CheckCanAttack(attackCooldown),
                        new BTSelector(new List<BTNode>
                        {
                         // Chosse close range Melee Attack
                            new BTSequence(new List<BTNode>
                            {
                            new CheckInMeleeRange(transform, meleeDistanceThreshold),
                            new TaskMeleeAttack(meleeAttack, stats.normalAttackDamage, stats.resetNormalAttackTimer),
                            }),
                            // Choose Special Attack
                            new BTSequence(new List<BTNode>
                            { 
                                new CheckUseSpecialAttacks(randomChance, percentIncrease, stats.chanceToUseSpecialAttack),
                                // Choose to use special high or low attack
                                new BTSelector(new List<BTNode>
                                { 
                                    // use special high attack
                                    //new BTSequence(new List<BTNode>
                                    //{
                                        // Check should attack high
                                        // Task High
                                    //}),
                                    // use special low attack
                                    new BTSequence(new List<BTNode>
                                    {
                                        new CheckTargetIsBelowSpecialAttackHeightThreshold(
                                            transform, distanceToTargetY, stats.specialHighAttackMinY
                                            ),
                                        new TaskSpecialAttackLow(specialAttacks),
                                    }),
                                }),
                            }),
                            // Choose Long Range Normal Attack
                            new TaskRangeAttackNormal(shoot, shotForce, stats.resetNormalAttackTimer),
                        }),
                    }),
                }),
            }),
            new TaskIdle(),
        }) ;
        return root;
    }

    
}



