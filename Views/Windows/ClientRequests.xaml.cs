using Frontend_ServiceDesk.ApplicationData;
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
    /// Interaction logic for ClientRequests.xaml
    /// </summary>
    public partial class ClientRequests : Window
    {
        public ClientRequests()
        {
            InitializeComponent();
        }

        public async Task LoadRequests()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{App.conString}requestsview/get/user/{App.enteredUser.UserId}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var userRequests = JsonConvert.DeserializeObject<RequestsView[]>(content);
                requestsListView.ItemsSource = userRequests.ToList();
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadRequests();
        }

        private void ellipseStatus_Loaded(object sender, RoutedEventArgs e)
        {
            Ellipse ellipse = sender as Ellipse;
            var currentStatus = int.Parse(ellipse.Tag.ToString());
            switch (currentStatus)
            {
                case 1:
                    ellipse.Fill = Brushes.Red;
                    break;
                case 2:
                    ellipse.Fill = Brushes.DarkOrange;
                    break;
                case 3:
                    ellipse.Fill = Brushes.Green;
                    break;
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
