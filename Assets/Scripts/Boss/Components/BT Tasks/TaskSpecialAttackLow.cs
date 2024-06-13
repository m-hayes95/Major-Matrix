using UnityEngine;
using BehaviourTree;

public class TaskSpecialAttackLow : BTNode
{
    private int lowAttackIndex = 0;
    private SpecialAttacks spAtk;
    private bool notInAttack;
    public TaskSpecialAttackLow(SpecialAttacks specialAttacks)
    {
        spAtk = specialAttacks;
    }

    public override NodeState Evaluate()
    {
        spAtk.CallSpecialAttackLowOrHigh(lowAttackIndex);
        state = NodeState.RUNNING;
        Debug.Log($"Task Use Special Attack Low state = {state}");
        return state;
        /*
        notInAttack = (bool)GetData("Can Attack");
        if (notInAttack)
        {
            
        }
        state = NodeState.FAILURE;
        Debug.Log($"Task Use Special Attack Low state = {state}");
        return state;
        */
    }
}
