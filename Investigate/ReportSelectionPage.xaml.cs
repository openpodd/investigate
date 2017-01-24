using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace Investigate
{
	public partial class ReportSelectionPage : ContentPage
	{
		public ReportSelectionPage()
		{
			InitializeComponent();
			var context = new ReportSelectionViewModel();
			context.DoneReportSelection = new Action<HashSet<SearchItem>>(ClosePage);
			BindingContext = context;
		}

		async public void ClosePage(HashSet<SearchItem> reports)
		{
			await Navigation.PopAsync(true);
		}
	}
}
