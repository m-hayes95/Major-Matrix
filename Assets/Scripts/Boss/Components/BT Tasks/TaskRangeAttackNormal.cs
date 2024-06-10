using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskRangeAttackNormal : BTNode
{
    private Transform target;
    private Shoot shoot;
    private float shotForce;
    public TaskRangeAttackNormal(Transform target, Shoot shootClass, float shotForce)
    {
        this.target = target;
        shoot = shootClass;
        this.shotForce = shotForce;
    }

    public override NodeState Evaluate()
    {
        shoot.FireWeaponBoss(target, shotForce);
        state = NodeState.RUNNING;
        Debug.Log($"Task Range Attack Normal state = {state}");
        return state;
    }
}
