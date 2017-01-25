using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Realms;
using Xamarin.Forms;

namespace Investigate
{
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

		public ReportInvestigateListViewModel()
		{
			var realm = Realm.GetInstance();
			ReportInvestigates = realm.All<ReportInvestigate>().OrderByDescending(r => r.CreatedAt);
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
