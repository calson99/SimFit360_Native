using SimFit360.Classes;
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
using System.Windows.Threading;
using SimFit360.Data;

namespace SimFit360
{
    /// <summary>
    /// Interaction logic for SportPage.xaml
    /// </summary>
    public partial class SportPage : UserControl
    {
        private int Difficulty { get; set; } = 0;
        private TimeSpan workoutTime = new TimeSpan(0, 0, 0);
        private const int MaxDifficulty = 10; // Maximum difficulty level
        private DispatcherTimer timer;
        private bool isPaused = false;
        public int UserId { get; set; }
        public SportPage(int userId)
        {
            InitializeComponent();
            UpdateDifficultyText();
            UserId = userId;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Update every second
            timer.Tick += Timer_Tick;
        }

        public void StartTimer()
        {
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!isPaused)
            {
                // Update the workout time and display it
                workoutTime = workoutTime.Add(TimeSpan.FromSeconds(1));
                // Update UI on the main thread
                WorkoutTimeText.Text = workoutTime.ToString(@"hh\:mm\:ss");

                // Calculate and display calories burned
                double caloriesBurned = CalculateKcal();
                CaloriesText.Text = $"Calories: {caloriesBurned:F2}"; // Display calories with two decimal places
            }
        }

        private void DifficultyIncrease_Click(object sender, RoutedEventArgs e)
        {
            // Check if difficulty is less than the maximum before incrementing
            if (Difficulty < MaxDifficulty)
            {
                Difficulty++;
                UpdateDifficultyText();
            }
        }

        private void DifficultyDecrease_Click(object sender, RoutedEventArgs e)
        {
            Difficulty = Math.Max(0, Difficulty - 1);
            UpdateDifficultyText();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            // Pause the timer
            isPaused = true;

            // Prompt the user to save the workout if it's under 3 minutes
            if (workoutTime.TotalMinutes < 3)
            {
                MessageBoxResult result = MessageBox.Show("The workout is under 3 minutes. Do you want to save it?", "Save Workout", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Save workout information to the database
                    SaveWorkout();
                    // Reset everything
                    ResetWorkout();
                }
                else
                {
                    // Unpause the timer if the user chooses not to save
                    isPaused = false;
                    // Handle other actions if needed
                }
            }
            else
            {
                // Save workout information to the database
                SaveWorkout();
                // Reset everything
                ResetWorkout();
            }
        }

        private void ResetWorkout()
        {
            // Reset workout variables
            workoutTime = new TimeSpan(0, 0, 0);
            isPaused = false;

            // Reset UI
            WorkoutTimeText.Text = "00:00:00";
            CaloriesText.Text = "Calories: 0.00";
        }

        private void UpdateDifficultyText()
        {
            // Update and display the difficulty in the TextBlock
            DifficultyText.Text = Difficulty.ToString();
        }

        private void SaveWorkout()
        {
            using (AppDbContext dbContext = new AppDbContext())
            {
                try
                {
                    // Create an instance of Activity
                    Activity workoutActivity = new Activity
                    {
                        Time = workoutTime,
                        Date = DateTime.Now,
                        Kcal = (int)CalculateKcal(),
                        UserId = UserId,
                        MaschineId = 1 // Replace with the correct property name
                        // Add other properties as needed
                    };

                    // Add the workoutActivity to the Activities DbSet
                    dbContext.Activities.Add(workoutActivity);

                    // Save changes to the database
                    dbContext.SaveChanges();

                    // Show confirmation message
                    MessageBox.Show("Workout saved!", "Save Workout", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Navigate back to MainPage
                    NavigateToMainPage();
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that may occur during the database operation
                    MessageBox.Show($"Error saving workout: {ex.Message}", "Save Workout Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void NavigateToMainPage()
        {
            MainWindow.Instance.NavigateToMainPage(UserId);
        }
        private double CalculateKcal()
        {
            // Define baseline calorie burn rate per second
            const double BaselineCaloriesPerSecond = 0.1;

            // Adjust the baseline based on difficulty
            double adjustedCaloriesPerSecond = BaselineCaloriesPerSecond + (Difficulty * 0.02);

            // Calculate total calories burned
            double totalCalories = adjustedCaloriesPerSecond * workoutTime.TotalSeconds;

            return totalCalories;
        }

    }

}

