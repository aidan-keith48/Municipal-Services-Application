using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Municipal_Services_Application.Data_Structures
{
    public class BinarySearchTree
    {
        // Nested class representing a node in the binary search tree
        private class TreeNode
        {
            // The value stored in the node, which is a ServiceRequest object
            public ServiceRequest Value;
            // Reference to the left child node
            public TreeNode Left;
            // Reference to the right child node
            public TreeNode Right;

            // Constructor to initialize the node with a ServiceRequest value
            public TreeNode(ServiceRequest value)
            {
                Value = value;
            }
        }
        //----------------------------------------------------------------------------------

        // Root node of the binary search tree
        private TreeNode _root;

        // Public method to insert a new ServiceRequest into the tree
        public void Insert(ServiceRequest request)
        {
            // Call the private recursive insert method starting from the root
            _root = Insert(_root, request);
        }
        //----------------------------------------------------------------------------------

        // Private recursive method to insert a new ServiceRequest into the tree
        private TreeNode Insert(TreeNode node, ServiceRequest request)
        {
            // If the current node is null, create a new node with the ServiceRequest
            if (node == null)
                return new TreeNode(request);

            // If the request ID is less than the current node's request ID, insert into the left subtree
            if (request.RequestId < node.Value.RequestId)
                node.Left = Insert(node.Left, request);
            // If the request ID is greater than the current node's request ID, insert into the right subtree
            else if (request.RequestId > node.Value.RequestId)
                node.Right = Insert(node.Right, request);

            // Return the current node to link the tree correctly
            return node;
        }
        //----------------------------------------------------------------------------------

        // Public method to search for a ServiceRequest by its request ID
        public ServiceRequest Search(int requestId)
        {
            // Call the private recursive search method starting from the root
            var node = Search(_root, requestId);
            // Return the value of the found node, or null if not found
            return node?.Value;
        }
        //----------------------------------------------------------------------------------

        // Private recursive method to search for a node by its request ID
        private TreeNode Search(TreeNode node, int requestId)
        {
            // If the current node is null or matches the request ID, return the node
            if (node == null || node.Value.RequestId == requestId)
                return node;

            // If the request ID is less than the current node's request ID, search in the left subtree
            if (requestId < node.Value.RequestId)
                return Search(node.Left, requestId);

            // Otherwise, search in the right subtree
            return Search(node.Right, requestId);
        }
        //----------------------------------------------------------------------------------

        // Public method to remove a ServiceRequest by its request ID
        public void Remove(int requestId)
        {
            // Call the private recursive remove method starting from the root
            _root = Remove(_root, requestId);
        }
        //----------------------------------------------------------------------------------

        // Private recursive method to remove a node by its request ID
        private TreeNode Remove(TreeNode node, int requestId)
        {
            // If the current node is null, return null (base case)
            if (node == null)
                return null;

            // If the request ID is less than the current node's request ID, remove from the left subtree
            if (requestId < node.Value.RequestId)
                node.Left = Remove(node.Left, requestId);
            // If the request ID is greater than the current node's request ID, remove from the right subtree
            else if (requestId > node.Value.RequestId)
                node.Right = Remove(node.Right, requestId);
            // If the current node matches the request ID, remove this node
            else
            {
                // If the node has no left child, return the right child
                if (node.Left == null)
                    return node.Right;
                // If the node has no right child, return the left child
                if (node.Right == null)
                    return node.Left;

                // If the node has two children, find the minimum node in the right subtree
                var minNode = GetMinNode(node.Right);
                // Replace the current node's value with the minimum node's value
                node.Value = minNode.Value;
                // Remove the minimum node from the right subtree
                node.Right = Remove(node.Right, minNode.Value.RequestId);
            }

            // Return the current node to link the tree correctly
            return node;
        }
        //----------------------------------------------------------------------------------

        // Private method to find the minimum node in a subtree
        private TreeNode GetMinNode(TreeNode node)
        {
            // Traverse to the leftmost node
            while (node.Left != null)
                node = node.Left;

            // Return the leftmost (minimum) node
            return node;
        }
        //----------------------------------------------------------------------------------
    }
}
