using UnityEngine;
using BehaviourTree;

public class TaskSpecialAttackLow : BTNode
{
    private int lowAttackIndex = 0;
    private SpecialAttacks spAtk;
    private Transform transform;
    private bool notInAttack;
    public TaskSpecialAttackLow(SpecialAttacks specialAttacks, Transform transform)
    {
        spAtk = specialAttacks;
        this.transform = transform;
    }

    public override NodeState Evaluate()
    {
        spAtk.CallSpecialAttackLowOrHigh(lowAttackIndex, transform);
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
