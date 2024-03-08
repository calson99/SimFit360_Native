using SimFit360.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
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
        private static System.Timers.Timer aTimer;
        private static Random random = new Random();
        private static System.Timers.Timer Timer;
        public int UserId { get; set; }
        public SportPage(int userId)
        {
            InitializeComponent();
            UpdateDifficultyText();
            OpenRandomVideoButton_Click();
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
        public async void GetVideoDetails(string Id)
        {
            // get api url from video
            string url = "https://www.googleapis.com/youtube/v3/videos?id=" + Id + "&part=contentDetails&key=AIzaSyAUUfUMYO9BeU1vxpg1M3yYnSCHMq3dpe0";

            var client = new HttpClient();
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            // take video duration from the api
            VideoListResponse videoList = JsonSerializer.Deserialize<VideoListResponse>(content, options);
            var video = videoList.Items[0];
            string duration = video.ContentDetails.Duration;
            VideoTimer(duration);
        }

        public async void VideoTimer(string duration)
        {
            // start a timer matching video duration
            TimeSpan timeSpan = XmlConvert.ToTimeSpan(duration);
            double doubleTimeSpan = timeSpan.TotalMilliseconds;

            aTimer = new System.Timers.Timer(doubleTimeSpan);

            // start a new video when the timer finishes

            aTimer.Elapsed += OnTimedEvent;
            aTimer.Enabled = true;
        }

        public static string RandomString(int length)
        {
            // make a 3 symbol long random string

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private async void LoadYoutubeVideo(string url)
        {
            // add an iframe to the webbrowser to show the youtube video
            string html = "<html><head>";
            html += "<meta content='IE=Edge' http-equiv='X-UA-Compatible'/>";

            html += "<iframe id='video' src= '" + url + "'height=\"100%\" style=\"position:absolute; top:0; left: 0\" width=\"100%\" frameborder='0' allow = \"autoplay; encrypted-media\"  fullscreen allowfullscreen></iframe>";

            html += "</body></html>";

            Dispatcher.Invoke(() =>
            {
                // Navigate the WebBrowser control to the HTML content
                webBrowser.NavigateToString(html);
            });
        }

        private async void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            OpenRandomVideoButton_Click();
            aTimer.Enabled = false;
        }

        private async void OpenRandomVideoButton_Click()
        {
            try
            {
                // get a random youtube video id

                string videoUrl = await GetRandomVideoUrl();

                // put the id in an embed url and a watch url

                if (!string.IsNullOrEmpty(videoUrl))
                {
                    var videoUrlEmbed = $"https://www.youtube.com/embed/{videoUrl}" + "?autoplay=1";
                    var videoUrlWatch = $"https://www.youtube.com/watch?v={videoUrl}";
                    GetVideoDetails(videoUrl);
                    LoadYoutubeVideo(videoUrlEmbed);
                }
                else
                {
                    MessageBox.Show("Failed to get a valid video URL from YouTube API.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                CaloriesText.Text = $"Calories: {caloriesBurned:F0}"; // Display calories with two decimal places
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
            double adjustedCaloriesPerSecond = BaselineCaloriesPerSecond + (totalDifficulty * 0.004);

            // Calculate total calories burned
            double totalCalories = adjustedCaloriesPerSecond * workoutTime.TotalSeconds;

            return totalCalories;
        }

        private static async Task<string> GetRandomVideoUrl()
        {
            // get a random video id using a random string in the youtube api

            var q = RandomString(3);
            string apiUrl = "https://www.googleapis.com/youtube/v3/search?part=snippet&type=video&maxResults=1&q=" + q;
            string apiKey = "AIzaSyAUUfUMYO9BeU1vxpg1M3yYnSCHMq3dpe0"; 

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl + "&key=" + apiKey);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                    string videoId = data.items[0].id.videoId;
                    return videoId;
                }
                else
                {
                    throw new Exception("Failed to fetch a random video ID from YouTube API.");
                }
            }
        }
    }
}


