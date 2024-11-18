using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Municipal_Services_Application.Data_Structures
{
    public class Graph
    {
        // Dictionary to store the adjacency list of the graph
        private readonly Dictionary<int, List<int>> _adjacencyList = new Dictionary<int, List<int>>();

        // Method to add an edge from one node to another
        public void AddEdge(int from, int to)
        {
            // If the 'from' node does not exist in the adjacency list, add it
            if (!_adjacencyList.ContainsKey(from))
                _adjacencyList[from] = new List<int>();

            // Add the 'to' node to the adjacency list of the 'from' node
            _adjacencyList[from].Add(to);
        }
        //--------------------------------------------------------------------------------

        // Method to get the dependencies (adjacent nodes) of a given node
        public List<int> GetDependencies(int nodeId)
        {
            // Return the list of adjacent nodes if the node exists in the adjacency list, otherwise return an empty list
            return _adjacencyList.ContainsKey(nodeId) ? _adjacencyList[nodeId] : new List<int>();
        }
        //--------------------------------------------------------------------------------

        // Method to remove a node and its edges from the graph
        public void RemoveNode(int nodeId)
        {
            // If the node exists in the adjacency list, remove it
            if (_adjacencyList.ContainsKey(nodeId))
            {
                _adjacencyList.Remove(nodeId);
            }

            // Remove the node from the adjacency lists of all other nodes
            foreach (var key in _adjacencyList.Keys.ToList())
            {
                _adjacencyList[key].Remove(nodeId);
            }
        }
        //--------------------------------------------------------------------------------

        // Method to perform a topological sort on the graph
        public List<int> TopologicalSort()
        {
            // Set to keep track of visited nodes
            var visited = new HashSet<int>();
            // Stack to store the topologically sorted nodes
            var stack = new Stack<int>();
            // List to store the result of the topological sort
            var result = new List<int>();

            // Perform topological sort for each node in the adjacency list
            foreach (var node in _adjacencyList.Keys)
            {
                if (!visited.Contains(node))
                    TopologicalSortUtil(node, visited, stack);
            }

            // Pop all nodes from the stack and add them to the result list
            while (stack.Count > 0)
                result.Add(stack.Pop());

            // Return the result of the topological sort
            return result;
        }
        //--------------------------------------------------------------------------------

        // Utility method to perform a topological sort on a node
        private void TopologicalSortUtil(int node, HashSet<int> visited, Stack<int> stack)
        {
            // Mark the node as visited
            visited.Add(node);

            // Recursively perform topological sort for all adjacent nodes
            if (_adjacencyList.ContainsKey(node))
            {
                foreach (var neighbor in _adjacencyList[node])
                {
                    if (!visited.Contains(neighbor))
                        TopologicalSortUtil(neighbor, visited, stack);
                }
            }

            // Push the node onto the stack
            stack.Push(node);
        }
        //--------------------------------------------------------------------------------
    }


}
//------------------------------------------------------End of file------------------------------------------------------
