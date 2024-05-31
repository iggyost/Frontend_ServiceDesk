using Frontend_ServiceDesk.ApplicationData;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Frontend_ServiceDesk
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string conString = "http://192.168.0.10:45455/api/";
        public static User enteredUser = new User();
        public static Admin enteredAdmin = new Admin();
        public static bool isNeedToRemember = false;
    }
}