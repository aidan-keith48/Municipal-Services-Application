using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Municipal_Services_Application.Model;
using Municipal_Services_Application.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Municipal_Services_Application.ViewModels
{
    public class ServiceRequestStatusViewModel : ViewModelBase
    {
        private readonly IServiceRequestRepository _repository;

        // ObservableCollections to bind to the DataGrids in the UI
        public ObservableCollection<ServiceRequest> ServiceRequests { get; private set; }
        public ObservableCollection<ServiceRequest> PrioritizedRequests { get; private set; }
        public ObservableCollection<ServiceRequest> DependencyRequests { get; private set; }

        // Selected Service Request
        private ServiceRequest _selectedServiceRequest;
        public ServiceRequest SelectedServiceRequest
        {
            get => _selectedServiceRequest;
            set => Set(ref _selectedServiceRequest, value);
        }

        private string _selectedRequestId;
        public string SelectedRequestId
        {
            get => _selectedRequestId;
            set => Set(ref _selectedRequestId, value);
        }

        // New Request Properties
        public string NewRequestTitle { get; set; }
        public string NewRequestStatus { get; set; }
        public string NewRequestPriority { get; set; }
        public string NewRequestDependencies { get; set; }
        public ObservableCollection<string> StatusOptions { get; private set; }

        // Search Text for Filtering
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (Set(ref _searchText, value))
                {
                    PerformSearch();
                }
            }
        }

        // Commands for UI Interactions
        public ICommand SearchCommand { get; private set; }
        public ICommand ViewDetailsCommand { get; private set; }
        public ICommand LoadDependenciesCommand { get; private set; }
        public ICommand SaveNewRequestCommand { get; private set; }
        public ICommand DeleteRequestCommand { get; private set; }

        public ServiceRequestStatusViewModel(IServiceRequestRepository repository)
        {
            _repository = repository;

            // Initialize collections
            ServiceRequests = new ObservableCollection<ServiceRequest>(_repository.GetAllRequests());
            PrioritizedRequests = new ObservableCollection<ServiceRequest>();
            DependencyRequests = new ObservableCollection<ServiceRequest>();

            // Initialize Status Options
            StatusOptions = new ObservableCollection<string> { "Pending", "In Progress", "Completed" };

            // Initialize commands
            SearchCommand = new RelayCommand(SearchUsingBST);
            ViewDetailsCommand = new RelayCommand<ServiceRequest>(ViewDetails);
            LoadDependenciesCommand = new RelayCommand(LoadDependencies);
            SaveNewRequestCommand = new RelayCommand(SaveNewRequest);
            DeleteRequestCommand = new RelayCommand(DeleteSelectedRequest);

            // Load initial data
            PerformSearch();
            LoadPrioritizedRequests();
        }

        // Perform search filtering for Service Requests (dynamic filtering)
        private void PerformSearch()
        {
            ServiceRequests.Clear();

            var filteredRequests = string.IsNullOrEmpty(SearchText)
                ? _repository.GetAllRequests()
                : _repository.SearchRequests(SearchText);

            foreach (var request in filteredRequests)
            {
                ServiceRequests.Add(request);
            }
        }

        // Use BST explicitly for precise search
        private void SearchUsingBST()
        {
            if (!int.TryParse(SearchText, out int requestId))
            {
                MessageBox.Show("Invalid Request ID. Please enter a valid number.");
                return;
            }

            var result = _repository.SearchInBST(requestId); // Explicitly using BST
            if (result != null)
            {
                ServiceRequests.Clear();
                ServiceRequests.Add(result);
            }
            else
            {
                MessageBox.Show($"No service request found with ID: {requestId}");
            }
        }

        // Load prioritized requests from Max Heap
        private void LoadPrioritizedRequests()
        {
            PrioritizedRequests.Clear();

            foreach (var request in _repository.GetPrioritizedRequests())
            {
                PrioritizedRequests.Add(request);
            }
        }

        // Load dependencies for a specific request
        private void LoadDependencies()
        {
            DependencyRequests.Clear();

            if (!int.TryParse(SelectedRequestId, out int requestId))
            {
                MessageBox.Show("Invalid Request ID. Please enter a valid number.");
                return;
            }

            var dependencies = _repository.GetDependencies(requestId);
            if (dependencies == null || !dependencies.Any())
            {
                MessageBox.Show($"No dependencies found for Request ID: {requestId}");
                return;
            }

            foreach (var dep in dependencies)
            {
                var request = _repository.GetRequestById(dep);
                if (request != null)
                {
                    DependencyRequests.Add(request);
                }
            }
        }

        // Save a new request
        private void SaveNewRequest()
        {
            if (string.IsNullOrEmpty(NewRequestTitle) || string.IsNullOrEmpty(NewRequestStatus))
            {
                MessageBox.Show("Title and Status are required.");
                return;
            }

            if (!int.TryParse(NewRequestPriority, out int priority))
            {
                MessageBox.Show("Invalid priority. Please enter a number.");
                return;
            }

            var dependencies = string.IsNullOrEmpty(NewRequestDependencies)
                ? new List<int>()
                : NewRequestDependencies
                    .Split(',')
                    .Select(dep =>
                    {
                        if (int.TryParse(dep.Trim(), out int depId))
                            return depId;
                        else
                            return -1;
                    })
                    .Where(depId => depId != -1)
                    .ToList();

            var newRequest = new ServiceRequest(
                requestId: _repository.GetAllRequests().Max(r => r.RequestId) + 1,
                title: NewRequestTitle,
                status: NewRequestStatus,
                dateSubmitted: DateTime.Now,
                progress: 0,
                priority: priority,
                dependencies: dependencies
            );

            _repository.AddServiceRequest(newRequest);
            PerformSearch(); // Reload requests
            MessageBox.Show("New request added successfully.");

            // Clear the form
            NewRequestTitle = string.Empty;
            NewRequestStatus = string.Empty;
            NewRequestPriority = string.Empty;
            NewRequestDependencies = string.Empty;
        }

        // Delete a selected request
        private void DeleteSelectedRequest()
        {
            if (SelectedServiceRequest == null)
            {
                MessageBox.Show("Please select a request to delete.");
                return;
            }

            _repository.DeleteServiceRequest(SelectedServiceRequest.RequestId);
            PerformSearch(); // Reload requests
            MessageBox.Show("Request deleted successfully.");
        }

        // Show details of a selected request
        private void ViewDetails(ServiceRequest request)
        {
            if (request == null) return;

            MessageBox.Show(
                $"Details for Request ID: {request.RequestId}\n" +
                $"Title: {request.Title}\n" +
                $"Status: {request.Status}\n" +
                $"Priority: {request.Priority}\n" +
                $"Progress: {request.Progress}%\n" +
                $"Dependencies: {string.Join(", ", request.Dependencies)}"
            );
        }
    }
}
