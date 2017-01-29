namespace Investigate
{
	public class ReportInvestigateReportDetailViewModel : BaseViewModel
	{
		public ReportInvestigate ReportInvestigate { get; set; }

		public ReportInvestigateReportDetailViewModel(ReportInvestigate reportInvestigate)
		{
			ReportInvestigate = reportInvestigate;
		}
	}
}
