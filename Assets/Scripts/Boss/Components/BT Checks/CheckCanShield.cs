using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class CheckCanShield : BTNode
{
    private Shield shield;
    public CheckCanShield(Shield shield)
    {
        this.shield = shield;
    }
    public override NodeState Evaluate()
    {
        if (shield.GetCurrentShieldsAmount() > 0)
        {
            if (!shield.GetShieldStatus() && shield.GetCanShield())
            {
                state = NodeState.SUCCESS;
                Debug.Log($"Check Can Shield = {state}");
                return state;
            }
            return state = NodeState.FAILURE;
        }
        state = NodeState.FAILURE;
        Debug.Log($"Check Can Shield = {state}");
        return state;
    }
    
}
