using GalaSoft.MvvmLight.CommandWpf;
using Municipal_Services_Application.Views;
using System;
using System.Windows;
using System.Windows.Input;

namespace Municipal_Services_Application.ViewModels
{
    /// <summary>
    /// The main view model for the application.
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets the report issue view model.
        /// </summary>
        public ReportIssueViewModel ReportIssueVM { get; private set; }

        /// <summary>
        /// Gets the navigate command.
        /// </summary>
        public ICommand NavigateCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
            ReportIssueVM = new ReportIssueViewModel();
            InitializeCommands();
        }
        //-------------------------------------------------------------------------------------------

        private void InitializeCommands()
        {
            // Assuming PerformNavigation method correctly accepts an 'object' parameter
            NavigateCommand = new RelayCommand<object>(PerformNavigation);
        }
        //-------------------------------------------------------------------------------------------

        private object _currentView;

        /// <summary>
        /// Gets or sets the current view.
        /// </summary>
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView)); // Notify the UI of the change
            }
        }
        //-------------------------------------------------------------------------------------------

        /// <summary>
        /// This method performs navigation based on the parameter.
        /// </summary>
        /// <param name="parameter"></param>
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
        //-------------------------------------------------------------------------------------------
    }
}
//---------------------------------------------------------------------------End of File--------------------------------------------------------------------------------
