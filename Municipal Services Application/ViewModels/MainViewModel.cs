using GalaSoft.MvvmLight.CommandWpf;
using Municipal_Services_Application.Views;
using System;
using System.Windows;
using System.Windows.Input;

namespace Municipal_Services_Application.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ReportIssueViewModel ReportIssueVM { get; private set; }

        public ICommand NavigateCommand { get; private set; }

        public MainViewModel()
        {
            ReportIssueVM = new ReportIssueViewModel();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            // Assuming PerformNavigation method correctly accepts an 'object' parameter
            NavigateCommand = new RelayCommand<object>(PerformNavigation);
        }

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView)); // Notify the UI of the change
            }
        }


        private void PerformNavigation(object parameter)
        {
            if (parameter != null)
            {
                switch (parameter.ToString())
                {
                    case "ReportIssues":
                        var reportIssuesView = new ReportIssuesView();
                        reportIssuesView.DataContext = ReportIssueVM;
                        CurrentView = reportIssuesView;
                        break;

                    case "LocalEvents":
                        var localEventsView = new LocalEvent_Annoucments();
                        CurrentView = localEventsView;
                        break;

                    case "ServiceRequestStatus":
                        var serviceRequestStatusView = new ServiceRequestStatus();
                        CurrentView = serviceRequestStatusView;
                        break;

                    case "ViewReports": // Switch to the View Reports view
                        var issueListView = new IssueListView();
                        issueListView.DataContext = new IssueListViewModel(ReportIssueVM.GetAllIssues());
                        CurrentView = issueListView;
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
