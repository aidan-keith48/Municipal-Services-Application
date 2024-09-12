using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Municipal_Services_Application.Model
{
    /// <summary>
    /// Represents an issue ticket in the municipal services application.
    /// </summary>
    public class IssueTicket
    {
        /// <summary>
        /// Gets or sets the unique ID for each issue.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the location of the issue.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the category of the issue.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the description of the issue.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the list of image attachments for the issue.
        /// </summary>
        public List<string> ImageAttachments { get; set; } = new List<string>();

        /// <summary>
        /// Gets the first image in the list as a preview.
        /// </summary>
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

        /// <summary>
        /// Gets a string representation of all image paths as a comma-separated list.
        /// </summary>
        public string AttachmentsAsString => string.Join(", ", ImageAttachments);
    }
}
