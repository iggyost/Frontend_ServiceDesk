using Frontend_ServiceDesk.ApplicationData;
using Frontend_ServiceDesk.Views.Windows;
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
    /// Interaction logic for RequestsPage.xaml
    /// </summary>
    public partial class RequestsPage : Page
    {
        public static List<RequestsView> currentRequests = new List<RequestsView>();
        public static List<RequestsView> newRequests = new List<RequestsView>();
        public static bool isSearchFieldFocus = false;
        public RequestsPage()
        {
            InitializeComponent();
        }
        public async Task GetCategories()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{App.conString}categoriesview/get");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<Category[]>(content).ToList();
                Category allCategory = new Category()
                {
                    Name = "Все",
                };
                categories.Insert(3, allCategory);
                categoriesComboBox.ItemsSource = categories;
                categoriesComboBox.DisplayMemberPath = "Name";
            }
        }
        public async Task GetRequests()
        {
            loadingIndicator.Visibility = Visibility.Visible;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{App.conString}requestsview/get");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var requests = JsonConvert.DeserializeObject<RequestsView[]>(content);
                if (requests != null)
                {
                    currentRequests = requests.ToList();
                    newRequests = currentRequests;
                }
                else
                {
                    CustomDisplayAlert alert = new CustomDisplayAlert("Новых запросов нет", "");
                    alert.ShowDialog();
                }
            }
            loadingIndicator.Visibility = Visibility.Collapsed;
        }
        public async Task ListenerRequests()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{App.conString}requestsview/get");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var requests = JsonConvert.DeserializeObject<RequestsView[]>(content);
                if (requests != null)
                {
                    newRequests = requests.ToList();
                }
                else
                {
                    CustomDisplayAlert alert = new CustomDisplayAlert("Новых запросов нет", "");
                    alert.ShowDialog();
                }
            }
        }

        

        private async void firstNewRadiobutton_Checked(object sender, RoutedEventArgs e)
        {
            if (firstNewRadiobutton.IsChecked == true)
            {
                firstOldRadioButton.IsChecked = false;

                searchField.Text = string.Empty;
                senderField.Text = string.Empty;



                categoriesComboBox.SelectedIndex = 3;

                await GetRequests();
                requestsListView.ItemsSource = currentRequests.OrderByDescending(x => x.Date);
            }
            else if (firstNewRadiobutton.IsChecked == false && firstOldRadioButton.IsChecked == false)
            {
                await GetRequests();
                requestsListView.ItemsSource = currentRequests;
            }
        }

        private async void firstOldRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (firstOldRadioButton.IsChecked == true)
            {
                firstNewRadiobutton.IsChecked = false;

                searchField.Text = string.Empty;
                senderField.Text = string.Empty;

                categoriesComboBox.SelectedIndex = 3;

                await GetRequests();
                requestsListView.ItemsSource = currentRequests.OrderBy(x => x.Date);
            }
            else if (firstNewRadiobutton.IsChecked == false && firstOldRadioButton.IsChecked == false)
            {
                await GetRequests();
                requestsListView.ItemsSource = currentRequests;
            }
        }
        private async void searchField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (searchField.Text != string.Empty)
                {
                    isSearchFieldFocus = true;
                    senderField.Text = string.Empty;

                    firstNewRadiobutton.IsChecked = false;
                    firstOldRadioButton.IsChecked = false;

                    categoriesComboBox.SelectedIndex = 3;

                    var searchName = searchField.Text;
                    await GetRequests();
                    requestsListView.ItemsSource = currentRequests.Where(x => x.Name.Contains(searchName)).ToList();
                }
                else
                {
                    isSearchFieldFocus = false;
                    await GetRequests();
                    requestsListView.ItemsSource = currentRequests;
                }
            }
            else
            {
                isSearchFieldFocus = false;
            }
        }
        private async void senderField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (senderField.Text != string.Empty)
                {
                    isSearchFieldFocus = true;
                    searchField.Text = string.Empty;

                    firstNewRadiobutton.IsChecked = false;
                    firstOldRadioButton.IsChecked = false;

                    categoriesComboBox.SelectedIndex = 3;

                    var searchName = senderField.Text;
                    await GetRequests();
                    requestsListView.ItemsSource = currentRequests.Where(x => x.UserEmail.Contains(searchName)).ToList();
                }
                else
                {
                    isSearchFieldFocus = false;
                    await GetRequests();
                    requestsListView.ItemsSource = currentRequests;
                }
            }
            else
            {
                isSearchFieldFocus = false;
                await GetRequests();
                requestsListView.ItemsSource = currentRequests;
            }
        }
        private async void categoriesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            searchField.Text = string.Empty;
            senderField.Text = string.Empty;

            firstNewRadiobutton.IsChecked = false;
            firstOldRadioButton.IsChecked = false;

            switch (categoriesComboBox.SelectedIndex)
            {
                case 0:
                    await GetRequests();
                    requestsListView.ItemsSource = currentRequests.Where(x => x.CategoryId == 1).ToList();
                    break;
                case 1:
                    await GetRequests();
                    requestsListView.ItemsSource = currentRequests.Where(x => x.CategoryId == 2).ToList();
                    break;
                case 2:
                    await GetRequests();
                    requestsListView.ItemsSource = currentRequests.Where(x => x.CategoryId == 3).ToList();
                    break;
                case 3:
                    await GetRequests();
                    requestsListView.ItemsSource = currentRequests.ToList();
                    break;
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await GetRequests();
            requestsListView.ItemsSource = currentRequests;
            await GetCategories();

            while (true)
            {
                await Task.Delay(5000);
                await ListenerRequests();
                if (newRequests.SequenceEqual(currentRequests) != true)
                {
                    if (firstNewRadiobutton.IsChecked == true || firstOldRadioButton.IsChecked == true)
                    {
                        switch (firstNewRadiobutton.IsChecked)
                        {
                            case true:
                                await GetRequests();
                                requestsListView.ItemsSource = currentRequests.OrderByDescending(x => x.Date);
                                break;
                            case false:
                                await GetRequests();
                                requestsListView.ItemsSource = currentRequests.OrderBy(x => x.Date);
                                break;
                        }
                    }
                    else if (searchField.Text != string.Empty || senderField.Text != string.Empty)
                    {
                        if (isSearchFieldFocus == true)
                        {
                            switch (searchField.Text != string.Empty)
                            {
                                case true:
                                    await GetRequests();
                                    requestsListView.ItemsSource = currentRequests.Where(x => x.Name.Contains(searchField.Text)).ToList();
                                    break;
                                case false:
                                    await GetRequests();
                                    requestsListView.ItemsSource = currentRequests.Where(x => x.UserEmail.Contains(senderField.Text)).ToList();
                                    break;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else if (categoriesComboBox.SelectedIndex != 3)
                    {
                        switch (categoriesComboBox.SelectedIndex)
                        {
                            case 0:
                                await GetRequests();
                                requestsListView.ItemsSource = currentRequests.Where(x => x.CategoryId == 1).ToList();
                                break;
                            case 1:
                                await GetRequests();
                                requestsListView.ItemsSource = currentRequests.Where(x => x.CategoryId == 2).ToList();
                                break;
                            case 2:
                                await GetRequests();
                                requestsListView.ItemsSource = currentRequests.Where(x => x.CategoryId == 3).ToList();
                                break;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    await GetRequests();
                    requestsListView.ItemsSource = currentRequests;
                }
            }
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
        public async Task GetRequestInWork(int requestId)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{App.conString}reportsview/set/work/{App.enteredAdmin.AdminId}/{requestId}");
            if (response.IsSuccessStatusCode)
            {
                CustomDisplayAlert alert = new CustomDisplayAlert("Запрос принят в работу", "");
            }
            else
            {
                CustomDisplayAlert alert = new CustomDisplayAlert("Вы уже приняли данный запрос", "");
            }
        }
        private async void acceptRequestButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var requestId = int.Parse(button.Tag.ToString());
            loadingIndicator.Visibility = Visibility.Visible;
            await GetRequestInWork(requestId);
            await Task.Delay(4000);
            loadingIndicator.Visibility = Visibility.Collapsed;
        }
    }
}
