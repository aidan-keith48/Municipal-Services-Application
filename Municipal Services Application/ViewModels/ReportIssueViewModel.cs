using System.Windows.Forms; // Required for OpenFileDialog
using System.Collections.Generic;
using Municipal_Services_Application.Model;
using Municipal_Services_Application.ViewModels;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Windows;

public class ReportIssueViewModel : BaseViewModel
{
    private Dictionary<int, IssueTicket> _issuesDictionary = new Dictionary<int, IssueTicket>();
    private int _nextIssueId = 1;

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

    public ICommand SubmitCommand { get; private set; }
    public ICommand AttachImageCommand { get; private set; }

    public ReportIssueViewModel()
    {
        SubmitCommand = new RelayCommand(SubmitIssue);
        AttachImageCommand = new RelayCommand(AttachImage);
    }

    private void SubmitIssue()
    {
        var storedIssue = new IssueTicket
        {
            Location = this.Location,
            Category = this.Category,
            Description = this.Description,
            ImageAttachments = new List<string>(_currentIssue.ImageAttachments)
        };

        // Store the issue in the dictionary
        _issuesDictionary[_nextIssueId++] = storedIssue;

        // Create a message to show what has been submitted
        string submittedData = $"Issue Submitted!\n" +
                               $"Location: {storedIssue.Location}\n" +
                               $"Category: {storedIssue.Category}\n" +
                               $"Description: {storedIssue.Description}\n" +
                               $"Attachments: {string.Join(", ", storedIssue.ImageAttachments)}";

        // Show a message box to confirm submission and show the data
        System.Windows.MessageBox.Show(submittedData, "Submission Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);

        // Clear the current issue after submission
        ClearCurrentIssue();
    }


    private void AttachImage()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Multiselect = true,
            Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            foreach (var filePath in openFileDialog.FileNames)
            {
                _currentIssue.ImageAttachments.Add(filePath);
            }
        }
    }

    private void ClearCurrentIssue()
    {
        Location = string.Empty;
        Category = string.Empty;
        Description = string.Empty;
        _currentIssue.ImageAttachments.Clear();
    }

    public Dictionary<int, IssueTicket> GetAllIssues()
    {
        return _issuesDictionary;
    }

}
