using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        /// Gets or sets the list of attachments for the issue (image paths, PDFs, etc.).
        /// </summary>
        public List<string> Attachments { get; set; } = new List<string>();

        /// <summary>
        /// Gets a preview image if the first attachment is an image file.
        /// </summary>
        public BitmapImage ImagePreview
        {
            get
            {
                if (Attachments != null && Attachments.Any() && IsImageFile(Attachments.First()))
                {
                    return new BitmapImage(new Uri(Attachments.First(), UriKind.Absolute));
                }
                return null;
            }
        }

        /// <summary>
        /// Gets a string representation of all attachment paths as a comma-separated list.
        /// </summary>
        public string AttachmentsAsString => string.Join(", ", Attachments);

        /// <summary>
        /// Checks if the given file path is an image file.
        /// </summary>
        /// <param name="filePath">The file path to check.</param>
        /// <returns>True if the file is an image; otherwise, false.</returns>
        private bool IsImageFile(string filePath)
        {
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".bmp" };
            string extension = Path.GetExtension(filePath).ToLower();
            return imageExtensions.Contains(extension);
        }

        /// <summary>
        /// Gets a list of non-image file attachments (e.g., PDFs, Word documents).
        /// </summary>
        public List<string> NonImageAttachments
        {
            get
            {
                return Attachments.Where(file => !IsImageFile(file)).ToList();
            }
        }
    }
}
