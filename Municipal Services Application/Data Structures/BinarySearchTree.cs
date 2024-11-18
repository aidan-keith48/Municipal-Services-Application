using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Municipal_Services_Application.Data_Structures
{
    public class BinarySearchTree
    {
        private class TreeNode
        {
            public ServiceRequest Value;
            public TreeNode Left;
            public TreeNode Right;

            public TreeNode(ServiceRequest value)
            {
                Value = value;
            }
        }

        private TreeNode _root;

        public void Insert(ServiceRequest request)
        {
            _root = Insert(_root, request);
        }

        private TreeNode Insert(TreeNode node, ServiceRequest request)
        {
            if (node == null)
                return new TreeNode(request);

            if (request.RequestId < node.Value.RequestId)
                node.Left = Insert(node.Left, request);
            else if (request.RequestId > node.Value.RequestId)
                node.Right = Insert(node.Right, request);

            return node;
        }

        public ServiceRequest Search(int requestId)
        {
            var node = Search(_root, requestId);
            return node?.Value;
        }

        private TreeNode Search(TreeNode node, int requestId)
        {
            if (node == null || node.Value.RequestId == requestId)
                return node;

            if (requestId < node.Value.RequestId)
                return Search(node.Left, requestId);

            return Search(node.Right, requestId);
        }

        public void Remove(int requestId)
        {
            _root = Remove(_root, requestId);
        }

        private TreeNode Remove(TreeNode node, int requestId)
        {
            if (node == null)
                return null;

            if (requestId < node.Value.RequestId)
                node.Left = Remove(node.Left, requestId);
            else if (requestId > node.Value.RequestId)
                node.Right = Remove(node.Right, requestId);
            else
            {
                if (node.Left == null)
                    return node.Right;
                if (node.Right == null)
                    return node.Left;

                var minNode = GetMinNode(node.Right);
                node.Value = minNode.Value;
                node.Right = Remove(node.Right, minNode.Value.RequestId);
            }

            return node;
        }

        private TreeNode GetMinNode(TreeNode node)
        {
            while (node.Left != null)
                node = node.Left;

            return node;
        }
    }
}
