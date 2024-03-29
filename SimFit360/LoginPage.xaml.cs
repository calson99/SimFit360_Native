﻿using SimFit360.Data;
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
	/// Interaction logic for LoginPage.xaml
	/// </summary>
	public partial class LoginPage : UserControl
	{
		public LoginPage()
		{
			InitializeComponent();
		}

		// Number buttons, adds the number to the pin.
		private void Number_Click(object sender, RoutedEventArgs e)
		{
			var button = sender as Button;
			if (button != null)
			{
				if (textBlockLogin.Text.Length < 4)
				{
					textBlockLogin.Text += button.Content;
				}
			}
		}

		// Backspace button, removes the last character from the pin.
		private void BackSpace_Click(object sender, RoutedEventArgs e)
		{
			if (textBlockLogin.Text.Length > 0)
			{
				textBlockLogin.Text = textBlockLogin.Text.Remove(textBlockLogin.Text.Length - 1);
			}
		}

		// Login button, checks if the barcode and the pin is correct and navigates to the main page.
		private void Login_Click(object sender, RoutedEventArgs e)
		{
			using var db = new AppDbContext();
			{
				// Check if the pin is 4 characters long.
				if (textBlockLogin.Text.Length == 4)
				{
					// Check if the barcode is a number.
					int barcode = int.Parse(textBoxTestBarcode.Text);
					
					// Check if the barcode exists in the database.
					var user = db.Users.FirstOrDefault(u => u.Barcode == barcode);
					if (user != null)
					{
						// Check if the pin is correct.
						bool isPinCorrect = SecureHasher.Verify(textBlockLogin.Text, user.Pincode);
						if (isPinCorrect)
						{
							// Navigate to the main page.
							MainWindow.Instance.NavigateToMainPage(user.Id);
						}
						else
						{
							// Show a message box if the pin is incorrect.
							MessageBox.Show("Invalid Pin");
						}
					}
				}
			}
			
		}
	}
}
