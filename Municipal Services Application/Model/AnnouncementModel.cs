using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Municipal_Services_Application.Model
{
    public class AnnouncementModel
    {
        public int Id { get; set; }
        public string AnnouncementTitle { get; set; }
        public string AnnouncementContent { get; set; }

        public string AnnouncementCategory { get; set; }
        public DateTime AnnouncementDate { get; set; }
        public bool IsUrgent { get; set; } // True if the announcement is urgent
        public int Priority { get; set; }   // 1 = High, 2 = Medium, 3 = Low


        public AnnouncementModel(int id, string title, string content, string category, DateTime date, bool isUrgent, int priority)
        {
            Id = id;
            AnnouncementTitle = title;
            AnnouncementContent = content;
            AnnouncementCategory = category;
            AnnouncementDate = date;
            IsUrgent = isUrgent;
            Priority = priority;
        }
    }

}
