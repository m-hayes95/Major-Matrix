using System.Collections.Generic;
namespace BehaviourTree
{
    public class BTSequence : BTNode
    {
        public BTSequence()  : base() { }   
        public BTSequence(List<BTNode> children) : base(children) { }
        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false;
            foreach (BTNode node in children)
            {
                switch(node.Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }
            state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state; 
        }
    }
}

