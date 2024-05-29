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
using System.Windows.Shapes;

namespace Frontend_ServiceDesk.Views.Windows
{
    /// <summary>
    /// Interaction logic for CustomDisplayAlert.xaml
    /// </summary>
    public partial class CustomDisplayAlert : Window
    {
        public CustomDisplayAlert()
        {
            InitializeComponent();
        }
        public CustomDisplayAlert(string title, string content)
        {
            InitializeComponent();
            titleText.Text = title;
            contentText.Text = content;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
