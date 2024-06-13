using System.Collections.Generic;
namespace BehaviourTree
{
    public class BTSelector : BTNode
    {
        // Create a composite node to evaluate each childs node state, and return the first success or running
        // (similar to OR logic gate)

        // Constructors
        public BTSelector() : base() { }
        public BTSelector(List<BTNode> children) : base(children) { }
        public override NodeState Evaluate()
        {
            // Iterate through each of the child nodes to check their node state
            foreach (BTNode node in children)
            {
                switch(node.Evaluate())
                {
                    // If node state is failure, move to next node
                    case NodeState.FAILURE:
                        continue;
                    // On the first node state that returns success, return success
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    // On the first node state that returns running, return running
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }
            // If all child nodes return failure, return node state failure
            state = NodeState.FAILURE;
            return state;
        }
    }
}

