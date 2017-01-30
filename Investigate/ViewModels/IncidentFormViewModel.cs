using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Investigate
{
	public class IncidentFormViewModel : BaseViewModel
	{
		public long ReportInvestigateId { get; set; }
		public Incident Incident { get; set; }
	    public IEnumerable<IncidentAnimalStat> IncidentAnimalStatList { get; set; }

		public ICommand SaveCommand { get; private set; }
		public Action SaveSuccessAction { get; set; }

		public static async Task<IncidentFormViewModel> Create(long reportInvestigateId, string uuid)
		{
			var instance = new IncidentFormViewModel();
			instance.ReportInvestigateId = reportInvestigateId;

			if (string.IsNullOrEmpty(uuid))
			{
				instance.Incident = new Incident();
				instance.Incident.ReportInvestigateId = reportInvestigateId;
			    Debug.WriteLine($"New Incident UUID: {instance.Incident.Uuid} : {instance.Incident.Village} {instance.Incident.HouseNumber} {instance.Incident.HouseOwnerName}");
			}
			else
			{
				instance.Incident = await App.Repository.GetIncidentByUUID(uuid);
			    instance.IncidentAnimalStatList = await App.Repository.GetIncidentAnimalStatListByIncidentUuid(uuid);
			    Debug.WriteLine($"Existing Incident UUID: {instance.Incident.Uuid} : {instance.Incident.Village} {instance.Incident.HouseNumber} {instance.Incident.HouseOwnerName}");
			    Debug.WriteLine($"--- Got IncidenAnimalStat {instance.IncidentAnimalStatList.Count()} rows");
			}

			return instance;
		}

		public IncidentFormViewModel()
		{
			SaveCommand = new Command(Save);
		}

		void Save()
		{
			Debug.WriteLine("SaveCommand called");

			Incident.UpdatedAt = DateTimeOffset.Now;
			App.Repository.InsertOrUpdateAsync(Incident);

		    Debug.WriteLine($"Updated Incident : {Incident.Village} {Incident.HouseNumber} {Incident.HouseOwnerName}");
		    Debug.WriteLine($"Saved : incident UUID : {Incident.Uuid}");

			SaveSuccessAction?.Invoke();
		}
	}
}
