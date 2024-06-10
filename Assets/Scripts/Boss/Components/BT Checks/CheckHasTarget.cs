using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHasTarget : BTNode
{
    private LayerMask targetLayer;
    private Transform transform;
    private float bossFOV;
    private const string TARGET = "Target";
    public CheckHasTarget(Transform transform, float bossFOV, LayerMask targetLayer)
    {
        this.transform = transform;
        this.bossFOV = bossFOV;
        this.targetLayer = targetLayer;
    }

    public override NodeState Evaluate()
    {  
        object target = GetData(TARGET);
        if (target == null)
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(
                transform.position, new Vector2(bossFOV, bossFOV), 0, targetLayer
                );
            if (collider2Ds.Length > 0)
            {
                parent.parent.SetData(TARGET, collider2Ds[0].transform);
                Debug.Log($"Boss In Combat with {collider2Ds[0].gameObject.name}");
                state = NodeState.SUCCESS;
                return state;
            }
            state = NodeState.FAILURE;
            return state;
        }
        if (target != null) Debug.Log($"Boss has target {target}");
        Debug.Log($"Check Has Target state = {state}");
        state = NodeState.SUCCESS;
        
        return state;
    }
}
