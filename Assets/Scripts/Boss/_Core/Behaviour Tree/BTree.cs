using UnityEngine;
namespace BehaviourTree
{
    public abstract class BTree : MonoBehaviour
    {
        // Set the root (first node) of the tree
        private BTNode root = null;
        protected void Start()
        {
            root = SetupTree();
        }
        // If a tree exists, continuously evaluate it

        private void Update()
        {
            if (root != null)
                root.Evaluate();
        }
        protected abstract BTNode SetupTree();
    }
}


