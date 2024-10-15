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
    /// Interaction logic for LocalEvent_Annoucments.xaml
    /// </summary>
    public partial class LocalEvent_Annoucments : UserControl
    {
        public LocalEvent_Annoucments()
        {
            InitializeComponent();
            DataContext = new LocalEventsViewModel();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
