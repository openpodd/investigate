using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

		protected override async void OnAppearing()
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


    public class ReportInvestigateListViewModel : BaseViewModel
    {
        public IEnumerable<ReportInvestigate> ReportInvestigates { get; private set; }
        public int ReportInvestigatesCount
        {
            get { return ReportInvestigates.Count(); }
        }
        public bool IsEmptyReportInvestigates
        {
            get { return !ReportInvestigates.Any(); }
        }
        public bool IsNotEmptyReportInvestigates
        {
            get { return ReportInvestigates.Any(); }
        }

        public Action<ReportInvestigate> SelectItemAction { get; set; }

        /**
         * to use async method in constructor
         * we must use this pattern to create instance
         */
        public static async Task<ReportInvestigateListViewModel> create()
        {
            var instance = new ReportInvestigateListViewModel();
            instance.ReportInvestigates = await App.Repository.AllReportInvestigates();
            return instance;
        }

        private ReportInvestigateListViewModel()
        {
        }

        public void RefreshReportInvestigateListVisibility()
        {
            OnPropertyChanged("IsEmptyReportInvestigates");
            OnPropertyChanged("IsNotEmptyReportInvestigates");
        }

        void OnSelectItem(object sender, SelectedItemChangedEventArgs e)
        {
            Debug.WriteLine("OnSelectItem called");
            if (SelectItemAction != null)
            {
                if (e.SelectedItem != null) // not run on deselect
                {
                    SelectItemAction((ReportInvestigate)e.SelectedItem);
                }
            }
        }
    }
}
