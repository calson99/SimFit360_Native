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
					if (button.Name == "Button1")
					{
						textBlockLogin.Text += "1";
					}
					else if (button.Name == "Button2")
					{
						textBlockLogin.Text += "2";
					}
					else if (button.Name == "Button3")
					{
						textBlockLogin.Text += "3";
					}
					else if (button.Name == "Button4")
					{
						textBlockLogin.Text += "4";
					}
					else if (button.Name == "Button5")
					{
						textBlockLogin.Text += "5";
					}
					else if (button.Name == "Button6")
					{
						textBlockLogin.Text += "6";
					}
					else if (button.Name == "Button7")
					{
						textBlockLogin.Text += "7";
					}
					else if (button.Name == "Button8")
					{
						textBlockLogin.Text += "8";
					}
					else if (button.Name == "Button9")
					{
						textBlockLogin.Text += "9";
					}
					else if (button.Name == "Button0")
					{
						textBlockLogin.Text += "0";
					}
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
				if (textBlockLogin.Text.Length == 4)
				{
					int barcode = int.Parse(textBoxTestBarcode.Text);
					
					var user = db.Users.FirstOrDefault(u => u.Barcode == barcode);
					if (user != null)
					{
						bool isPinCorrect = SecureHasher.Verify(textBlockLogin.Text, user.Pincode);
						if (isPinCorrect)
						{
							MainWindow.Instance.NavigateToMainPage(user.Id);
						}
						else
						{
							MessageBox.Show("Invalid Pin");
						}
					}
				}
			}
			
		}
	}
}
