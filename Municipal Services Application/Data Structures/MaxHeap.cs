using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Municipal_Services_Application.Data_Structures
{
    public class MaxHeap
    {
        private readonly List<ServiceRequest> _heap = new List<ServiceRequest>();

        public int Count => _heap.Count;

        public void Insert(ServiceRequest request)
        {
            _heap.Add(request);
            HeapifyUp(_heap.Count - 1);
        }

        public ServiceRequest ExtractMax()
        {
            if (_heap.Count == 0)
                return null;

            var max = _heap[0];
            _heap[0] = _heap[_heap.Count - 1];
            _heap.RemoveAt(_heap.Count - 1);
            HeapifyDown(0);

            return max;
        }

        public ServiceRequest PeekMax()
        {
            return _heap.Count > 0 ? _heap[0] : null;
        }

        public void Remove(ServiceRequest request)
        {
            var index = _heap.FindIndex(r => r.RequestId == request.RequestId);
            if (index == -1)
                return;

            _heap[index] = _heap[_heap.Count - 1];
            _heap.RemoveAt(_heap.Count - 1);

            HeapifyDown(index);
            HeapifyUp(index);
        }

        private void HeapifyUp(int index)
        {
            while (index > 0 && _heap[index].Priority > _heap[Parent(index)].Priority)
            {
                Swap(index, Parent(index));
                index = Parent(index);
            }
        }

        private void HeapifyDown(int index)
        {
            while (LeftChild(index) < _heap.Count)
            {
                var largerChildIndex = LeftChild(index);

                if (RightChild(index) < _heap.Count && _heap[RightChild(index)].Priority > _heap[largerChildIndex].Priority)
                    largerChildIndex = RightChild(index);

                if (_heap[index].Priority >= _heap[largerChildIndex].Priority)
                    break;

                Swap(index, largerChildIndex);
                index = largerChildIndex;
            }
        }

        private void Swap(int index1, int index2)
        {
            var temp = _heap[index1];
            _heap[index1] = _heap[index2];
            _heap[index2] = temp;
        }

        private static int Parent(int index) => (index - 1) / 2;
        private static int LeftChild(int index) => 2 * index + 1;
        private static int RightChild(int index) => 2 * index + 2;
    }


}
