using Municipal_Services_Application.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Municipal_Services_Application.ViewModels
{
    public class IssueListViewModel : BaseViewModel
    {
        public ObservableCollection<IssueTicket> Issues { get; set; }

        public IssueListViewModel(Dictionary<int, IssueTicket> issueDictionary)
        {
            Issues = new ObservableCollection<IssueTicket>(issueDictionary.Values);
        }
    }
}
