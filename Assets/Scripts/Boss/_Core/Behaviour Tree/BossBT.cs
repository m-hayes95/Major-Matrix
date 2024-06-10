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
            new BTSequence(new List<BTNode>
            {
                new CheckHasTarget(transform, bossFOV, targetLayer),
                new BTSequence(new List<BTNode>
                {
                    new CheckDistance(transform, distanceThreshold),
                    new TaskChasePlayer(chasePlayer, transform, speed)
                }),
            }),
            new TaskIdle()

        });
        return root;
    }
}



