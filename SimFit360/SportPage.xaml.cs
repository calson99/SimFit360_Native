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
    /// Interaction logic for SportPage.xaml
    /// </summary>
    public partial class SportPage : UserControl
    {
        private int Difficulty { get; set; } = 0;
        private TimeSpan workoutTime = new TimeSpan(0, 0, 0);
        private const int MaxDifficulty = 10; // Maximum difficulty level
        public SportPage()
        {
            InitializeComponent();
            UpdateDifficultyText();
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
            // Prompt the user to save the workout if it's under 3 minutes
            if (workoutTime.TotalMinutes < 3)
            {
                MessageBoxResult result = MessageBox.Show("The workout is under 3 minutes. Do you want to save it?", "Save Workout", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Save workout information here
                    SaveWorkout();
                }
                else
                {
                    // Handle other actions if needed
                }
            }
            else
            {
                // Save workout information since it's not under 3 minutes
                SaveWorkout();
            }
        }

        private void UpdateDifficultyText()
        {
            // Update and display the difficulty in the TextBlock
            DifficultyText.Text = Difficulty.ToString();
        }

        private void SaveWorkout()
        {
            // Implement the logic to save the workout information
            // This can include saving the difficulty, workout time, etc.
            // For demonstration purposes, we'll display a message for now.
            MessageBox.Show("Workout saved!", "Save Workout", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

}

