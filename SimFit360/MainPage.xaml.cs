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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimFit360
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : UserControl
    {
        public int UserId { get; set; }
        public MainPage(int userId)
        {
            InitializeComponent();
            UserId = userId;
        }


        private void StartSportClick(object sender, RoutedEventArgs e)
        {
            // Pass the UserId to the SportPage constructor
            SportPage sportPage = new SportPage(UserId);

            // Navigate to the SportPage
            MainWindow.Instance.NavigateToSportPage(sportPage);

            // Start the timer in the SportPage
            sportPage.StartTimer();
        }
        private void BekijkAlleActiviteit(object sender, RoutedEventArgs e)
        {
            ActivityPage activityPage = new ActivityPage(UserId);

            MainWindow.Instance.NavigateToActivityPage(activityPage);
        }

        private void Loguit(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.NavigateToLoginPage();
        }
    }
}
