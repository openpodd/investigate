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

		public ICommand AddReportInvestigateCommand { get; }

		public IncidentListViewModel(long reportInvestigateId)
		{
			AddReportInvestigateCommand = new Command(AddMockData);
				
			var realm = Realm.GetInstance();
			ReportInvestigateInstance = realm.Find<ReportInvestigate>(reportInvestigateId);
			Incidents = ReportInvestigateInstance.Incidents;
			Debug.WriteLine(string.Format("Done loading from realm total {0} rows", Incidents.Count()));
		}

		void AddMockData()
		{
			Debug.WriteLine("AddMockData called");
			var realm = Realm.GetInstance();
			realm.Write(() =>
			{
				var incident = new Incident
				{
					Village = "9",
					HouseNumber = "299/29",
					HouseOwnerName = "Siriwat Umangamsup",
					Telephone = "+66841291342",
					Latitude = 0,
					Longitude = 0
				};

				incident.IncidentAnimalStats.Add(new IncidentAnimalStat()
				{
					AnimalType = "โคเนื้อ",
					Date = System.DateTimeOffset.Now,
					SickCount = 2,
					DeathCount = 1,
					SickAccumulatedCount = 3,
					DeathAccumulatedCount = 2
				});

				incident.IncidentAnimalStats.Add(new IncidentAnimalStat()
				{
					AnimalType = "โคนม",
					Date = System.DateTimeOffset.Now,
					SickCount = 4,
					DeathCount = 2,
					SickAccumulatedCount = 6,
					DeathAccumulatedCount = 4
				});

				ReportInvestigateInstance.Incidents.Add(incident);
			});
		}
	}
}
