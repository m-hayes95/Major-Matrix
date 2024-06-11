using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCurrentHealth : BTNode 
{
    private BossHealth health;
    private float dangerThreshold;
    public CheckCurrentHealth(BossHealth bossHealth, float dangerThreshold)
    {
        health = bossHealth;
        this.dangerThreshold = dangerThreshold;
    }

    public override NodeState Evaluate()
    {
        if (health == null)
        {
            return state = NodeState.FAILURE;
        }
        // If current health is less than threshold, move to next node, else return failure
        if (health.GetBossCurrentHP() <= dangerThreshold)
        {
            state = NodeState.SUCCESS;
            Debug.Log($"Check Current Health state = {state}");
            return state;
        }
        state = NodeState.FAILURE;
        Debug.Log($"Check Current Health state = {state}");
        return state;
    }
}
