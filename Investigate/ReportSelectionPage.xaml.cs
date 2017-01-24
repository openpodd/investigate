using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
			await Navigation.PopAsync(true);
		}
	}
}
