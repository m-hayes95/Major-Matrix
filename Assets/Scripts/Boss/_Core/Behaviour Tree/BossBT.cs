using UnityEngine;
using BehaviourTree;
using System.Collections.Generic;

public class BossBT : BTree
{
    [SerializeField] float speed, chaseDistanceThreshold, shotForce, dangerThreshold, meleeDistanceThreshold;
    [SerializeField] float percentIncrease, specialAttackPercentThresold;
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
    private BossStatsScriptableObject stats;

    private void Awake()
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
                            new TaskMeleeAttack(meleeAttack),
                            }),
                            // Choose Special Attack
                            new BTSequence(new List<BTNode>
                            { 
                                // new CheckUseSpecialAttacks(randomChance, percentIncrease, specialAttackPercentThresold),
                                // Selector
                            }),
                            // Choose Long Range Normal Attack
                            new TaskRangeAttackNormal(shoot, shotForce, stats.resetAttackTimer),
                    }),
                    }),


                    
                }),
            }),
            new TaskIdle(),
        }) ;
        return root;
    }
}



