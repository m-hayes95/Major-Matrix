using System.Collections.Generic;
namespace BehaviourTree
{
    public class BTSequence : BTNode
    {
        // Create a composite node to evaluate each childs node state, and only return success if all states return success
        // (similar to AND logic gate)
       
        // Class constructors to be used within the BT
        public BTSequence()  : base() { }   
        public BTSequence(List<BTNode> children) : base(children) { }
        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false;
            // Override Nodestate evaluate method and iterate through each child to check its nodestate value
            foreach (BTNode node in children)
            {
                switch(node.Evaluate())
                {
                    // If next child returns node state of failure, set the sequence parent node to failure too
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    // If next child returns success, continue
                    case NodeState.SUCCESS:
                        continue;
                    // If next child returns running, set the running bool to true
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    // If all children returned success, return success
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }
            // If running bool is true, set the parent node state to running,
            // else set the node state to success
            state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state; 
        }
    }
}

