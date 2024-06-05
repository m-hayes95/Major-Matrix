using System.Collections.Generic;
namespace BehaviourTree
{
    public class BTSelector : BTNode
    {
        public BTSelector() : base() { }
        public BTSelector(List<BTNode> children) : base(children) { }
        public override NodeState Evaluate()
        {
            foreach (BTNode node in children)
            {
                switch(node.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }
            state = NodeState.FAILURE;
            return state;
        }
    }
}

