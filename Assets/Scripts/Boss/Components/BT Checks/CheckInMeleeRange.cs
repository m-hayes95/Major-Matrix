using UnityEngine;
using BehaviourTree;

public class CheckInMeleeRange : BTNode
{
    private Transform boss, player;
    private float distanceThres;
    public CheckInMeleeRange(Transform bossPos, float distanceThreshold)
    {
        boss = bossPos;
        distanceThres = distanceThreshold;
    }
    public override NodeState Evaluate()
    {
        player = (Transform)GetData("Target");
        float distance =
                Vector2.Distance(boss.position, player.transform.position);
        if (distance < distanceThres) state = NodeState.SUCCESS;
        else state = NodeState.FAILURE;
        Debug.Log($"Check In Melee Range state = {state}");
        return state;

    }
}
