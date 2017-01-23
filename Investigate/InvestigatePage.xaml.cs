using System;
using Xamarin.Forms;

namespace Investigate
{
	public partial class InvestigatePage : ContentPage
	{

		PoddService service = null;

		public InvestigatePage()
		{
			InitializeComponent();
			System.Diagnostics.Debug.WriteLine("reports");

			service = new PoddService();
		}

		async void OnLogoutButtonClicked(object sender, EventArgs e)
		{
			Settings.Token = "";

			Navigation.InsertPageBefore(new LoginPage(), this);
			await Navigation.PopAsync();
		}

		async void OnFetchReportClicked(object sender, EventArgs e)
		{
			label.Text = "fetch reports";
			var results = await service.Search(new SearchRequest());
			System.Diagnostics.Debug.WriteLine(results);
			label.Text = results.ErrorMessage;
		}
	}
}
