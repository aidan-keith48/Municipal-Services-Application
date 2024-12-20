﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Municipal_Services_Application.Model
{
    public class RecommendedEventsModel
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string Category { get; set; }
        public bool IsUrgent { get; set; } // True if event is urgent
        public int Priority { get; set; }   // 1 = High, 2 = Medium, 3 = Low
        public int SearchCounter { get; set; }   // New counter for recommendations
        public string ImagePath { get; set; }   // Path to the event image

        public RecommendedEventsModel(int id, string eventName, DateTime eventDate, string category, bool isUrgent, int priority, int searchCounter, string imagePath)
        {
            Id = id;
            EventName = eventName;
            EventDate = eventDate;
            Category = category;
            IsUrgent = isUrgent;
            Priority = priority;
            SearchCounter = searchCounter;
            ImagePath = imagePath;
        }
    }
}
