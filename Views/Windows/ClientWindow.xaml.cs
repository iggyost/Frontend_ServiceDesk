using Frontend_ServiceDesk.ApplicationData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        public ClientWindow()
        {
            InitializeComponent();
            userEmailTextBlock.Text = App.enteredUser.Email;
        }
        public async Task SendRequest()
        {
            HttpClient client = new HttpClient();
            var selectedCategory = categoriesComboBox.SelectedItem as Category;
            HttpResponseMessage response = await client.GetAsync($"{App.conString}requestsview/send/{nameField.Text}/{descriptionField.Text}/{selectedCategory.CategoryId}/{App.enteredUser.UserId}");
            if (response.IsSuccessStatusCode)
            {
                CustomDisplayAlert alert = new CustomDisplayAlert("Запрос отправлен", "Ваш запрос успешно был отправлен в техподдержку!");
                alert.ShowDialog();
                nameField.Text = string.Empty;
                descriptionField.Text = string.Empty;
                categoriesComboBox.SelectedItem = null;
            }
        }
        public async Task GetRequestCount()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{App.conString}requestsview/get/user/{App.enteredUser.UserId}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var userRequests = JsonConvert.DeserializeObject<RequestsView[]>(content).ToList();
                CountingBadge.Badge = userRequests.Count.ToString();
            }
        }
        public async Task GetCategories()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{App.conString}categoriesview/get");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<Category>>(content);
                categoriesComboBox.ItemsSource = categories;
                categoriesComboBox.DisplayMemberPath = "Name";
            }
        }
        private async void sendRequestButton_Click(object sender, RoutedEventArgs e)
        {
            if (nameField.Text != string.Empty && descriptionField.Text != string.Empty && categoriesComboBox.SelectedItem != null)
            {
                await SendRequest();
            }
            else
            {
                CustomDisplayAlert alert = new CustomDisplayAlert("Заполните шаблон запроса!", "Для отправки запроса в поддержку необходимо полностью заполнить шаблон");
                alert.ShowDialog();
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            WelcomeWindow welcomeWindow = new WelcomeWindow();
            welcomeWindow.Show();
            Properties.Settings.Default.Reset();
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void myRequestsButoon_Click(object sender, RoutedEventArgs e)
        {
            ClientRequests clientRequests = new ClientRequests();
            clientRequests.ShowDialog();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await GetCategories();
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
                await GetRequestCount();
                await Task.Delay(5000);
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
