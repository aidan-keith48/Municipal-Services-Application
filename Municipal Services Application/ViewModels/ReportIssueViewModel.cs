using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using Microsoft.Win32;
using Municipal_Services_Application.Model;
using Municipal_Services_Application.ViewModels;

public class ReportIssueViewModel : BaseViewModel
{
    private Dictionary<int, IssueTicket> _issuesDictionary = new Dictionary<int, IssueTicket>();
    private int _nextIssueId = 1; // Dictionary key and Issue ID are both managed here

    /// <summary>
    /// Gets or sets the list of categories to bind to the ComboBox.
    /// </summary>
    public ObservableCollection<string> Categories { get; set; }

    private IssueTicket _currentIssue = new IssueTicket();

    /// <summary>
    /// Gets the command to submit the current issue.
    /// </summary>
    public ICommand SubmitCommand { get; private set; }

    /// <summary>
    /// Gets the command to attach an image to the current issue.
    /// </summary>
    public ICommand AttachImageCommand { get; private set; }

    /// <summary>
    /// Gets or sets the location of the current issue.
    /// </summary>
    public string Location
    {
        get => _currentIssue.Location;
        set { _currentIssue.Location = value; OnPropertyChanged(nameof(Location)); }
    }
    //-------------------------------------------------------------------------------------------

    /// <summary>
    /// Gets or sets the category of the current issue.
    /// </summary>
    public string Category
    {
        get => _currentIssue.Category;
        set { _currentIssue.Category = value; OnPropertyChanged(nameof(Category)); }
    }
    //-------------------------------------------------------------------------------------------

    /// <summary>
    /// Gets or sets the description of the current issue.
    /// </summary>
    public string Description
    {
        get => _currentIssue.Description;
        set { _currentIssue.Description = value; OnPropertyChanged(nameof(Description)); }
    }
    //-------------------------------------------------------------------------------------------
    

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportIssueViewModel"/> class.
    /// </summary>
    public ReportIssueViewModel()
    {
        Categories = new ObservableCollection<string>
        {
            "Sanitation",
            "Roads",
            "Utilities"
        };

        SubmitCommand = new RelayCommand(SubmitIssue);
        AttachImageCommand = new RelayCommand(AttachImage);
    }
    //-------------------------------------------------------------------------------------------

    /// <summary>
    /// Submits the current issue.
    /// </summary>
    private void SubmitIssue()
    {
        if (string.IsNullOrWhiteSpace(Location) || string.IsNullOrWhiteSpace(Category) || string.IsNullOrWhiteSpace(Description))
        {
            MessageBox.Show("Please fill in all the fields before submitting.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var storedIssue = new IssueTicket
        {
            ID = _nextIssueId, // Set the unique ID for this issue
            Location = this.Location,
            Category = this.Category,
            Description = this.Description,
            ImageAttachments = new List<string>(_currentIssue.ImageAttachments)
        };

        // Store the issue in the dictionary using the ID as the key
        _issuesDictionary[_nextIssueId] = storedIssue;
        _nextIssueId++; // Increment the ID for the next issue

        // Display the submitted data
        string submittedData = $"Issue Submitted!\n" +
                               $"ID: {storedIssue.ID}\n" + // Display the ID
                               $"Location: {storedIssue.Location}\n" +
                               $"Category: {storedIssue.Category}\n" +
                               $"Description: {storedIssue.Description}\n" +
                               $"Attachments: {string.Join(", ", storedIssue.ImageAttachments)}";

        MessageBox.Show(submittedData, "Submission Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);

        // Clear the current issue after submission
        ClearCurrentIssue();
    }
    //-------------------------------------------------------------------------------------------

    /// <summary>
    /// Attaches an image to the current issue.
    /// </summary>
    private void AttachImage()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Multiselect = true,
            Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            foreach (var filePath in openFileDialog.FileNames)
            {
                _currentIssue.ImageAttachments.Add(filePath);
            }
        }
    }
    //-------------------------------------------------------------------------------------------

    /// <summary>
    /// Clears the current issue.
    /// </summary>
    private void ClearCurrentIssue()
    {
        Location = string.Empty;
        Category = string.Empty;
        Description = string.Empty;
        _currentIssue.ImageAttachments.Clear();
    }
    //-------------------------------------------------------------------------------------------

    /// <summary>
    /// Gets all the issues stored in the dictionary.
    /// </summary>
    /// <returns>A dictionary containing all the issues.</returns>
    public Dictionary<int, IssueTicket> GetAllIssues()
    {
        return _issuesDictionary;
    }
    //-------------------------------------------------------------------------------------------
}

//-------------------------------------------------------------------------End of File.-------------------------------------------------------------------------
