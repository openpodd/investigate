using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Realms;
using Xamarin.Forms;

namespace Investigate
{
	public partial class ReportSelectionPage : ContentPage
	{
		public ReportSelectionPage()
		{
			InitializeComponent();

			BindingContext = new ReportSelectionViewModel
			{
				DoneReportSelection = new Action<HashSet<SearchItem>>(ClosePage),
				PoddService = new PoddService()
			};
		}

		async public void ClosePage(HashSet<SearchItem> reports)
		{
			// Create investigate here.
			var realm = Realm.GetInstance();
			realm.Write(() =>
			{
				foreach (var item in reports)
				{
					var report = new Report
					{
						Id = item.Id,
						Date = item.Date,
						AdministrationAreaName = item.AdministrationAreaName,
						CreateByName = item.CreateByName,
						RendererFormData = item.RendererFormData
					};

					realm.Add(report, true);
					realm.Add(new ReportInvestigate { Report = report });
				}
			});

			await Navigation.PopAsync(true);
		}
	}
}
