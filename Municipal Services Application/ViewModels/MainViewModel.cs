using GalaSoft.MvvmLight.CommandWpf;
using Municipal_Services_Application.Views;
using System;
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
                        CurrentView = new ReportIssuesView(); // Assuming ReportIssuesView is a UserControl
                        break;
                    case "LocalEvents":
                        CurrentView = new LocalEvent_Annoucments(); // Assuming LocalEventsView is a UserControl
                        break;
                    case "ServiceRequestStatus":
                        CurrentView = new ServiceRequestStatus(); // Assuming ServiceRequestStatusView is a UserControl
                        break;
                    default:
                        // Handle unknown view request or show default view
                        break;
                }
            }
        }

    }
}
