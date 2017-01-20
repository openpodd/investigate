using System;
using Xamarin.Forms;

namespace Investigate
{
	public partial class InvestigatePage : ContentPage
	{
		public InvestigatePage()
		{
			InitializeComponent();
		}

		async void OnLogoutButtonClicked(object sender, EventArgs e)
		{
			Settings.Token = "";

			Navigation.InsertPageBefore(new LoginPage(), this);
			await Navigation.PopAsync();
		}
	}
}
