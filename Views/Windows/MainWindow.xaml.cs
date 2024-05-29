using Frontend_ServiceDesk.Views.Pages;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            emailTextBlock.Text = App.enteredAdmin.Email;
        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ordersTab_Checked(object sender, RoutedEventArgs e)
        {
            if (ordersTab.IsChecked == true)
            {
                contentFrame.NavigationService.Navigate(new RequestsPage());
            }
        }

        private void reportsTab_Checked(object sender, RoutedEventArgs e)
        {
            if (ordersTab.IsChecked == true)
            {
                contentFrame.NavigationService.Navigate(new ReportsPage());
            }
        }

        private void contactsTab_Checked(object sender, RoutedEventArgs e)
        {
            if (ordersTab.IsChecked == true)
            {
                contentFrame.NavigationService.Navigate(new ContactsPage());
            }
        }

    }
}
