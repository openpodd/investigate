using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Investigate
{
	public partial class InvestigatePage : ContentPage
	{
		
		public static async Task<InvestigatePage> Create()
		{
			var instance = new InvestigatePage();
			var viewModel = await ReportInvestigateListViewModel.create();
			viewModel.Navigation = instance.Navigation;
			instance.BindingContext = viewModel;
			return instance;
		}

		public InvestigatePage()
		{
			InitializeComponent();
			listView.ItemTapped += SelectItem;
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();
			var viewModel = await ReportInvestigateListViewModel.create();
			viewModel.Navigation = Navigation;
			BindingContext = viewModel;
		}

		async void OnLogoutButtonClicked(object sender, EventArgs e)
		{
			Settings.Clear();
			Navigation.InsertPageBefore(new LoginPage(), this);
			await Navigation.PopAsync();
		}

		async void OnFetchReportClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ReportSelectionPage(), true);
		}

		async void SelectItem(object sender, ItemTappedEventArgs e)
		{
			Debug.WriteLine("SelectItem called");
			var page = new ReportInvestigateDetailPage((ReportInvestigate)e.Item);
			await Navigation.PushAsync(page, true);
		}
	}
}
