using UnityEngine;
using BehaviourTree;

public class TaskMeleeAttack : BTNode
{
    private MeleeAttack meleeAttack;
    private float damage;
    private float timer;
    public TaskMeleeAttack(MeleeAttack meleeAttack, float damageToTarget, float attackCooldownTimer)
    {
        this.meleeAttack = meleeAttack;
        damage = damageToTarget;
        timer = attackCooldownTimer;
    }
    public override NodeState Evaluate()
    {
        meleeAttack.UseMeleeAttack(damage, timer);
        state = NodeState.RUNNING;
        Debug.Log($"Task Melee Attack state = {state}");
        return state;
    }
}
