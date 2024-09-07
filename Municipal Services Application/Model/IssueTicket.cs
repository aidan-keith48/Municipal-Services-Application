using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Municipal_Services_Application.Model
{
    public class IssueTicket
    {
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public List<string> ImageAttachments { get; set; } = new List<string>();

        // Constructor to initialize an empty issue
        public IssueTicket()
        {
            Location = string.Empty;
            Category = string.Empty;
            Description = string.Empty;
        }
    }
}
