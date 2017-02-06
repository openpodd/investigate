using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Investigate
{
	public class IncidentAnimalStatFormViewModel : BaseViewModel
    {
        public string IncidentUuid { get; private set; }
        public IncidentAnimalStat IncidentAnimalStat { get; set; }
        public ICommand SaveCommand { get; private set; }
        public Action SaveSuccessAction { get; set; }

        public IncidentAnimalStatFormViewModel()
        {
            SaveCommand = new Command(Save);
        }

        public static async Task<IncidentAnimalStatFormViewModel> Create(string incidentUuid, string incidentAnimalStatUuid)
        {
            Investigate.IncidentAnimalStat incidentAnimalStat;
            if (!string.IsNullOrEmpty(incidentAnimalStatUuid))
            {
                incidentAnimalStat = await App.Repository.GetIncidentAnimalStatByUuid(incidentAnimalStatUuid);
                Debug.WriteLine($"Loaded IncidentAnimalStat with UUID: {incidentAnimalStat.Uuid}, {incidentAnimalStat.AnimalType}");
            }
            else
            {
                incidentAnimalStat = new IncidentAnimalStat();
                incidentAnimalStat.IncidentUuid = incidentUuid;
                Debug.WriteLine($"New IncidentAnimalStat with UUID: {incidentAnimalStat.Uuid}");
            }

            var viewModel = new IncidentAnimalStatFormViewModel()
            {
                IncidentUuid = incidentUuid,
                IncidentAnimalStat = incidentAnimalStat
            };

            return viewModel;
        }

        public async void Save()
        {
            Debug.WriteLine("SaveCommand called");
            Debug.WriteLine($"Updated: UUID: {IncidentAnimalStat.Uuid}, {IncidentAnimalStat.AnimalType}");

            IncidentAnimalStat.UpdatedAt = DateTimeOffset.Now;
            await App.Repository.InsertOrUpdateAsync(IncidentAnimalStat);

            Debug.WriteLine($"Saved : incidentAnimalStat UUID : {IncidentAnimalStat.Uuid} as child of incident UUID: {IncidentAnimalStat.IncidentUuid}");

            SaveSuccessAction?.Invoke();
        }
    }
}