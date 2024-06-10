using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskChasePlayer : BTNode
{
    private ChasePlayer chasePlayer;
    private Transform currentPos;
    private float moveSpeed;
    public TaskChasePlayer(ChasePlayer chasePlayer, Transform currentPos, float speed)
    {
        //this.targetPos = targetPos;
        this.currentPos = currentPos;
        moveSpeed = speed;
        this.chasePlayer = chasePlayer;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("Target");
        chasePlayer.Chase(target, currentPos, moveSpeed);
        
        state = NodeState.RUNNING;
        Debug.Log($"Task case player state = {state}");
        return state;
    }
}
