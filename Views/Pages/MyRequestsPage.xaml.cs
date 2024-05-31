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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Frontend_ServiceDesk.Views.Pages
{
    /// <summary>
    /// Interaction logic for MyRequestsPage.xaml
    /// </summary>
    public partial class MyRequestsPage : Page
    {
        public MyRequestsPage()
        {
            InitializeComponent();
        }
        public async Task GetAdminRequests()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{App.conString}reportsview/get/admin/{App.enteredAdmin.AdminId}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ReportsView[]>(content);
                myRequestsListView.ItemsSource = data.ToList();
            }
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await GetAdminRequests();
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

        private void readyButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
