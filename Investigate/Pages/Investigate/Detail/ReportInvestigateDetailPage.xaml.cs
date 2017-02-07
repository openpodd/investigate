using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace Investigate
{
	public partial class ReportInvestigateDetailPage : TabbedPage
	{
		public ReportInvestigateDetailPage(ReportInvestigate reportInvestigate)
		{
			InitializeComponent();
			reportDetailPage.ReportInvestigate = reportInvestigate;
			incidentsPage.ReportInvestigate = reportInvestigate;
		}
	}
}
