using UnityEngine;
using BehaviourTree;
using System.Collections.Generic;

public class BossBT : BTree
{
    protected override BTNode SetupTree()
    {
        throw new System.NotImplementedException();
        // Add sequencer and check for player distance
            // if player is within distance, melee attack player, if not then shoot at the player
    }
}



