﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Investigate
{
	public partial class ReportSelectionPage : ContentPage
	{


	    private readonly ReportSelectionViewModel _viewModel;
		public ReportSelectionPage()
		{
			InitializeComponent();

			_viewModel = new ReportSelectionViewModel
			{
				DoneReportSelection = new Action<HashSet<SearchItem>>(ClosePage),
			    ShowAuthoritySelectPageAction = new Action(ShowAuthoritySelectPage),
				PoddService = new PoddService()
			};

		    BindingContext = _viewModel;
		}

		async public void ClosePage(HashSet<SearchItem> reports)
		{
			// Create investigate here.
			foreach (var item in reports)
			{
				var investigate = new ReportInvestigate
				{
					ReportId = item.Id,
					ReportDate = item.Date,
					ReportIncidentDate = new DateTimeOffset(item.IncidentDate),
					ReportTypeName = item.ReportTypeName,
					ReportStateName = item.ReportStateName,
					ReportAdministrationAreaName = item.AdministrationAreaName,
					ReportCreateByName = item.CreateByName,
					ReportCreateByContact = item.CreateByContact,
					ReportCreateByTelephone = item.CreateByTelephone,
					ReportRendererFormData = item.RendererFormData
				};
				await App.Repository.CreateAsync(investigate);
			}

		    await Navigation.PopAsync(true);
		}

	    public async void ShowAuthoritySelectPage()
	    {
			var viewModel = new AuthoritySelectViewModel()
			{
			    PoddService = new PoddService(),
			    BackCommand = new Command(() => Navigation.PopModalAsync())
			};
	        await Navigation.PushModalAsync(new AuthoritySelectPage(viewModel));
	        var item = await viewModel.TaskCompletionSource.Task;
            _viewModel.SetAuthorityFilter(item);
	        await Navigation.PopModalAsync();
	        Debug.WriteLine(item.Name);
	    }
	}


    public class ReportSelectionViewModel : BaseViewModel
    {
        public IPoddService PoddService { set; get; }

        public Action<HashSet<SearchItem>> DoneReportSelection { get; set; }
        public Action CloseAction { set; get; } = () => { };
        public Action ShowAuthoritySelectPageAction { set; get; } = () => { };

        public ObservableCollection<SearchItem> Reports { get; }
        public int NumberOfReports => Reports.Count;
        public HashSet<SearchItem> SelectedReports { get; }
        public int NumberOfSelectedReports => SelectedReports.Count;

        private bool canSearch = true;
        public ICommand SearchCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand SelectReportCommand { get; }
        public ICommand ReportSelectionDoneCommand { get; }

        public String AuthorityLabelColor { get; set; }
        public String AuthorityLabelText { get; set; }
        public ICommand AuthorityLabelTappedCommand { get; }
        private AuthorityItem _authorityItem;


        public ReportSelectionViewModel()
        {
            Reports = new ObservableCollection<SearchItem>();
            SelectedReports = new HashSet<SearchItem>();
            SearchCommand = new Command(async () => await Search(), () => canSearch);
            SelectReportCommand = new Command<SearchItem>(SelectReport);
            ReportSelectionDoneCommand = new Command(ReportSelectionDone);
            ResetCommand = new Command(ResetSearchCriteria);
            AuthorityLabelTappedCommand = new Command(PopupAuthoritySelectPage);

            ResetSearchCriteria();
        }

        private void ResetSearchCriteria()
        {
            AuthorityLabelColor = "#999";
            AuthorityLabelText = "Tap here to change Authority";
            _authorityItem = null;
            OnPropertyChanged("AuthorityLabelText");
            OnPropertyChanged("AuthorityLabelColor");
        }

        private async Task Search()
        {
            Debug.WriteLine("[ReportSelectionViewModel] Before set canSearch = false");
            CanSearch(false);

            Reports.Clear();

            Debug.WriteLine("[ReportSelectionViewModel] Before PoddService.Search");
            var results = await PoddService.Search(new SearchRequest()
            {
                AuthorityId = _authorityItem?.Id ?? -99
            });
            Debug.WriteLine("[ReportSelectionViewModel] After PoddService.Search");
            foreach (var item in results.Results)
            {
                Reports.Add(item);
            }

            CanSearch(true);
            Debug.WriteLine("[ReportSelectionViewModel] After set canSearch = true");
            OnPropertyChanged("Reports");
            OnPropertyChanged("NumberOfReports");
        }

        public void CanSearch(bool value)
        {
            canSearch = value;
            ((Command) SearchCommand).ChangeCanExecute();
        }

        private void SelectReport(SearchItem report)
        {
            if (SelectedReports.Contains(report)) return;

            SelectedReports.Add(report);
            OnPropertyChanged("NumberOfSelectedReports");
        }

        private void ReportSelectionDone()
        {
            DoneReportSelection?.Invoke(SelectedReports);
        }

        public void PopupAuthoritySelectPage()
        {
            ShowAuthoritySelectPageAction();
            Debug.WriteLine("AuthoritySelect called");
        }

        public void SetAuthorityFilter(AuthorityItem authorityItem)
        {
            _authorityItem = authorityItem;
            AuthorityLabelColor = "#333";
            AuthorityLabelText = authorityItem.Name;
            OnPropertyChanged("AuthorityLabelColor");
            OnPropertyChanged("AuthorityLabelText");
        }
    }
}
