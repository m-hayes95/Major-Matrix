using UnityEngine;
using BehaviourTree;

public class CheckCanAttack : BTNode
{
    private AttackCooldown attackCooldown;
    public CheckCanAttack(AttackCooldown attackCooldown)
    {
        this.attackCooldown = attackCooldown;
    }
    public override NodeState Evaluate()
    {
        if (attackCooldown.GetCanAttack())
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
