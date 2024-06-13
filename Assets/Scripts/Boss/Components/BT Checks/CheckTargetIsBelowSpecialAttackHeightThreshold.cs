using UnityEngine;
using BehaviourTree;
using System;

public class CheckTargetIsBelowSpecialAttackHeightThreshold : BTNode
{
    private Transform transform;
    private DistanceToTargetY distance;
    float distanceThreshold;
    private const string DISTNACE_Y = "Distance to target Y";
    public CheckTargetIsBelowSpecialAttackHeightThreshold(Transform thisTransform, DistanceToTargetY distanceToTargetY, float distanceThreshold)
    {
        transform = thisTransform;
        distance = distanceToTargetY;
        this.distanceThreshold = distanceThreshold;
    }

    public override NodeState Evaluate()
    {
        // Get target data from blackboard and share distance on Y axis
        Transform target = (Transform)GetData("Target");
        //parent.parent.SetData(DISTNACE_Y, distance.GetDistanceY(target, transform));
        //float distanceY = (float)GetData(DISTNACE_Y);
        
        if (distance.GetDistanceY(target, transform) < distanceThreshold)
        {
            state = NodeState.SUCCESS;
            Debug.Log($"Check Target Y Distance state = {state}");
            return state;
        }
        else 
        {
            state = NodeState.FAILURE;
            Debug.Log($"Check Target Y Distance state = {state}");
            return state;
        }
    }
}
