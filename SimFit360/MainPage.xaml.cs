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
        public MainPage()
        {
            InitializeComponent();
            MainFrame.Navigated += MainFrame_Navigated; 
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            
        }

        private void StartSportClick(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.NavigateToSportPage();
        }

        private void BekijkAlleActiviteit(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.NavigateToActivityPage();
        }

        private void Loguit(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.NavigateToLoginPage();
        }
    }
}
