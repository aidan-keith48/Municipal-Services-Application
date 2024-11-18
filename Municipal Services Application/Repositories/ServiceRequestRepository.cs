using System;
using System.Collections.Generic;
using System.Linq;
using Municipal_Services_Application.Data_Structures;
using Municipal_Services_Application.Model;

namespace Municipal_Services_Application.Repositories
{
    public class ServiceRequestRepository : IServiceRequestRepository
    {
        private readonly List<ServiceRequest> _serviceRequests;
        private BinarySearchTree _bst;
        private MaxHeap _maxHeap;
        private Graph _graph;

        public ServiceRequestRepository()
        {
            _serviceRequests = new List<ServiceRequest>();
            SeedData();
            InitializeBST();
            InitializeHeap();
            InitializeGraph();
        }

        public IEnumerable<ServiceRequest> GetAllRequests()
        {
            return _serviceRequests;
        }

        public ServiceRequest GetRequestById(int requestId)
        {
            return _serviceRequests.FirstOrDefault(r => r.RequestId == requestId);
        }

        public void AddServiceRequest(ServiceRequest request)
        {
            _serviceRequests.Add(request);
            _bst.Insert(request); // Insert into BST
            _maxHeap.Insert(request); // Insert into Heap
            if (request.Dependencies != null && request.Dependencies.Any())
            {
                foreach (var dependency in request.Dependencies)
                {
                    _graph.AddEdge(request.RequestId, dependency); // Add dependencies to graph
                }
            }
        }

        public void UpdateServiceRequest(ServiceRequest request)
        {
            var existingRequest = GetRequestById(request.RequestId);
            if (existingRequest != null)
            {
                existingRequest.Status = request.Status;
                existingRequest.Title = request.Title;
                existingRequest.DateSubmitted = request.DateSubmitted;
                existingRequest.Progress = request.Progress;
                existingRequest.Priority = request.Priority;
            }
        }

        public void DeleteServiceRequest(int requestId)
        {
            var request = GetRequestById(requestId);
            if (request != null)
            {
                _serviceRequests.Remove(request);
                _bst.Remove(request.RequestId); // Remove from BST
                _maxHeap.Remove(request); // Remove from Heap
                _graph.RemoveNode(request.RequestId); // Remove from Graph
            }
        }

        public IEnumerable<ServiceRequest> SearchRequests(string searchText)
        {
            return _serviceRequests.Where(r =>
                r.Title.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                r.RequestId.ToString().Contains(searchText));
        }

        private void SeedData()
        {
            // Sample data to simulate initial service requests
            _serviceRequests.Add(new ServiceRequest(1, "Leaking Pipe", "Pending", DateTime.Now.AddDays(-3), 20, 1, new List<int> { 2 }));
            _serviceRequests.Add(new ServiceRequest(2, "Street Light Repair", "In Progress", DateTime.Now.AddDays(-5), 50, 2, new List<int> { 3 }));
            _serviceRequests.Add(new ServiceRequest(3, "Road Maintenance", "Completed", DateTime.Now.AddDays(-10), 100, 3, null));
            _serviceRequests.Add(new ServiceRequest(4, "Water Supply Issue", "Pending", DateTime.Now.AddDays(-2), 10, 4, null));
            _serviceRequests.Add(new ServiceRequest(5, "Garbage Collection", "In Progress", DateTime.Now.AddDays(-1), 30, 5, null));
            _serviceRequests.Add(new ServiceRequest(6, "Tree Trimming", "Completed", DateTime.Now.AddDays(-7), 100, 6, null));
            _serviceRequests.Add(new ServiceRequest(7, "Pothole Repair", "Pending", DateTime.Now.AddDays(-4), 15, 7, null));
            _serviceRequests.Add(new ServiceRequest(8, "Sewer Backup", "In Progress", DateTime.Now.AddDays(-6), 40, 8, null));
            _serviceRequests.Add(new ServiceRequest(9, "Graffiti Removal", "Completed", DateTime.Now.AddDays(-8), 100, 9, null));
            _serviceRequests.Add(new ServiceRequest(10, "Street Cleaning", "Pending", DateTime.Now.AddDays(-9), 25, 10, null));
            _serviceRequests.Add(new ServiceRequest(11, "Park Maintenance", "In Progress", DateTime.Now.AddDays(-11), 60, 11, null));
            _serviceRequests.Add(new ServiceRequest(12, "Snow Removal", "Completed", DateTime.Now.AddDays(-12), 100, 12, null));
            _serviceRequests.Add(new ServiceRequest(13, "Sidewalk Repair", "Pending", DateTime.Now.AddDays(-13), 35, 13, null));
            _serviceRequests.Add(new ServiceRequest(14, "Traffic Signal Issue", "In Progress", DateTime.Now.AddDays(-14), 70, 14, null));
            _serviceRequests.Add(new ServiceRequest(15, "Public Restroom Cleaning", "Completed", DateTime.Now.AddDays(-15), 100, 15, null));
            _serviceRequests.Add(new ServiceRequest(16, "Playground Equipment Repair", "Pending", DateTime.Now.AddDays(-16), 45, 16, null));
            _serviceRequests.Add(new ServiceRequest(17, "Fire Hydrant Repair", "In Progress", DateTime.Now.AddDays(-17), 80, 17, null));
            _serviceRequests.Add(new ServiceRequest(18, "Street Sign Replacement", "Completed", DateTime.Now.AddDays(-18), 100, 18, null));
            _serviceRequests.Add(new ServiceRequest(19, "Bus Stop Shelter Repair", "Pending", DateTime.Now.AddDays(-19), 55, 19, null));
            _serviceRequests.Add(new ServiceRequest(20, "Drainage Issue", "In Progress", DateTime.Now.AddDays(-20), 90, 20, null));
            _serviceRequests.Add(new ServiceRequest(21, "Bridge Maintenance", "Completed", DateTime.Now.AddDays(-21), 100, 21, null));
            _serviceRequests.Add(new ServiceRequest(22, "Flood Control", "Pending", DateTime.Now.AddDays(-22), 65, 22, null));
            _serviceRequests.Add(new ServiceRequest(23, "Public Art Installation", "In Progress", DateTime.Now.AddDays(-23), 95, 23, null));
            _serviceRequests.Add(new ServiceRequest(24, "Bike Lane Repair", "Completed", DateTime.Now.AddDays(-24), 100, 24, null));
            _serviceRequests.Add(new ServiceRequest(25, "Community Garden Maintenance", "Pending", DateTime.Now.AddDays(-25), 75, 25, null));
            _serviceRequests.Add(new ServiceRequest(26, "Street Furniture Repair", "In Progress", DateTime.Now.AddDays(-26), 100, 26, null));
            _serviceRequests.Add(new ServiceRequest(27, "Public Wi-Fi Issue", "Completed", DateTime.Now.AddDays(-27), 100, 27, null));
            _serviceRequests.Add(new ServiceRequest(28, "Storm Drain Cleaning", "Pending", DateTime.Now.AddDays(-28), 85, 28, null));
            _serviceRequests.Add(new ServiceRequest(29, "Public Library Maintenance", "In Progress", DateTime.Now.AddDays(-29), 100, 29, null));
            _serviceRequests.Add(new ServiceRequest(30, "Community Center Repair", "Completed", DateTime.Now.AddDays(-30), 100, 30, null));
            _serviceRequests.Add(new ServiceRequest(31, "Fire Station Maintenance", "Pending", DateTime.Now.AddDays(-31), 95, 31, null));
            _serviceRequests.Add(new ServiceRequest(32, "Police Station Repair", "In Progress", DateTime.Now.AddDays(-32), 100, 32, null));
            _serviceRequests.Add(new ServiceRequest(33, "Public Pool Maintenance", "Completed", DateTime.Now.AddDays(-33), 100, 33, null));
            _serviceRequests.Add(new ServiceRequest(34, "Recycling Bin Replacement", "Pending", DateTime.Now.AddDays(-34), 100, 34, null));
            _serviceRequests.Add(new ServiceRequest(35, "Public Park Lighting", "In Progress", DateTime.Now.AddDays(-35), 100, 35, null));
            _serviceRequests.Add(new ServiceRequest(36, "Community Event Setup", "Completed", DateTime.Now.AddDays(-36), 100, 36, null));
            _serviceRequests.Add(new ServiceRequest(37, "Public Fountain Repair", "Pending", DateTime.Now.AddDays(-37), 100, 37, null));
            _serviceRequests.Add(new ServiceRequest(38, "Public Bench Repair", "In Progress", DateTime.Now.AddDays(-38), 100, 38, null));
            _serviceRequests.Add(new ServiceRequest(39, "Public Restroom Repair", "Completed", DateTime.Now.AddDays(-39), 100, 39, null));
            _serviceRequests.Add(new ServiceRequest(40, "Public Park Cleanup", "Pending", DateTime.Now.AddDays(-40), 100, 40, null));
            _serviceRequests.Add(new ServiceRequest(41, "Public Garden Maintenance", "In Progress", DateTime.Now.AddDays(-41), 100, 41, null));
            _serviceRequests.Add(new ServiceRequest(42, "Public Playground Repair", "Completed", DateTime.Now.AddDays(-42), 100, 42, null));
            _serviceRequests.Add(new ServiceRequest(43, "Public Park Repair", "Pending", DateTime.Now.AddDays(-43), 100, 43, null));
            _serviceRequests.Add(new ServiceRequest(44, "Public Park Equipment Repair", "In Progress", DateTime.Now.AddDays(-44), 100, 44, null));
            _serviceRequests.Add(new ServiceRequest(45, "Public Park Facility Repair", "Completed", DateTime.Now.AddDays(-45), 100, 45, null));
            _serviceRequests.Add(new ServiceRequest(46, "Public Park Infrastructure Repair", "Pending", DateTime.Now.AddDays(-46), 100, 46, null));
            _serviceRequests.Add(new ServiceRequest(47, "Public Park Landscaping", "In Progress", DateTime.Now.AddDays(-47), 100, 47, null));
            _serviceRequests.Add(new ServiceRequest(48, "Public Park Maintenance", "Completed", DateTime.Now.AddDays(-48), 100, 48, null));
            _serviceRequests.Add(new ServiceRequest(49, "Public Park Pathway Repair", "Pending", DateTime.Now.AddDays(-49), 100, 49, null));
            _serviceRequests.Add(new ServiceRequest(50, "Public Park Playground Repair", "In Progress", DateTime.Now.AddDays(-50), 100, 50, null));
        }

        // Initialize Binary Search Tree
        public void InitializeBST()
        {
            _bst = new BinarySearchTree();
            foreach (var request in _serviceRequests)
            {
                _bst.Insert(request);
            }
        }

        public ServiceRequest SearchInBST(int requestId)
        {
            return _bst.Search(requestId);
        }

        // Initialize Max Heap
        public void InitializeHeap()
        {
            _maxHeap = new MaxHeap();
            foreach (var request in _serviceRequests)
            {
                _maxHeap.Insert(request);
            }
        }

        public IEnumerable<ServiceRequest> GetPrioritizedRequests()
        {
            var results = new List<ServiceRequest>();
            while (_maxHeap.Count > 0)
            {
                results.Add(_maxHeap.ExtractMax());
            }
            return results;
        }

        // Initialize Graph
        public void InitializeGraph()
        {
            _graph = new Graph();
            foreach (var request in _serviceRequests)
            {
                if (request.Dependencies != null)
                {
                    foreach (var dependency in request.Dependencies)
                    {
                        _graph.AddEdge(request.RequestId, dependency);
                    }
                }
            }
        }

        public IEnumerable<int> GetDependencies(int requestId)
        {
            return _graph.GetDependencies(requestId);
        }

        public IEnumerable<int> TopologicalSort()
        {
            return _graph.TopologicalSort();
        }
    }

    public interface IServiceRequestRepository
    {
        IEnumerable<ServiceRequest> GetAllRequests();
        ServiceRequest GetRequestById(int requestId);
        void AddServiceRequest(ServiceRequest request);
        void UpdateServiceRequest(ServiceRequest request);
        void DeleteServiceRequest(int requestId);
        IEnumerable<ServiceRequest> SearchRequests(string searchText);

        // Add these methods
        IEnumerable<ServiceRequest> GetPrioritizedRequests(); // For prioritized requests
        IEnumerable<int> GetDependencies(int requestId); // For request dependencies (IDs of dependent requests)
        ServiceRequest SearchInBST(int requestId); // For binary search
        IEnumerable<int> TopologicalSort(); // Topological sorting for dependencies
    }
}
