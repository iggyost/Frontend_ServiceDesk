﻿using Frontend_ServiceDesk.ApplicationData;
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
    /// Interaction logic for ReportsPage.xaml
    /// </summary>
    public partial class ReportsPage : Page
    {
        public ReportsPage()
        {
            InitializeComponent();
        }
        public async Task GetReports()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{App.conString}reportsview/get");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ReportsView[]>(content);
                reportsListView.ItemsSource = data.ToList();
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            loadingIndicator.Visibility = Visibility.Visible;
            await GetReports();
            loadingIndicator.Visibility = Visibility.Collapsed;
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
    }
}
