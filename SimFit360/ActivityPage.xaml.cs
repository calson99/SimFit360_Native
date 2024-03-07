using Microsoft.EntityFrameworkCore;
using SimFit360.Classes;
using SimFit360.Data;
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
	/// Interaction logic for ActivityPage.xaml
	/// </summary>
	public partial class ActivityPage : UserControl
	{
        public int UserId { get; set; }

        public ActivityPage(int userId)
        {
            InitializeComponent();

            UserId = userId;

            // Call the method to load activities
            LoadActivities();
        }

        // Method to load activities from the database and bind to UI
        private void LoadActivities()
        {
            // Using statement ensures proper disposal of resources
            using (var db = new AppDbContext())
            {
                // Query activities from the database including related Maschine entities
                var activities = db.Activities
                                   .Include(a => a.Maschine)
                                   .Where(a => a.UserId == UserId)
                                   .ToList();

                // Set the DataContext of the UserControl to the list of activities
                DataContext = activities;
            }
        }

        // Event handler for the BackButton Click event
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Call the NavigateToMainPage method with the current UserId
            NavigateToMainPage(UserId);
        }

        // Method to navigate to the main page
        public void NavigateToMainPage(int userId)
        {
            // Use MainWindow.Instance to access the main window and navigate to the main page
            MainWindow.Instance.NavigateToMainPage(UserId);
        }

    }
}
