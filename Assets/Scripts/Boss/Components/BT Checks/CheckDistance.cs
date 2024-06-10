using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDistance : BTNode
{
    private Transform boss, player;
    private float distanceThres;
    public CheckDistance(Transform bossPos, Transform playerPos, float distanceThreshold)
    {
        boss = bossPos;
        player = playerPos;
        distanceThres = distanceThreshold;
    }
    public override NodeState Evaluate()
    {
        float distance = 
                Vector2.Distance(boss.position, player.transform.position);
        if (distance > distanceThres) state = NodeState.SUCCESS;
        else state = NodeState.FAILURE;
        Debug.Log($"Check Distance state = {state}");
        return state; 

    }
}
