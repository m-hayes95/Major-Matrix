using UnityEngine;
using BehaviourTree;

public class CheckCanNormalAttack : BTNode
{
    private AttackCooldown attackCooldown;
    private const string CAN_ATTACK = "Can Attack";
    public CheckCanNormalAttack(AttackCooldown attackCooldown)
    {
        this.attackCooldown = attackCooldown;
    }
    public override NodeState Evaluate()
    {
        //parent.parent.SetData(CAN_ATTACK, attackCooldown.GetCanAttack());
        //bool canAttack = (bool)GetData(CAN_ATTACK);

        if (attackCooldown.GetCanNormalAttack())
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
