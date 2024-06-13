using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskIdle : BTNode
{
    public TaskIdle()
    {

    }
    public override NodeState Evaluate()
    {
        Debug.Log($"Task Idle state = {state}");
        state = NodeState.RUNNING;
        return state;
    }
}
