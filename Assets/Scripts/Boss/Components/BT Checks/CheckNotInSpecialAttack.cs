using UnityEngine;
using BehaviourTree;

public class CheckNotInSpecialAttack : BTNode
{
    private SpecialAttacks specialAttacks;
    public CheckNotInSpecialAttack(SpecialAttacks specialAttacks) 
    { 
        this.specialAttacks = specialAttacks;
    }

    public override NodeState Evaluate()
    {
        if (!specialAttacks.GetIsInSpecialAttack())
        {
            state = NodeState.SUCCESS;
            Debug.Log($"Check Not In Special Attack state = {state}");
            return state;
        }
        
        state = NodeState.FAILURE;
        Debug.Log($"Check Not In Special Attack state = {state}");
        return state;
    }
}
