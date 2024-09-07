using GalaSoft.MvvmLight.Command;
using Municipal_Services_Application.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Municipal_Services_Application.ViewModels
{
    public class ReportIssueViewModel : BaseViewModel
    {
        private IssueTicket _currentIssue = new IssueTicket();

        public string Location
        {
            get => _currentIssue.Location;
            set { _currentIssue.Location = value; OnPropertyChanged(nameof(Location)); }
        }

        public string Category
        {
            get => _currentIssue.Category;
            set { _currentIssue.Category = value; OnPropertyChanged(nameof(Category)); }
        }

        public string Description
        {
            get => _currentIssue.Description;
            set { _currentIssue.Description = value; OnPropertyChanged(nameof(Description)); }
        }

        // Commands
        public ICommand SubmitCommand { get; }

        public ReportIssueViewModel()
        {
            SubmitCommand = new RelayCommand(SubmitIssue);
        }

        private void SubmitIssue()
        {
            // Logic to handle issue submission
        }
    }

}
