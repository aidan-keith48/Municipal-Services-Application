using GalaSoft.MvvmLight.Command;
using Municipal_Services_Application.Model;
using Municipal_Services_Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

public class LocalEventsViewModel : BaseViewModel
{
    // Data structures to store events and announcements
    private SortedDictionary<int, Queue<LocalEventsModel>> _eventQueue;
    private Dictionary<string, AnnouncementModel> _announcementsDictionary;

    // Sets for efficient searching
    private Dictionary<DateTime, HashSet<LocalEventsModel>> _eventsByDate;
    private Dictionary<string, HashSet<LocalEventsModel>> _eventsByCategory;

    private Dictionary<DateTime, HashSet<AnnouncementModel>> _announcementsByDate;
    private Dictionary<string, HashSet<AnnouncementModel>> _announcementsByCategory;

    // Search input properties
    public DateTime? SelectedDate { get; set; }
    public string SelectedCategory { get; set; }

    // Observable collections to bind to the UI
    public ObservableCollection<LocalEventsModel> Events { get; set; }
    public ObservableCollection<AnnouncementModel> Announcements { get; set; }

    // Command for search functionality
    public ICommand SearchCommand { get; private set; }

    public LocalEventsViewModel()
    {
        // Initialize data structures
        _eventQueue = new SortedDictionary<int, Queue<LocalEventsModel>>();
        _announcementsDictionary = new Dictionary<string, AnnouncementModel>();

        _eventsByDate = new Dictionary<DateTime, HashSet<LocalEventsModel>>();
        _eventsByCategory = new Dictionary<string, HashSet<LocalEventsModel>>();

        _announcementsByDate = new Dictionary<DateTime, HashSet<AnnouncementModel>>();
        _announcementsByCategory = new Dictionary<string, HashSet<AnnouncementModel>>();

        Events = new ObservableCollection<LocalEventsModel>();
        Announcements = new ObservableCollection<AnnouncementModel>();

        SearchCommand = new RelayCommand(Search);

        LoadDummyData(); // For testing
    }

    // Add events to the priority queue and sets
    public void AddEvent(LocalEventsModel newEvent)
    {
        if (!_eventQueue.ContainsKey(newEvent.Priority))
        {
            _eventQueue[newEvent.Priority] = new Queue<LocalEventsModel>();
        }
        _eventQueue[newEvent.Priority].Enqueue(newEvent);

        // Add event to sets by date and category
        if (!_eventsByDate.ContainsKey(newEvent.EventDate.Date))
        {
            _eventsByDate[newEvent.EventDate.Date] = new HashSet<LocalEventsModel>();
        }
        _eventsByDate[newEvent.EventDate.Date].Add(newEvent);

        if (!_eventsByCategory.ContainsKey(newEvent.Category))
        {
            _eventsByCategory[newEvent.Category] = new HashSet<LocalEventsModel>();
        }
        _eventsByCategory[newEvent.Category].Add(newEvent);

        UpdateEventsList();
    }

    // Add announcements to the dictionary and sets
    public void AddAnnouncement(AnnouncementModel newAnnouncement)
    {
        if (newAnnouncement.AnnouncementCategory == null)
        {
            throw new ArgumentNullException(nameof(newAnnouncement.AnnouncementCategory), "Announcement category cannot be null.");
        }

        _announcementsDictionary[newAnnouncement.AnnouncementTitle] = newAnnouncement;

        if (!_announcementsByDate.ContainsKey(newAnnouncement.AnnouncementDate.Date))
        {
            _announcementsByDate[newAnnouncement.AnnouncementDate.Date] = new HashSet<AnnouncementModel>();
        }
        _announcementsByDate[newAnnouncement.AnnouncementDate.Date].Add(newAnnouncement);

        if (!_announcementsByCategory.ContainsKey(newAnnouncement.AnnouncementCategory))
        {
            _announcementsByCategory[newAnnouncement.AnnouncementCategory] = new HashSet<AnnouncementModel>();
        }
        _announcementsByCategory[newAnnouncement.AnnouncementCategory].Add(newAnnouncement);

        UpdateAnnouncementsList();
    }

    // Search for events based on date and category
    public IEnumerable<LocalEventsModel> SearchEvents(DateTime? date, string category)
    {
        HashSet<LocalEventsModel> resultSet = null;

        // Search by date
        if (date.HasValue && _eventsByDate.ContainsKey(date.Value))
        {
            resultSet = _eventsByDate[date.Value];
        }

        // Search by category
        if (!string.IsNullOrEmpty(category) && _eventsByCategory.ContainsKey(category))
        {
            if (resultSet != null)
            {
                resultSet = resultSet.Intersect(_eventsByCategory[category]).ToHashSet();
            }
            else
            {
                resultSet = _eventsByCategory[category];
            }
        }

        return resultSet ?? Enumerable.Empty<LocalEventsModel>();
    }

    // Search for announcements based on date and category
    public IEnumerable<AnnouncementModel> SearchAnnouncements(DateTime? date, string category)
    {
        HashSet<AnnouncementModel> resultSet = null;

        // Search by date
        if (date.HasValue && _announcementsByDate.ContainsKey(date.Value))
        {
            resultSet = _announcementsByDate[date.Value];
        }

        // Search by category
        if (!string.IsNullOrEmpty(category) && _announcementsByCategory.ContainsKey(category))
        {
            if (resultSet != null)
            {
                resultSet = resultSet.Intersect(_announcementsByCategory[category]).ToHashSet();
            }
            else
            {
                resultSet = _announcementsByCategory[category];
            }
        }

        return resultSet ?? Enumerable.Empty<AnnouncementModel>();
    }

    // Execute the search and update the UI
    private void Search()
    {
        var filteredEvents = SearchEvents(SelectedDate, SelectedCategory);
        var filteredAnnouncements = SearchAnnouncements(SelectedDate, SelectedCategory);

        Events.Clear();
        foreach (var ev in filteredEvents)
        {
            Events.Add(ev);
        }

        Announcements.Clear();
        foreach (var ann in filteredAnnouncements)
        {
            Announcements.Add(ann);
        }
    }

    // Dummy data for testing
    private void LoadDummyData()
    {
        AddEvent(new LocalEventsModel(1, "Music Festival", DateTime.Now.AddDays(5), "Music", false, 1));
        AddEvent(new LocalEventsModel(2, "Sports Day", DateTime.Now.AddDays(3), "Sports", true, 2));

        AddAnnouncement(new AnnouncementModel(1, "Water Cut Announcement", "There will be a water cut in your area.", "Music", DateTime.Now, true, 1));
        AddAnnouncement(new AnnouncementModel(2, "City Clean-up", "Join us for a city clean-up.", "Sport", DateTime.Now.AddDays(1), false, 2));
    }

    // Update the ObservableCollection for Events
    private void UpdateEventsList()
    {
        Events.Clear();
        foreach (var priorityQueue in _eventQueue.Values)
        {
            foreach (var eventItem in priorityQueue)
            {
                Events.Add(eventItem);
            }
        }
    }

    // Update the ObservableCollection for Announcements
    private void UpdateAnnouncementsList()
    {
        Announcements.Clear();
        foreach (var announcement in _announcementsDictionary.Values)
        {
            Announcements.Add(announcement);
        }
    }
}
