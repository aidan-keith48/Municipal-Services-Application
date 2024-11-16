using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Municipal_Services_Application.Model;
using Municipal_Services_Application.Repositories;
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

        public ServiceRequestStatusViewModel(IServiceRequestRepository repository)
        {
            _repository = repository;

            // Initialize collections
            ServiceRequests = new ObservableCollection<ServiceRequest>(_repository.GetAllRequests());
            PrioritizedRequests = new ObservableCollection<ServiceRequest>();
            DependencyRequests = new ObservableCollection<ServiceRequest>();

            // Initialize commands
            SearchCommand = new RelayCommand(PerformSearch);
            ViewDetailsCommand = new RelayCommand<ServiceRequest>(ViewDetails);
            LoadDependenciesCommand = new RelayCommand<int>(LoadDependencies);

            // Load initial data
            PerformSearch();
            LoadPrioritizedRequests();
        }

        // Perform search filtering for Service Requests
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
        private void LoadDependencies(int requestId)
        {
            DependencyRequests.Clear();

            foreach (var dep in _repository.GetDependencies(requestId))
            {
                var request = _repository.GetRequestById(dep);
                if (request != null)
                {
                    DependencyRequests.Add(request);
                }
            }
        }

        // Show details of a selected request
        private void ViewDetails(ServiceRequest request)
        {
            if (request == null) return;

            // Display details in a popup or overlay
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
