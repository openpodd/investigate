using System.Linq;
using System.Collections.Generic;
using Realms;
using System.Diagnostics;
using Xamarin.Forms;
using System.Windows.Input;

namespace Investigate
{
	public class IncidentListViewModel : BaseViewModel
	{
		ReportInvestigate ReportInvestigateInstance { get; set; }
		public IList<Incident> Incidents { get; }

		public IncidentListViewModel(long reportInvestigateId)
		{
			var realm = Realm.GetInstance();
			ReportInvestigateInstance = realm.Find<ReportInvestigate>(reportInvestigateId);
			Incidents = ReportInvestigateInstance.Incidents;
			Debug.WriteLine(string.Format("Done loading from realm total {0} rows", Incidents.Count()));
		}
	}
}
