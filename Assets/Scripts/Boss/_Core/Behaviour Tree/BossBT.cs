using UnityEngine;
using BehaviourTree;
using System.Collections.Generic;

public class BossBT : BTree
{
    [SerializeField] float speed, distanceThreshold, shotForce;
    [SerializeField] GameObject player;
    [SerializeField] Transform bossPos;
    [SerializeField] Shoot shoot;

    protected override BTNode SetupTree()
    {
        BTNode root = new BTSelector(new List<BTNode>
        {
            new BTSequence(new List<BTNode>
            {
                new CheckDistance(bossPos, player.transform, distanceThreshold),
                new TaskChasePlayer(player, bossPos, speed)
            }),
            new TaskRangeAttackNormal(player.transform, shoot, shotForce)
        });
        return root;
    }
}



