using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Realms;

namespace Investigate
{
	public class ReportInvestigateListViewModel : BaseViewModel
	{
		public IEnumerable<ReportInvestigate> ReportInvestigates { get; private set; }
		public int ReportInvestigatesCount
		{
			get { return ReportInvestigates.Count(); }
		}

		public ReportInvestigateListViewModel()
		{
			var realm = Realm.GetInstance();
			ReportInvestigates = realm.All<ReportInvestigate>();
		}
	}
}
