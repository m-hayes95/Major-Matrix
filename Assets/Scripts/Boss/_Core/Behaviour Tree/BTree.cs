using UnityEngine;
namespace BehaviourTree
{
    public abstract class BTree : MonoBehaviour
    {
        private BTNode root = null;
        protected void Start()
        {
            root = SetupTree();
        }
        private void Update()
        {
            if (root != null)
                root.Evaluate();
        }
        protected abstract BTNode SetupTree();
    }
}


