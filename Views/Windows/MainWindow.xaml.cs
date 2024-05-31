using Frontend_ServiceDesk.Views.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public async Task GetCount()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{App.conString}reportsview/get/count/{App.enteredAdmin.AdminId}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                CountingBadge.Badge = JsonConvert.DeserializeObject<int>(content).ToString();
            }
        }
        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
            if (reportsTab.IsChecked == true)
            {
                contentFrame.NavigationService.Navigate(new ReportsPage());
            }
        }

        private void myRequestsTab_Checked(object sender, RoutedEventArgs e)
        {
            if (myRequestsTab.IsChecked == true)
            {
                contentFrame.NavigationService.Navigate(new MyRequestsPage());
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            WelcomeWindow welcomeWindow = new WelcomeWindow();
            welcomeWindow.Show();
            Properties.Settings.Default.Reset();
            Properties.Settings.Default.Save();
            this.Close();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.isNeedToRemember == true)
            {
                if (App.enteredUser.Email != null)
                {
                    Properties.Settings.Default.email = App.enteredUser.Email;
                    Properties.Settings.Default.password = App.enteredUser.Password;
                    Properties.Settings.Default.isAdmin = false;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    Properties.Settings.Default.email = App.enteredAdmin.Email;
                    Properties.Settings.Default.password = App.enteredAdmin.Password;
                    Properties.Settings.Default.isAdmin = true;
                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                Properties.Settings.Default.email = string.Empty;
                Properties.Settings.Default.password = string.Empty;
                Properties.Settings.Default.isAdmin = false;
                Properties.Settings.Default.Save();
            }
            while (true)
            {
                await GetCount();
                await Task.Delay(5000);
            }
        }

    }
}
