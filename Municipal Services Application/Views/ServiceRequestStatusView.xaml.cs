using Municipal_Services_Application.Repositories;
using Municipal_Services_Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Municipal_Services_Application.Views
{
    /// <summary>
    /// Interaction logic for ServiceRequestStatus.xaml
    /// </summary>
    public partial class ServiceRequestStatus : UserControl
    {
        public ServiceRequestStatus()
        {
            InitializeComponent();
            // Set the DataContext to the ViewModel
            var repository = new ServiceRequestRepository(); // Create an instance of the repository
            DataContext = new ServiceRequestStatusViewModel(repository); // Pass the repository to the ViewModel
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
