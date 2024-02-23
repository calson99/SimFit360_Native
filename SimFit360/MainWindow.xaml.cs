﻿using SimFit360.Data;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            NavigateToMainPage();
        }

        public void NavigateToLoginPage()
        {
            MainFrame.Navigate(new LoginPage());
        }

        public void NavigateToMainPage()
        {
            MainFrame.Navigate(new MainPage());
        }
        public void NavigateToActivityPage()
        {
            MainFrame.Navigate(new ActivityPage());
        }
        public void NavigateToSportPage()
        {
            MainFrame.Navigate(new SportPage());
        }

    }
}