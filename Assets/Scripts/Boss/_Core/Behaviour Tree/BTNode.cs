using System.Collections.Generic;
using System.Diagnostics;
namespace BehaviourTree
{
    // Possible states for each node of the BT
    public enum NodeState { RUNNING, SUCCESS, FAILURE, }

    public class BTNode
    {
        protected NodeState state;
        // Store values of parent and child nodes
        public BTNode parent;
        protected List<BTNode> children = new List<BTNode>();
        // BT blackboard for shared storage of infomation
        private Dictionary<string, object> dataContext = new Dictionary<string, object>();
        // Set the defalut value of nodes in empty constuctors to null
        public BTNode() { parent = null; }
        // Link each created child node to it's parent node
        public BTNode(List<BTNode> children)
        {
            foreach (BTNode child in children)
                Attach(child);
        }
        private void Attach(BTNode child)
        {
            child.parent = this;
            children.Add(child);
        }
        // Override-able method to create each nodes behaviour, returns NodeState's current state (default to Failure).
        public virtual NodeState Evaluate() => NodeState.FAILURE;

        // Methods to add, get or clear data from the shared blackboard
        public void SetData(string key, object value)
        {
            dataContext[key] = value;
        }
        // Get and clear data, check if there is stored data somewhere within the tree by checking each parent node up the tree
        public object GetData(string key)
        {
            object value = null;
            if (dataContext.TryGetValue(key, out value))
                return value;

            BTNode node = parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value !=null) 
                    return value;
                node = node.parent; 
            }
            return null;
        }
        public bool ClearData(string key)
        {
            if (dataContext.ContainsKey(key))
            {
                dataContext.Remove(key);
                return true;
            }
            BTNode node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                {
                    return true;
                }
                node = node.parent;
            }
            return false;
        }

        public Dictionary<string, object> GetBlackboardData()
        {
            return dataContext;
        }
    }
}
