using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

using Realms;

namespace Investigate
{
	public class ReportInvestigateListViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public List<ReportInvestigate> ReportInvestigates { get; private set; }

		public ReportInvestigateListViewModel()
		{
			new Task(() =>
			{
				var realm = Realm.GetInstance();	

			}).Start();
		}
	}
}
