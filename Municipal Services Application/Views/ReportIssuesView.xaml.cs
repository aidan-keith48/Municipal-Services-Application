using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Municipal_Services_Application.Views
{
    /// <summary>
    /// Interaction logic for ReportIssuesView.xaml
    /// </summary>
    public partial class ReportIssuesView : System.Windows.Controls.UserControl
    {
        public ReportIssuesView()
        {
            InitializeComponent();
        }

        private void AttachFileBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Handle file selections, possibly setting some UI element to show selected file names
            }
        }

        private void SubmitReportBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Report submitted successfully!");
        }
    }
}
