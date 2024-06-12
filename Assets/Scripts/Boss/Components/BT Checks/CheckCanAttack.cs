using UnityEngine;
using BehaviourTree;

public class CheckCanAttack : BTNode
{
    private AttackCooldown attackCooldown;
    private const string CAN_ATTACK = "Can Attack";
    public CheckCanAttack(AttackCooldown attackCooldown)
    {
        this.attackCooldown = attackCooldown;
    }
    public override NodeState Evaluate()
    {
        parent.parent.SetData(CAN_ATTACK, attackCooldown.GetCanAttack());
        bool canAttack = (bool)GetData(CAN_ATTACK);

        if (canAttack)
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
