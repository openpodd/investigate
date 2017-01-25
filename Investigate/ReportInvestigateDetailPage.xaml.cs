using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Investigate
{
	public partial class ReportInvestigateDetailPage : TabbedPage
	{
		public ReportInvestigateDetailPage(long reportInvestigateId)
		{
			InitializeComponent();
			reportDetailPage.ReportInvestigateId = reportInvestigateId;
			incidentsPage.ReportInvestigateId = reportInvestigateId;
		}
	}
}
