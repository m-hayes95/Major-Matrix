using UnityEngine;
using BehaviourTree;
using System.Collections.Generic;

public class BossBT : BTree
{
    [SerializeField] float speed, distanceThreshold, shotForce;
    [SerializeField] Transform player;
    [SerializeField] GameObject target;
    [SerializeField] Shoot shoot;
    [SerializeField] ChasePlayer chasePlayer;
    [SerializeField] float bossFOV;
    [SerializeField] LayerMask targetLayer;
 
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
                    //new BTSequence(new List<BTNode>
                    //{
                        // Check Has low health
                        // Check Can shield
                        // Task Shield
                    //}),
                    // Chase the player
                    new BTSequence(new List<BTNode>
                    {
                        new CheckDistance(transform, distanceThreshold),
                        new TaskChasePlayer(chasePlayer, transform, speed),
                    }),
                    // Attack the player
                    new BTSelector(new List<BTNode>
                    {
                        // In melee Range -> Melee Attack
                        // Use special Attack -> Sel -> Check Distance Y -> Low Attack / High Attack
                        new TaskRangeAttackNormal(shoot, shotForce),
                    }),
                }),
            }),
            new TaskIdle(),
        }) ;
        return root;
    }
}



