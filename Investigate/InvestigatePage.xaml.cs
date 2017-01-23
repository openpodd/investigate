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

		async void OnFetchReportClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ReportSelectionPage(), true);
		}
	}
}
