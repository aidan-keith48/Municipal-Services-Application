using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using Microsoft.Win32;
using Municipal_Services_Application.Model;
using Municipal_Services_Application.ViewModels;
using System.IO;

/// <summary>
/// ViewModel for reporting issues in the municipal services application.
/// </summary>
public class ReportIssueViewModel : BaseViewModel
{
    /// <summary>
    /// Dictionary to store issues with their ID as the key.
    /// </summary>
    private Dictionary<int, IssueTicket> _issuesDictionary = new Dictionary<int, IssueTicket>();

    /// <summary>
    /// The next available issue ID.
    /// </summary>
    private int _nextIssueId = 1;

    /// <summary>
    /// Collection of available categories for issues.
    /// </summary>
    public ObservableCollection<string> Categories { get; set; }

    /// <summary>
    /// The current issue being reported.
    /// </summary>
    private IssueTicket _currentIssue = new IssueTicket();

    /// <summary>
    /// Command to submit the current issue.
    /// </summary>
    public ICommand SubmitCommand { get; private set; }

    /// <summary>
    /// Command to attach files to the current issue.
    /// </summary>
    public ICommand AttachFileCommand { get; private set; }

    /// <summary>
    /// Gets or sets the location of the current issue.
    /// </summary>
    public string Location
    {
        get => _currentIssue.Location;
        set
        {
            _currentIssue.Location = value;
            OnPropertyChanged(nameof(Location));
        }
    }

    /// <summary>
    /// Gets or sets the category of the current issue.
    /// </summary>
    public string Category
    {
        get => _currentIssue.Category;
        set
        {
            _currentIssue.Category = value;
            OnPropertyChanged(nameof(Category));
        }
    }

    /// <summary>
    /// Gets or sets the description of the current issue.
    /// </summary>
    public string Description
    {
        get => _currentIssue.Description;
        set
        {
            _currentIssue.Description = value;
            OnPropertyChanged(nameof(Description));
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportIssueViewModel"/> class.
    /// </summary>
    public ReportIssueViewModel()
    {
        // Initialize categories
        Categories = new ObservableCollection<string>
                {
                    "Sanitation",
                    "Roads",
                    "Utilities",
                    "Water Supply",
                    "Electricity",
                    "Public Transport",
                    "Clean Up",
                    "Health",
                    "Education",
                    "Recreation",
                    "Environment"
                };

        // Initialize commands
        SubmitCommand = new RelayCommand(SubmitIssue);
        AttachFileCommand = new RelayCommand(AttachFile);
    }

    /// <summary>
    /// Submits the current issue after validating the input.
    /// </summary>
    private void SubmitIssue()
    {
        // Input validation
        if (string.IsNullOrWhiteSpace(Location) || Location.Length < 5 || Location.Length > 100)
        {
            MessageBox.Show("Please enter a valid location (5-100 characters).", "Invalid Location", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (string.IsNullOrWhiteSpace(Category) || !Categories.Contains(Category))
        {
            MessageBox.Show("Please select a valid category.", "Invalid Category", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (string.IsNullOrWhiteSpace(Description) || Description.Length < 10)
        {
            MessageBox.Show("Please provide a detailed description (at least 10 characters).", "Invalid Description", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (_currentIssue.Attachments.Count == 0)
        {
            MessageBox.Show("Please attach at least one file.", "No Attachments", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // Store the issue
        var storedIssue = new IssueTicket
        {
            ID = _nextIssueId,
            Location = this.Location,
            Category = this.Category,
            Description = this.Description,
            Attachments = new List<string>(_currentIssue.Attachments)
        };

        _issuesDictionary[_nextIssueId] = storedIssue;
        _nextIssueId++;

        // Display confirmation message
        string submittedData = $"Issue Submitted!\n" +
                               $"ID: {storedIssue.ID}\n" +
                               $"Location: {storedIssue.Location}\n" +
                               $"Category: {storedIssue.Category}\n" +
                               $"Description: {storedIssue.Description}\n" +
                               $"Attachments: {string.Join(", ", storedIssue.Attachments)}";

        MessageBox.Show(submittedData, "Submission Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);

        // Clear the current issue
        ClearCurrentIssue();
    }

    /// <summary>
    /// Opens a file dialog to attach files to the current issue.
    /// </summary>
    private void AttachFile()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Multiselect = true,
            Filter = "All Supported Files|*.jpg;*.jpeg;*.png;*.bmp;*.pdf;*.doc;*.docx;*.xls;*.xlsx;*.ppt;*.pptx|" +
                     "Image Files|*.jpg;*.jpeg;*.png;*.bmp|" +
                     "PDF Files|*.pdf|" +
                     "Word Documents|*.doc;*.docx|" +
                     "Excel Files|*.xls;*.xlsx|" +
                     "PowerPoint Files|*.ppt;*.pptx"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            foreach (var filePath in openFileDialog.FileNames)
            {
                _currentIssue.Attachments.Add(filePath);
            }
        }
    }

    /// <summary>
    /// Clears the current issue details.
    /// </summary>
    private void ClearCurrentIssue()
    {
        Location = string.Empty;
        Category = string.Empty;
        Description = string.Empty;
        _currentIssue.Attachments.Clear();
    }

    /// <summary>
    /// Gets all reported issues.
    /// </summary>
    /// <returns>A dictionary of all reported issues.</returns>
    public Dictionary<int, IssueTicket> GetAllIssues()
    {
        return _issuesDictionary;
    }
}
