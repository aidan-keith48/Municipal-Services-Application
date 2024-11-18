using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Municipal_Services_Application.Data_Structures
{
    public class MaxHeap
    {
        // List to store heap elements
        private readonly List<ServiceRequest> _heap = new List<ServiceRequest>();

        // Property to get the number of elements in the heap
        public int Count => _heap.Count;

        // Method to insert a new element into the heap
        public void Insert(ServiceRequest request)
        {
            // Add the new element to the end of the list
            _heap.Add(request);
            // Restore the heap property by moving the new element up
            HeapifyUp(_heap.Count - 1);
        }
        //---------------------------------------------------------------

        // Method to extract the maximum element (root) from the heap
        public ServiceRequest ExtractMax()
        {
            // If the heap is empty, return null
            if (_heap.Count == 0)
                return null;

            // Store the maximum element (root)
            var max = _heap[0];
            // Move the last element to the root
            _heap[0] = _heap[_heap.Count - 1];
            // Remove the last element
            _heap.RemoveAt(_heap.Count - 1);
            // Restore the heap property by moving the new root down
            HeapifyDown(0);

            // Return the maximum element
            return max;
        }
        //---------------------------------------------------------------

        // Method to get the maximum element (root) without removing it
        public ServiceRequest PeekMax()
        {
            // Return the root element if the heap is not empty, otherwise return null
            return _heap.Count > 0 ? _heap[0] : null;
        }
        //---------------------------------------------------------------

        // Method to remove a specific element from the heap
        public void Remove(ServiceRequest request)
        {
            // Find the index of the element to be removed
            var index = _heap.FindIndex(r => r.RequestId == request.RequestId);
            // If the element is not found, return
            if (index == -1)
                return;

            // Move the last element to the position of the element to be removed
            _heap[index] = _heap[_heap.Count - 1];
            // Remove the last element
            _heap.RemoveAt(_heap.Count - 1);

            // Restore the heap property by moving the new element down and up
            HeapifyDown(index);
            HeapifyUp(index);
        }
        //---------------------------------------------------------------

        // Method to restore the heap property by moving an element up
        private void HeapifyUp(int index)
        {
            // While the element is not the root and its priority is greater than its parent's priority
            while (index > 0 && _heap[index].Priority > _heap[Parent(index)].Priority)
            {
                // Swap the element with its parent
                Swap(index, Parent(index));
                // Move to the parent's index
                index = Parent(index);
            }
        }
        //---------------------------------------------------------------

        // Method to restore the heap property by moving an element down
        private void HeapifyDown(int index)
        {
            // While the element has at least one child
            while (LeftChild(index) < _heap.Count)
            {
                // Assume the left child is the larger child
                var largerChildIndex = LeftChild(index);

                // If the right child exists and is larger than the left child, update the larger child index
                if (RightChild(index) < _heap.Count && _heap[RightChild(index)].Priority > _heap[largerChildIndex].Priority)
                    largerChildIndex = RightChild(index);

                // If the element's priority is greater than or equal to the larger child's priority, break the loop
                if (_heap[index].Priority >= _heap[largerChildIndex].Priority)
                    break;

                // Swap the element with the larger child
                Swap(index, largerChildIndex);
                // Move to the larger child's index
                index = largerChildIndex;
            }
        }
        //---------------------------------------------------------------

        // Method to swap two elements in the heap
        private void Swap(int index1, int index2)
        {
            var temp = _heap[index1];
            _heap[index1] = _heap[index2];
            _heap[index2] = temp;
        }
        //---------------------------------------------------------------

        // Method to get the index of the parent of an element
        private static int Parent(int index) => (index - 1) / 2;

        // Method to get the index of the left child of an element
        private static int LeftChild(int index) => 2 * index + 1;

        // Method to get the index of the right child of an element
        private static int RightChild(int index) => 2 * index + 2;
    }
    //------------------------------------------------------------------



}
//-----------------------------------------------End of file--------------------------------------
