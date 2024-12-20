﻿using System;

namespace Municipal_Services_Application.Model
{
    public class LocalEventsModel
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string Category { get; set; }
        public bool IsUrgent { get; set; } // True if event is urgent
        public int Priority { get; set; }   // 1 = High, 2 = Medium, 3 = Low
        public int SearchCounter { get; set; }   // New counter for recommendations
        public string ImagePath { get; set; } // Relative path for the event image
        public string Description { get; set; } // Detailed description of the event

        public LocalEventsModel(int id, string eventName, DateTime eventDate, string category, bool isUrgent, int priority, string imagePath, string description)
        {
            Id = id;
            EventName = eventName;
            EventDate = eventDate;
            Category = category;
            IsUrgent = isUrgent;
            Priority = priority;
            SearchCounter = 0;
            ImagePath = imagePath;
            Description = description;
        }
    }
}
