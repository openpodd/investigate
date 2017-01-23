using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Investigate
{
	public partial class LoginPage : ContentPage
	{

		PoddService service = null;

		public LoginPage()
		{
			InitializeComponent();
			userNameEntry.Next = passwordEntry;
			passwordEntry.Completed += OnLoginButtonClicked;
			service = new PoddService();
		}

		async void OnLoginButtonClicked(object sender, EventArgs e)
		{
			var userName = userNameEntry.Text;
			var password = passwordEntry.Text;

			var result = await service.Login(new LoginRequest(userName, password));
			if (result.Success)
			{
				messageLabel.Text = "Token = " + result.Message;
				Navigation.InsertPageBefore(new InvestigatePage(), this);
				await Navigation.PopAsync();
			}
			else {
				messageLabel.Text = "Login Failed with " + result.Message;
				passwordEntry.Text = String.Empty;
			}

		}


	}
}
