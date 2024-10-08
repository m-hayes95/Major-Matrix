using BehaviourTree;
using UnityEngine;

public class TaskRangeAttackNormal : BTNode
{
    private Transform target;
    private Shoot shoot;
    private float shotForce;
    private float timer;
    private bool notInAttack;
    public TaskRangeAttackNormal(Shoot shootClass, float shotForce, float attackResetTimer)
    {
        shoot = shootClass;
        this.shotForce = shotForce;
        timer = attackResetTimer;
    }

    public override NodeState Evaluate()
    {
        target = (Transform)GetData("Target");
        shoot.FireWeaponBoss(target, shotForce, timer);
        state = NodeState.RUNNING;
        Debug.Log($"Task Range Attack Normal state = {state}");
        return state;
        /*
        //notInAttack = (bool)GetData("Can Attack");
        if (notInAttack)
        {
            
        }
        state = NodeState.FAILURE;
        Debug.Log($"Task Range Attack Normal state = {state}");
        return state;
        */
    }
}
