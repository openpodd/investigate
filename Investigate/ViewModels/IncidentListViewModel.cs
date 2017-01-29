using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Investigate
{
	public class IncidentListViewModel : BaseViewModel
	{
		ReportInvestigate ReportInvestigateInstance { get; set; }
		public IEnumerable<Incident> Incidents { get; set; }


		/**
		 * to use async method in constructor 
		 * we must use this pattern to create instance
		 */
		public static async Task<IncidentListViewModel> create(long reportInvestigateId)
		{
			var instance = new IncidentListViewModel();
			instance.Incidents = await App.Repository.FindIncidentsByReportInvestigateId(reportInvestigateId);
			Debug.WriteLine(string.Format("Done loading from db total {0} rows", instance.Incidents.Count()));
			return instance;
		}

		private IncidentListViewModel()
		{
		}
	}
}
