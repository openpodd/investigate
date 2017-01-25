using System;
using System.Diagnostics;
using Realms;
using Xamarin.Forms;

namespace Investigate
{
	public partial class InvestigatePage : ContentPage
	{
		
		public InvestigatePage()
		{
			InitializeComponent();
			listView.ItemTapped += SelectItem;
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
			BindingContext = new ReportInvestigateListViewModel()
			{
				Navigation = Navigation
			};
			base.OnAppearing();
		}

		async void SelectItem(object sender, ItemTappedEventArgs e)
		{
			Debug.WriteLine("SelectItem called");
			var page = new ReportInvestigateDetailPage((ReportInvestigate)e.Item);
			await Navigation.PushAsync(page, true);
		}
	}
}
