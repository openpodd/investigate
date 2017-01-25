using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Investigate
{
	public partial class ReportDetailPage : ContentPage
	{
		public long ReportInvestigateId { get; set; }

		public ReportDetailPage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			BindingContext = new ReportInvestigateReportDetailViewModel(ReportInvestigateId);
			base.OnAppearing();
		}
	}
}
