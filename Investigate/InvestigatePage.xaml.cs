using System;
using Realms;
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
			var realm = Realm.GetInstance();
			realm.Write(() => realm.RemoveAll());

			Navigation.InsertPageBefore(new LoginPage(), this);
			await Navigation.PopAsync();
		}

		async void OnFetchReportClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ReportSelectionPage(), true);
		}

		protected override void OnAppearing()
		{
			BindingContext = new ReportInvestigateListViewModel();
			base.OnAppearing();
			//((ReportInvestigateListViewModel)BindingContext).RefreshReportInvestigateListVisibility();
		}
	}
}
