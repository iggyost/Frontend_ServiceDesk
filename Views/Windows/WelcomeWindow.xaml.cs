using Frontend_ServiceDesk.ApplicationData;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Frontend_ServiceDesk.Views.Windows
{
    /// <summary>
    /// Interaction logic for WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        public static List<Task> myTasks = new List<Task>();
        public static bool isBusy = false;
        public WelcomeWindow()
        {
            InitializeComponent();

        }
        public async Task LoadingAnimation()
        {
            loadingBorder.Visibility = Visibility.Visible;
            loadingIndicator.Visibility = Visibility.Visible;

            var animation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1.5),
                FillBehavior = FillBehavior.Stop
            };

            animation.Completed += (s, a) => loadingBorder.Opacity = 1;

            loadingBorder.BeginAnimation(UIElement.OpacityProperty, animation);
        }
        public async Task LoginUser(string email, string password)
        {
            isBusy = true;
            await Task.Delay(2000);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{App.conString}users/login/{email}/{password}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<User>(content);
                App.enteredUser = user;

                ClientWindow clientWindow = new ClientWindow();
                clientWindow.Show();
                this.Close();
            }
            else
            {
                await LoginAdmin(email, password);
            }
            loadingBorder.Visibility = Visibility.Collapsed;
            loadingIndicator.Visibility = Visibility.Collapsed;
            isBusy = false;
        }
        public async Task LoginAdmin(string email, string password)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{App.conString}admins/login/{email}/{password}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var admin = JsonConvert.DeserializeObject<Admin>(content);
                App.enteredAdmin = admin;

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                CustomDisplayAlert alert = new CustomDisplayAlert("Ошибка авторизации пользователя", "Пользователь с веденными данными не найден!");
                alert.ShowDialog();
            }
        }
        public async Task RegUser(string email, string password)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{App.conString}users/reg/{email}/{password}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<User>(content);
                App.enteredUser = user;
            }
            else
            {
                CustomDisplayAlert alert = new CustomDisplayAlert("Ошибка регистрации пользователя", "Пользователь с таким Email уже есть!");
                alert.ShowDialog();
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

        private void topPanelBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private async void enterButton_Click(object sender, RoutedEventArgs e)
        {
            if (emailField.Text.Length != 0 && passwordField.Password.Length != 0)
            {
                LoadingAnimation();
                await Task.WhenAll(LoginUser(emailField.Text, passwordField.Password));
            }
        }

        private async void regButton_Click(object sender, RoutedEventArgs e)
        {
            Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            if (emailFieldReg.Text != string.Empty && passwordFieldReg.Password != string.Empty && passwordFieldCompleteReg.Password != string.Empty)
            {
                if (emailFieldReg.Text.Length > 3 && passwordFieldReg.Password.Length > 3 && passwordFieldCompleteReg.Password.Length > 3)
                {
                    if (emailRegex.IsMatch(emailFieldReg.Text))
                    {
                        if (passwordFieldReg.Password == passwordFieldCompleteReg.Password)
                        {
                            await RegUser(emailFieldReg.Text, passwordFieldReg.Password);
                            ClientWindow clientWindow = new ClientWindow();
                            clientWindow.Show();
                            this.Close();
                        }
                        else
                        {
                            CustomDisplayAlert alert = new CustomDisplayAlert("Пароли не совпадают!", "");
                            alert.ShowDialog();
                        }
                    }
                    else
                    {
                        CustomDisplayAlert alert = new CustomDisplayAlert("Email не соответствует формату", "");
                        alert.ShowDialog();
                    }
                }
                else
                {
                    CustomDisplayAlert alert = new CustomDisplayAlert("Миинимальная длина полей '4' символа", "");
                    alert.ShowDialog();
                }
            }
            else
            {
                CustomDisplayAlert alert = new CustomDisplayAlert("Не все поля заполнены!", "");
                alert.ShowDialog(); 
            }
        }

        private void toRegButton_Click(object sender, RoutedEventArgs e)
        {
            authorizationGrid.Visibility = Visibility.Collapsed;
            registrationGrid.Visibility = Visibility.Visible;

            toRegGrid.Visibility = Visibility.Collapsed;
            toLoginGrid.Visibility = Visibility.Visible;
        }

        private void toLoginButton_Click(object sender, RoutedEventArgs e)
        {
            authorizationGrid.Visibility = Visibility.Visible;
            registrationGrid.Visibility = Visibility.Collapsed;

            toRegGrid.Visibility = Visibility.Visible;
            toLoginGrid.Visibility = Visibility.Collapsed;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(1000);
                if (Properties.Settings.Default.isNeedToRemember == true)
            {
                LoadingAnimation();
                await Task.Delay(1000);
                emailField.Text = Properties.Settings.Default.email;
                passwordField.Password = Properties.Settings.Default.password;
                bool isAdmin = Properties.Settings.Default.isAdmin;
                if (isAdmin == true)
                {
                    await Task.Delay(1000);
                    await LoginAdmin(emailField.Text, passwordField.Password);
                }
                else
                {
                    await Task.Delay(1000);
                    await LoginUser(emailField.Text, passwordField.Password);
                }
            }
            else
            {

            }
        }

        private void rememberCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (rememberCheckBox.IsChecked == true)
            {
                Properties.Settings.Default.isNeedToRemember = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.isNeedToRemember = false;
                Properties.Settings.Default.Save();
            }
        }
    }
}
