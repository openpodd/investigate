using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Investigate
{
	public partial class ReportDetailPage : ContentPage
	{
		public ReportInvestigate ReportInvestigate { get; set; }

		public ReportDetailPage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			BindingContext = new ReportInvestigateReportDetailViewModel(ReportInvestigate);
			base.OnAppearing();
		}
	}
}
