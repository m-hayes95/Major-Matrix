using System.Collections.Generic;
namespace BehaviourTree
{
    public enum NodeState { RUNNING, SUCCESS, FAILURE, }

    public class BTNode
    {
        protected NodeState state;
        public BTNode parent;
        protected List<BTNode> children = new List<BTNode>();
        private Dictionary<string, object> dataContext = new Dictionary<string, object>();
        public BTNode()
        {
            parent = null;
        }
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
        public virtual NodeState Evaluate() => NodeState.FAILURE;
        public void SetData(string key, object value)
        {
            dataContext[key] = value;
        }
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
    }
}
