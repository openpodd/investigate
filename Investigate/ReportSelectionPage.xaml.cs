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
					realm.Add(new ReportInvestigate
					{
						ReportId = item.Id,
						ReportDate = item.Date,
						ReportAdministrationAreaName = item.AdministrationAreaName,
						ReportCreateByName = item.CreateByName,
						ReportRendererFormData = item.RendererFormData
					});
				}
			});

			await Navigation.PopAsync(true);
		}
	}
}
