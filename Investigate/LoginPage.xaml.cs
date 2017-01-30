using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Investigate
{
	public partial class LoginPage : ContentPage
	{
	    readonly PoddService service = null;

		public LoginPage()
		{
			InitializeComponent();
			userNameEntry.Next = passwordEntry;
			passwordEntry.Completed += OnLoginButtonClicked;
			service = new PoddService();
		}

		async void OnLoginButtonClicked(object sender, EventArgs e)
		{
			if (Validate())
			{

				var userName = userNameEntry.Text;
				var password = passwordEntry.Text;

				var result = await service.Login(new LoginRequest(userName, password));
				if (result.Success)
				{
					//messageLabel.Text = "Token = " + result.Message;

				    await FetchAuthorities();
					Page nextPage = await InvestigatePage.Create();
					Navigation.InsertPageBefore(nextPage, this);
					await Navigation.PopAsync();
				}
				else {
					passwordEntry.Text = String.Empty;
					passwordLengthValidator.ResetValidation();

					await DisplayAlert("Fail", "Cannot login with provided credentials, please check and try again.", "OK");
				}
			}
		}

		public bool Validate()
		{
			usernameLengthValidator.ValidateLength(userNameEntry);
			passwordLengthValidator.ValidateLength(passwordEntry);

			if (!usernameLengthValidator.IsValid || !passwordLengthValidator.IsValid)
			{
				DisplayAlert("Alert", "Please check form value", "OK");
				return false;
			}
			else
			{
				return true;
			}
		}

	    private async Task FetchAuthorities()
	    {
	        await service.GetAuthorities();
	    }

	}
}
