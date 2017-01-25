using System;
using System.Linq;
using Realms;

namespace Investigate
{
	public class ReportInvestigateReportDetailViewModel : BaseViewModel
	{
		public ReportInvestigate ReportInvestigateInstance { get; set; }

		public ReportInvestigateReportDetailViewModel(long reportInvestigateId)
		{
			var realm = Realm.GetInstance();
			ReportInvestigateInstance = realm.Find<ReportInvestigate>(reportInvestigateId);
		}
	}
}
