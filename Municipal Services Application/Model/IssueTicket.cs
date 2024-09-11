using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Municipal_Services_Application.Model
{
    public class IssueTicket
    {
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public List<string> ImageAttachments { get; set; } = new List<string>();

        // Assuming you want to show the first image in the list as a preview
        public BitmapImage ImagePreview
        {
            get
            {
                if (ImageAttachments != null && ImageAttachments.Any())
                {
                    return new BitmapImage(new Uri(ImageAttachments.First(), UriKind.Absolute));
                }
                return null;
            }
        }

        // Helper to display all image paths as a string (if needed for listing attachments)
        public string AttachmentsAsString => string.Join(", ", ImageAttachments);
    }
}
