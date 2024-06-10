using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskChasePlayer : BTNode
{

    private GameObject target;
    private Transform currentPos;
    private float moveSpeed;
    public TaskChasePlayer(GameObject targetGameObject, Transform selfPosition, float speed)
    {
        target = targetGameObject;
        currentPos = selfPosition;
        moveSpeed = speed;
    }

    public override NodeState Evaluate()
    {
        // call ai move to function
        Vector2 playerXPos = new Vector2(target.transform.position.x, currentPos.position.y);
        currentPos.position =
            Vector2.MoveTowards(currentPos.position, playerXPos, moveSpeed * Time.deltaTime);
        state = NodeState.RUNNING;
        Debug.Log($"Task case player state = {state}");
        return state;
    }
}
