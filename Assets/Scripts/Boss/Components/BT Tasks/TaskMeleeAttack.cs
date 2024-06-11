using UnityEngine;
using BehaviourTree;

public class TaskMeleeAttack : BTNode
{
    private MeleeAttack meleeAttack; 
    public TaskMeleeAttack(MeleeAttack meleeAttack)
    {
        this.meleeAttack = meleeAttack;
    }
    public override NodeState Evaluate()
    {
        meleeAttack.UseMeleeAttack();
        state = NodeState.RUNNING;
        Debug.Log($"Task Melee Attack state = {state}");
        return state;
    }
}
