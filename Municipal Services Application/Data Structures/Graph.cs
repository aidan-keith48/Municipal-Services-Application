using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Municipal_Services_Application.Data_Structures
{
    public class Graph
    {
        private readonly Dictionary<int, List<int>> _adjacencyList = new Dictionary<int, List<int>>();

        public void AddEdge(int from, int to)
        {
            if (!_adjacencyList.ContainsKey(from))
                _adjacencyList[from] = new List<int>();

            _adjacencyList[from].Add(to);
        }

        public List<int> GetDependencies(int nodeId)
        {
            return _adjacencyList.ContainsKey(nodeId) ? _adjacencyList[nodeId] : new List<int>();
        }

        public void RemoveNode(int nodeId)
        {
            if (_adjacencyList.ContainsKey(nodeId))
            {
                _adjacencyList.Remove(nodeId);
            }

            foreach (var key in _adjacencyList.Keys)
            {
                _adjacencyList[key].Remove(nodeId);
            }
        }

        public List<int> TopologicalSort()
        {
            var visited = new HashSet<int>();
            var stack = new Stack<int>();
            var result = new List<int>();

            foreach (var node in _adjacencyList.Keys)
            {
                if (!visited.Contains(node))
                    TopologicalSortUtil(node, visited, stack);
            }

            while (stack.Count > 0)
                result.Add(stack.Pop());

            return result;
        }

        private void TopologicalSortUtil(int node, HashSet<int> visited, Stack<int> stack)
        {
            visited.Add(node);

            if (_adjacencyList.ContainsKey(node))
            {
                foreach (var neighbor in _adjacencyList[node])
                {
                    if (!visited.Contains(neighbor))
                        TopologicalSortUtil(neighbor, visited, stack);
                }
            }

            stack.Push(node);
        }
    }


}
