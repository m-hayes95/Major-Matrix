using UnityEngine;
using BehaviourTree;

public class CheckCanSpecialAttack : BTNode
{
    private AttackCooldown attackCooldown;  
    public CheckCanSpecialAttack(AttackCooldown attackCooldown) 
    {
        this.attackCooldown = attackCooldown;
    }

    public override NodeState Evaluate()
    {
        if (attackCooldown.GetCanSpecialAttack())
        {
            state = NodeState.SUCCESS;
            Debug.Log($"Check Can Attack state = {state}");
            return state;
        }
        state = NodeState.FAILURE;
        Debug.Log($"Check Can Attack state = {state}");
        return state;
    }

}
