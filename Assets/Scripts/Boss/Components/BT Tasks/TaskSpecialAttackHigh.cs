using UnityEngine;
using BehaviourTree;

public class TaskSpecialAttackHigh : BTNode
{
    private int highAttackIndex = 1;
    private SpecialAttacks spAtk;
    private Transform transform;
    private bool notInAttack;
    public TaskSpecialAttackHigh(SpecialAttacks specialAttacks, Transform transform)
    {
        spAtk = specialAttacks;
        this.transform = transform;
    }

    public override NodeState Evaluate()
    {
        spAtk.CallSpecialAttackLowOrHigh(highAttackIndex, transform);
        state = NodeState.RUNNING;
        Debug.Log($"Task Use Special Attack High state = {state}");
        return state;
        /*
        notInAttack = (bool)GetData("Can Attack");
        if (notInAttack)
        {
            spAtk.CallSpecialAttackLowOrHigh(highAttackIndex);
            state = NodeState.RUNNING;
            Debug.Log($"Task Use Special Attack High state = {state}");
            return state;
        }
        state = NodeState.FAILURE;
        Debug.Log($"Task Use Special Attack High state = {state}");
        return state;*/
    }
}
