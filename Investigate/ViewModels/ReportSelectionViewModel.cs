using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Investigate
{
	public class ReportSelectionViewModel : INotifyPropertyChanged
	{
		public Action<HashSet<SearchItem>> DoneReportSelection { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;
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

		public EventHandler OnSelectedReportCompleted;

		public ReportSelectionViewModel()
		{
			Reports = new ObservableCollection<SearchItem>();
			SelectedReports = new HashSet<SearchItem>();
			SearchCommand = new Command(async () => await Search(), () => canSearch);
			SelectReportCommand = new Command<SearchItem>(SelectReport);
			ReportSelectionDoneCommand = new Command(ReportSelectionDone);
		}

		async Task Search()
		{
			CanSearch(false);

			Reports.Clear();

			var service = new PoddService();
			var results = await service.Search(new SearchRequest());
			foreach (SearchItem item in results.results)
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
			SelectedReports.Add(report);
			OnPropertyChanged("NumberOfSelectedReports");
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
	}
}
