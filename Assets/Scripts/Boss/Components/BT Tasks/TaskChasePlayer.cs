using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskChasePlayer : BTNode
{

    private GameObject target;
    public TaskChasePlayer(GameObject targetGameObject)
    {
        target = targetGameObject;
    }

    public override NodeState Evaluate()
    {
        // call ai move to function

        state = NodeState.RUNNING;
        return state;
    }
}
