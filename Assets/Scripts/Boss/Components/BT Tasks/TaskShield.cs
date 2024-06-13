using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class TaskShield : BTNode
{
    private Shield shield;
    public TaskShield(Shield shield)
    {
        this.shield = shield;
    }
    public override NodeState Evaluate()
    {
        shield.UseShield();
        state = NodeState.RUNNING;
        Debug.Log($"Task Shield state = {state}");
        return state;
    }
}
