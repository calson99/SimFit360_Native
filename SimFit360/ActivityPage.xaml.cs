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
            LoadActivities();
        }

        private void LoadActivities()
        {
            using (var db = new AppDbContext())
            {
                var activities = db.Activities
                                   .Include(a => a.Maschine)
                                   .Where(a => a.UserId == UserId)
                                   .ToList();
                DataContext = activities;
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToMainPage(UserId);
        }

        public void NavigateToMainPage(int userId)
        {
            MainWindow.Instance.NavigateToMainPage(UserId);
        }
   
    }
}
