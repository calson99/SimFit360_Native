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
        private int Difficulty { get; set; } = 1;
        private TimeSpan workoutTime = new TimeSpan(0, 0, 0);
        private int currentlevel = 1;
        private List<DispatcherTimer> levelTimers = new List<DispatcherTimer>();
        private int[] levelTimes = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        private List<int> countdowns = new List<int>();
        private double totalSeconds = 0;
        private DispatcherTimer timer;
        private bool isPaused = false;
        public int UserId { get; set; }
        public SportPage(int userId)
        {
            InitializeComponent();
            UpdateDifficultyText();
            UserId = userId;
            InitializeTimers();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Update every second
            timer.Tick += Timer_Tick;
        }

        private void InitializeTimers()
        {
            for (int i = 0; i < 10; i++)
            {
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += LevelTimer_Tick;
                levelTimers.Add(timer);
                countdowns.Add(0); // Start allemaal op nul
            }
            UpdateTimerStatus();
        }

        public void StartTimer()
        {
            timer.Start();
        }

        private void LevelTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < countdowns.Count; i++)
            {
                if (i == Difficulty - 1) // Controleer of de huidige teller overeenkomt met het huidige niveau
                {
                    countdowns[i]++; // Tel 1 seconde bij de teller op
                }
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!isPaused)
            {
                // Update the workout time and display it
                workoutTime = workoutTime.Add(TimeSpan.FromSeconds(1));
                // Update UI on the main thread
                WorkoutTimeText.Text = workoutTime.ToString(@"hh\:mm\:ss");

                totalSeconds++;
                // Calculate and display calories burned
                double caloriesBurned = CalculateKcal();
                CaloriesText.Text = $"Calories: {caloriesBurned:F2}"; // Display calories with two decimal places
            }
        }

        private void UpdateTimerStatus()
        {
            for (int i = 0; i < levelTimers.Count; i++)
            {
                if (i == Difficulty - 1)
                {
                    levelTimers[i].Start();
                }
                else
                {
                    levelTimers[i].Stop();
                }
            }
        }

        private void DifficultyIncrease_Click(object sender, RoutedEventArgs e)
        {
            // Check if difficulty is less than the maximum before incrementing
            if (Difficulty < 10)
            {
                Difficulty++;
                UpdateTimerStatus();
                UpdateDifficultyText();
            }
        }

        private void DifficultyDecrease_Click(object sender, RoutedEventArgs e)
        {
            if (Difficulty > 1)
            {
                Difficulty--;
                UpdateTimerStatus();
                UpdateDifficultyText();
            }
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
                    ResetWorkout();
                    NavigateToMainPage();
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

            // Calculate total difficulty
            double totalDifficulty = 0;
            for (int i = 0; i < countdowns.Count; i++)
            {
                totalDifficulty += (i + 1) * countdowns[i]; // difficulty * seconds
            }

            // Adjust the baseline based on total difficulty
            double adjustedCaloriesPerSecond = BaselineCaloriesPerSecond + (totalDifficulty * 0.0005);

            // Calculate total calories burned
            double totalCalories = adjustedCaloriesPerSecond * workoutTime.TotalSeconds;

            return totalCalories;
        }

    }

}

