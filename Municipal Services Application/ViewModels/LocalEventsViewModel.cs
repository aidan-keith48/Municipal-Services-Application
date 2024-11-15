using GalaSoft.MvvmLight.Command;
using Municipal_Services_Application.Model;
using Municipal_Services_Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

/// <summary>
/// ViewModel for managing local events and announcements.
/// </summary>
public class LocalEventsViewModel : BaseViewModel
{
    // Data structures to store events, announcements, and recommended events
    private SortedDictionary<int, Queue<LocalEventsModel>> _eventQueue;
    private Dictionary<string, AnnouncementModel> _announcementsDictionary;
    private Dictionary<int, RecommendedEventsModel> _recommendedEventsDictionary; // For recommendations

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
    public ObservableCollection<RecommendedEventsModel> RecommendedEvents { get; set; }  // For recommendations

    // Commands for interaction
    public ICommand SearchCommand { get; private set; }
    public ICommand CloseEventDetailsCommand { get; private set; } // Added command to close the pop-up

    private LocalEventsModel _selectedEvent;
    public LocalEventsModel SelectedEvent
    {
        get => _selectedEvent;
        set
        {
            _selectedEvent = value;
            OnPropertyChanged(nameof(SelectedEvent));
        }
    }

    private RecommendedEventsModel _selectedRecommendedEvent;
    public RecommendedEventsModel SelectedRecommendedEvent
    {
        get => _selectedRecommendedEvent;
        set
        {
            _selectedRecommendedEvent = value;
            OnPropertyChanged(nameof(SelectedRecommendedEvent));
        }
    }

    public ICommand CloseRecommendedEventDetailsCommand { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LocalEventsViewModel"/> class.
    /// </summary>
    public LocalEventsViewModel()
    {
        // Initialize data structures
        _eventQueue = new SortedDictionary<int, Queue<LocalEventsModel>>();
        _announcementsDictionary = new Dictionary<string, AnnouncementModel>();
        _recommendedEventsDictionary = new Dictionary<int, RecommendedEventsModel>();  // For recommendations

        _eventsByDate = new Dictionary<DateTime, HashSet<LocalEventsModel>>();
        _eventsByCategory = new Dictionary<string, HashSet<LocalEventsModel>>();

        _announcementsByDate = new Dictionary<DateTime, HashSet<AnnouncementModel>>();
        _announcementsByCategory = new Dictionary<string, HashSet<AnnouncementModel>>();

        Events = new ObservableCollection<LocalEventsModel>();
        Announcements = new ObservableCollection<AnnouncementModel>();
        RecommendedEvents = new ObservableCollection<RecommendedEventsModel>();  // For recommendations

        SearchCommand = new RelayCommand(Search);
        CloseEventDetailsCommand = new RelayCommand(CloseEventDetails); // Initialize close command
        CloseRecommendedEventDetailsCommand = new RelayCommand(CloseRecommendedEventDetails);

        LoadDummyData();  // Load dummy data for testing
    }

    /// <summary>
    /// Closes the event details pop-up by clearing the selected event.
    /// </summary>
    private void CloseEventDetails()
    {
        SelectedEvent = null; // Hide the pop-up
    }

    private void CloseRecommendedEventDetails()
    {
        SelectedRecommendedEvent = null;
    }


    /// <summary>
    /// Adds a new event to the event queue and relevant sets.
    /// </summary>
    /// <param name="newEvent">The new event to add.</param>
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

    /// <summary>
    /// Adds a new announcement to the dictionary and relevant sets.
    /// </summary>
    /// <param name="newAnnouncement">The new announcement to add.</param>
    /// <exception cref="ArgumentNullException">Thrown when the announcement category is null.</exception>
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

    /// <summary>
    /// Searches for events based on the specified date and category.
    /// </summary>
    /// <param name="date">The date to filter events by.</param>
    /// <param name="category">The category to filter events by.</param>
    /// <returns>A collection of events matching the search criteria.</returns>
    public IEnumerable<LocalEventsModel> SearchEvents(DateTime? date, string category)
    {
        var allEvents = _eventsByCategory.Values.SelectMany(e => e).ToHashSet();

        // Filter by category if a specific category is selected (ignoring "All Categories")
        if (!string.IsNullOrEmpty(category) && category != "All Categories")
        {
            if (_eventsByCategory.ContainsKey(category))
            {
                allEvents = _eventsByCategory[category].ToHashSet();
            }
            else
            {
                return Enumerable.Empty<LocalEventsModel>();
            }
        }

        // Filter by date if a date is selected
        if (date.HasValue)
        {
            if (_eventsByDate.ContainsKey(date.Value))
            {
                allEvents = allEvents.Intersect(_eventsByDate[date.Value]).ToHashSet();
            }
            else
            {
                return Enumerable.Empty<LocalEventsModel>();
            }
        }

        // Increment the SearchCounter for the filtered events and update recommendations
        foreach (var ev in allEvents)
        {
            ev.SearchCounter++;
            UpdateRecommendedEvents(ev);
        }

        return allEvents;
    }

    /// <summary>
    /// Updates the recommended events list based on the search counter.
    /// </summary>
    /// <param name="eventItem">The event item to update recommendations for.</param>
    private void UpdateRecommendedEvents(LocalEventsModel eventItem)
    {
        if (_recommendedEventsDictionary.ContainsKey(eventItem.Id))
        {
            var recommendedEvent = _recommendedEventsDictionary[eventItem.Id];
            recommendedEvent.SearchCounter = eventItem.SearchCounter;
        }
        else
        {
            var newRecommendedEvent = new RecommendedEventsModel(
                eventItem.Id,
                eventItem.EventName, eventItem.EventDate,
                eventItem.Category, eventItem.IsUrgent, eventItem.Priority,
                eventItem.SearchCounter, eventItem.ImagePath);

            _recommendedEventsDictionary.Add(newRecommendedEvent.Id, newRecommendedEvent);
        }

        UpdateRecommendedEventsList();
    }

    /// <summary>
    /// Updates the ObservableCollection for Recommended Events.
    /// </summary>
    private void UpdateRecommendedEventsList()
    {
        var recommended = _recommendedEventsDictionary.Values
            .OrderByDescending(e => e.SearchCounter)
            .Take(10);  // Get top 10 most searched events

        RecommendedEvents.Clear();
        foreach (var ev in recommended)
        {
            RecommendedEvents.Add(ev);
        }
    }

    /// <summary>
    /// Searches for announcements based on the specified date and category.
    /// </summary>
    /// <param name="date">The date to filter announcements by.</param>
    /// <param name="category">The category to filter announcements by.</param>
    /// <returns>A collection of announcements matching the search criteria.</returns>
    public IEnumerable<AnnouncementModel> SearchAnnouncements(DateTime? date, string category)
    {
        var allAnnouncements = _announcementsByCategory.Values.SelectMany(a => a).ToHashSet();

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

    /// <summary>
    /// Executes the search and updates the UI with the filtered events and announcements.
    /// </summary>
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

    /// <summary>
    /// Updates the ObservableCollection for Events.
    /// </summary>
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

    /// <summary>
    /// Updates the ObservableCollection for Announcements.
    /// </summary>
    private void UpdateAnnouncementsList()
    {
        Announcements.Clear();
        foreach (var announcement in _announcementsDictionary.Values)
        {
            Announcements.Add(announcement);
        }
    }

    /// <summary>
    /// Loads dummy data for testing purposes.
    /// </summary>
    private void LoadDummyData()
    {
        // Assuming images are located in the "Images" folder within the project directory
        string imageFolderPath = "../images/eventpictures/";

        // Events
        AddEvent(new LocalEventsModel(1, "Music Festival", DateTime.Now.AddDays(5), "Music", false, 1, $"{imageFolderPath}1.jpg", "A vibrant music festival featuring popular artists."));
        AddEvent(new LocalEventsModel(2, "Sports Day", DateTime.Now.AddDays(3), "Sports", true, 2, $"{imageFolderPath}2.jpg", "A day filled with exciting sports activities and competitions."));
        AddEvent(new LocalEventsModel(3, "Art Exhibition", DateTime.Now.AddDays(7), "Art", false, 3, $"{imageFolderPath}3.jpg", "Explore amazing art pieces by local and international artists."));
        AddEvent(new LocalEventsModel(4, "Food Truck Rally", DateTime.Now.AddDays(2), "Food", true, 1, $"{imageFolderPath}4.jpeg", "Enjoy diverse food options from the best food trucks in town."));
        AddEvent(new LocalEventsModel(5, "Tech Conference", DateTime.Now.AddDays(10), "Technology", false, 2, $"{imageFolderPath}5.jpg", "A conference for tech enthusiasts to network and learn."));
        AddEvent(new LocalEventsModel(6, "Marathon", DateTime.Now.AddDays(15), "Sports", true, 1, $"{imageFolderPath}6.jpeg", "A challenging marathon for runners of all skill levels."));
        AddEvent(new LocalEventsModel(7, "Jazz Night", DateTime.Now.AddDays(8), "Music", false, 2, $"{imageFolderPath}7.png", "An evening of smooth jazz and great company."));
        AddEvent(new LocalEventsModel(8, "Literature Fair", DateTime.Now.AddDays(12), "Literature", true, 3, $"{imageFolderPath}8.jpg", "Celebrate literature with authors, readings, and workshops."));
        AddEvent(new LocalEventsModel(9, "Photography Workshop", DateTime.Now.AddDays(9), "Art", false, 1, $"{imageFolderPath}9.jpg", "Learn photography skills from seasoned professionals."));
        AddEvent(new LocalEventsModel(10, "Yoga Retreat", DateTime.Now.AddDays(4), "Health", true, 2, $"{imageFolderPath}10.jpg", "Relax and rejuvenate at a peaceful yoga retreat."));
        AddEvent(new LocalEventsModel(11, "Film Festival", DateTime.Now.AddDays(6), "Film", false, 2, $"{imageFolderPath}11.jpeg", "Experience the best of international and local cinema."));
        AddEvent(new LocalEventsModel(12, "Stand-up Comedy", DateTime.Now.AddDays(1), "Comedy", true, 1, $"{imageFolderPath}12.jpg", "Enjoy a night of laughter with top comedians."));

        // Announcements
        AddAnnouncement(new AnnouncementModel(1, "Water Cut Announcement", "There will be a water cut in your area.", "Water Announcement", DateTime.Now, true, 1));
        AddAnnouncement(new AnnouncementModel(2, "City Clean-up", "Join us for a city clean-up.", "Clean Up", DateTime.Now.AddDays(1), false, 2));
        AddAnnouncement(new AnnouncementModel(3, "New Library Opening", "A new library is opening in your neighborhood.", "Community", DateTime.Now.AddDays(3), false, 1));
        AddAnnouncement(new AnnouncementModel(4, "Road Maintenance", "Road maintenance scheduled for next week.", "Roads", DateTime.Now.AddDays(2), true, 3));
        AddAnnouncement(new AnnouncementModel(5, "Electricity Outage", "Scheduled electricity outage in your area.", "Electricity", DateTime.Now.AddDays(4), true, 1));
        AddAnnouncement(new AnnouncementModel(6, "Public Holiday", "Reminder: Public holiday next week.", "General", DateTime.Now.AddDays(5), false, 2));
        AddAnnouncement(new AnnouncementModel(7, "COVID-19 Vaccination Drive", "Vaccination drive in your area.", "Health", DateTime.Now.AddDays(2), true, 2));
        AddAnnouncement(new AnnouncementModel(8, "Charity Fundraiser", "Charity fundraiser for local community.", "Charity", DateTime.Now.AddDays(7), false, 1));
        AddAnnouncement(new AnnouncementModel(9, "Local Election", "Upcoming local election details.", "Politics", DateTime.Now.AddDays(10), true, 2));
        AddAnnouncement(new AnnouncementModel(10, "New Park Inauguration", "A new park is opening in your town.", "Community", DateTime.Now.AddDays(3), false, 3));
    }
}
