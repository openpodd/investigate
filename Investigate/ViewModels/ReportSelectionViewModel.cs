using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Investigate
{
	public class ReportSelectionViewModel : INotifyPropertyChanged
	{

		public Action<HashSet<SearchItem>> DoneReportSelection { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		public Action CloseAction { set; get; } = () => { };
	    public Action ShowAuthoritySelectPageAction { set; get; } = () => { };

	    public IPoddService PoddService { set; get; }

		public ObservableCollection<SearchItem> Reports { get; }
		public int NumberOfReports
		{ 
			get
			{
				return Reports.Count;
			}
		}


		public HashSet<SearchItem> SelectedReports { get; }
		public int NumberOfSelectedReports
		{
			get
			{
				return SelectedReports.Count;
			}
		}

		bool canSearch = true;
		public ICommand SearchCommand { get; }
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

		    AuthorityLabelColor = "#999";
		    AuthorityLabelText = "Tap here to change Authority";
		    AuthorityLabelTappedCommand = new Command(PopupAuthoritySelectPage);
		}

		async Task Search()
		{
			CanSearch(false);

			Reports.Clear();

			var results = await PoddService.Search(new SearchRequest()
			{
			    AuthorityId = _authorityItem?.Id ?? -99
			});
			foreach (SearchItem item in results.Results)
			{
				Reports.Add(item);
			}

			CanSearch(true);
			OnPropertyChanged("Reports");
			OnPropertyChanged("NumberOfReports");
		}

		void CanSearch(bool value)
		{
			canSearch = value;
			((Command)SearchCommand).ChangeCanExecute();
		}

		void SelectReport(SearchItem report)
		{
			if (!SelectedReports.Contains(report))
			{
				SelectedReports.Add(report);
				OnPropertyChanged("NumberOfSelectedReports");
			}
		}

		void ReportSelectionDone()
		{
			if (DoneReportSelection != null)
			{
				DoneReportSelection(SelectedReports);
			}
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			var changed = PropertyChanged;
			if (changed != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
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
