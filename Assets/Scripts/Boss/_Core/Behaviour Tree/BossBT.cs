using UnityEngine;
using BehaviourTree;
using System.Collections.Generic;

public class BossBT : BTree
{
    [SerializeField] float speed, distanceThreshold, shotForce, dangerThreshold;
    [SerializeField] Transform player;
    [SerializeField] GameObject target;
    [SerializeField] float bossFOV;
    [SerializeField] LayerMask targetLayer;
    // Script Components
    [SerializeField] Shoot shoot;
    [SerializeField] ChasePlayer chasePlayer;
    [SerializeField] BossHealth health;
    [SerializeField] Shield shield;

 
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
                        new CheckDistance(transform, distanceThreshold),
                        new TaskChasePlayer(chasePlayer, transform, speed),
                    }),
                    // Attack the player
                    new BTSelector(new List<BTNode>
                    {
                        // Close Range Attack
                        new BTSequence(new List<BTNode> 
                        {
                            // Check in melee range
                            // Use melee attack
                        }),
                        // Long Range Attack
                        new BTSelector(new List<BTNode> 
                        {
                            new BTSequence(new List<BTNode>
                            { 
                                // Check can use special Attack
                                // Select Special Attack
                                new BTSelector(new List<BTNode>
                                { 
                                    // Task High Special Attack
                                    // Task Low Special Attack
                                })
                            }),
                            // Normal range attack
                            new TaskRangeAttackNormal(shoot, shotForce),
                        }),
                    }),
                }),
            }),
            new TaskIdle(),
        }) ;
        return root;
    }
}



