using Municipal_Services_Application.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Municipal_Services_Application.ViewModels
{
    /// <summary>
    /// Represents the view model for the issue list.
    /// </summary>
    public class IssueListViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets or sets the collection of issue tickets.
        /// </summary>
        public ObservableCollection<IssueTicket> Issues { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IssueListViewModel"/> class.
        /// </summary>
        /// <param name="issueDictionary">The dictionary of issue tickets.</param>
        public IssueListViewModel(Dictionary<int, IssueTicket> issueDictionary)
        {
            Issues = new ObservableCollection<IssueTicket>(issueDictionary.Values);
        }
    }
}
//---------------------------------------------------------Enf of file---------------------------------------------------------//