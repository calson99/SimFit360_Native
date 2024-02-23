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
            // Handle STOP button click if needed
        }

        private void UpdateDifficultyText()
        {
            // Update and display the difficulty in the TextBlock
            DifficultyText.Text = Difficulty.ToString();
        }
    }

}

