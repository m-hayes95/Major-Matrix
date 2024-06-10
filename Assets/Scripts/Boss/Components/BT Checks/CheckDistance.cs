using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDistance : BTNode
{
    private Transform boss, player;
    private float distanceThres;
    public CheckDistance(Transform bossPos, float distanceThreshold)
    {
        boss = bossPos;
        distanceThres = distanceThreshold;
    }
    public override NodeState Evaluate()
    {
        player = (Transform)GetData("Target");
        float distance = 
                Vector2.Distance(boss.position, player.transform.position);
        if (distance > distanceThres) state = NodeState.SUCCESS;
        else state = NodeState.FAILURE;
        Debug.Log($"Check Distance state = {state}");
        return state; 

    }
}
