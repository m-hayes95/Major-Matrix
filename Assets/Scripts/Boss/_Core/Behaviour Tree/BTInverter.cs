using System.Collections.Generic;
using BehaviourTree;

public class BTInverter : BTNode
{
    // Inverter returns fail if successful, and successful if fail
    public BTInverter() : base() { }
    public BTInverter(List<BTNode> children) : base(children) { }

    public override NodeState Evaluate()
    {
        foreach (BTNode node in children)
        {
            switch (node.Evaluate())
            {
                case NodeState.FAILURE:
                    state = NodeState.SUCCESS;
                    return state;
                case NodeState.SUCCESS:
                    state = NodeState.FAILURE;
                    return state;
                case NodeState.RUNNING:
                    state = NodeState.RUNNING;
                    return state;
                default:
                    state = NodeState.FAILURE;
                    return state;
            }
        }
        // If there is no children nodes
        state = NodeState.FAILURE;
        return state;
    }

}
