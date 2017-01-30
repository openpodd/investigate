using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Investigate
{
	public class IncidentFormViewModel : BaseViewModel
	{
		public long ReportInvestigateId { get; set; }
		public Incident Incident { get; set; }

		public ICommand SaveCommand { get; private set; }
		public Action SaveSuccessAction { get; set; }

		public static async Task<IncidentFormViewModel> Create(long reportInvestigateId, string uuid)
		{
			var instance = new IncidentFormViewModel(reportInvestigateId, uuid);
			instance.ReportInvestigateId = reportInvestigateId;

			if (string.IsNullOrEmpty(uuid))
			{
				instance.Incident = new Incident();
				instance.Incident.ReportInvestigateId = reportInvestigateId;
			}
			else
			{
				instance.Incident = await App.Repository.GetIncidentByUUID(uuid);
			}
			Debug.WriteLine($"Incident : {instance.Incident.Village} {instance.Incident.HouseNumber} {instance.Incident.HouseOwnerName}");


			return instance;
		}

		public IncidentFormViewModel(long reportInvestigateId, string uuid)
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
