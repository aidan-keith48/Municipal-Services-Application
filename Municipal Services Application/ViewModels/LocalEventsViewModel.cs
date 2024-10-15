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
    public ObservableCollection<LocalEventsModel> RecommendedEvents { get; set; }

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
        RecommendedEvents = new ObservableCollection<LocalEventsModel>();

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
        // Start with all events
        var allEvents = _eventsByCategory.Values.SelectMany(e => e).ToHashSet();

        // Filter by category if a specific category is selected (ignoring "All Categories")
        if (!string.IsNullOrEmpty(category) && category != "All Categories")
        {
            if (_eventsByCategory.ContainsKey(category))
            {
                // Filter by selected category
                allEvents = _eventsByCategory[category].ToHashSet();
            }
            else
            {
                // No events for the selected category
                return Enumerable.Empty<LocalEventsModel>();
            }
        }

        // Filter by date if a date is selected
        if (date.HasValue)
        {
            if (_eventsByDate.ContainsKey(date.Value))
            {
                // Intersect the results by date
                allEvents = allEvents.Intersect(_eventsByDate[date.Value]).ToHashSet();
            }
            else
            {
                // No events for the selected date
                return Enumerable.Empty<LocalEventsModel>();
            }
        }

        // Increment the SearchCounter for the filtered events
        foreach (var ev in allEvents)
        {
            ev.SearchCounter++;
        }

        return allEvents;
    }

    // Search for announcements based on date and category
    public IEnumerable<AnnouncementModel> SearchAnnouncements(DateTime? date, string category)
    {
        var allAnnouncements = _announcementsByCategory.Values.SelectMany(a => a).ToHashSet();

        // Filter by category if a specific category is selected (ignoring "All Categories")
        if (!string.IsNullOrEmpty(category) && category != "All Categories")
        {
            if (_announcementsByCategory.ContainsKey(category))
            {
                allAnnouncements = _announcementsByCategory[category].ToHashSet();
            }
            else
            {
                return Enumerable.Empty<AnnouncementModel>();
            }
        }

        // Filter by date if a date is selected
        if (date.HasValue)
        {
            if (_announcementsByDate.ContainsKey(date.Value))
            {
                allAnnouncements = allAnnouncements.Intersect(_announcementsByDate[date.Value]).ToHashSet();
            }
            else
            {
                return Enumerable.Empty<AnnouncementModel>();
            }
        }

        return allAnnouncements;
    }

    // Execute the search and update the UI
    private void Search()
    {
        var filteredEvents = SearchEvents(SelectedDate, SelectedCategory);
        var filteredAnnouncements = SearchAnnouncements(SelectedDate, SelectedCategory);

        // Update Events
        Events.Clear();
        foreach (var ev in filteredEvents)
        {
            Events.Add(ev);
        }

        // Update Announcements
        Announcements.Clear();
        foreach (var ann in filteredAnnouncements)
        {
            Announcements.Add(ann);
        }

        // Update the recommended events list
        UpdateRecommendedEventsList();
    }

    // Update the ObservableCollection for Recommended Events
    private void UpdateRecommendedEventsList()
    {
        var recommended = _eventQueue.Values.SelectMany(q => q)
            .OrderByDescending(e => e.SearchCounter)
            .Take(10);  // Get top 10 most searched events

        RecommendedEvents.Clear();
        foreach (var ev in recommended)
        {
            RecommendedEvents.Add(ev);
        }
    }

    // Dummy data for testing
    private void LoadDummyData()
    {
        AddEvent(new LocalEventsModel(1, "Music Festival", DateTime.Now.AddDays(5), "Music", false, 1));
        AddEvent(new LocalEventsModel(2, "Sports Day", DateTime.Now.AddDays(3), "Sports", true, 2));

        AddAnnouncement(new AnnouncementModel(1, "Water Cut Announcement", "There will be a water cut in your area.", "Water Announcement", DateTime.Now, true, 1));
        AddAnnouncement(new AnnouncementModel(2, "City Clean-up", "Join us for a city clean-up.", "Clean Up", DateTime.Now.AddDays(1), false, 2));
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
