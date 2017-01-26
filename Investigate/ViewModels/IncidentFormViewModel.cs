using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Realms;
using Xamarin.Forms;

namespace Investigate
{
    public class IncidentFormViewModel : BaseViewModel
    {
        public ReportInvestigate ReportInvestigate { get; }
        public Incident Incident { get; set; }
        private bool _isNew;

		public ICommand SaveCommand { get; private set; }
		public Action SaveSuccessAction { get; set; }

        public IncidentFormViewModel(long reportInvestigateId)
        {
            _isNew = true;
            SaveCommand = new Command(Save);
            var realm = Realm.GetInstance();
            ReportInvestigate = realm.All<ReportInvestigate>().First(i => i.Id == reportInvestigateId);

            Incident = new Incident();
        }

		public IncidentFormViewModel(long reportInvestigateId, string uuid)
		{
            SaveCommand = new Command(Save);
            var realm = Realm.GetInstance();
            ReportInvestigate = realm.All<ReportInvestigate>().First(i => i.Id == reportInvestigateId);

            if (string.IsNullOrEmpty(uuid))
            {
                _isNew = true;
                Incident = new Incident();
            }
            else
            {
                _isNew = false;
                var incident = realm.All<Incident>().First(i => i.Uuid == uuid);
                Incident = new Incident()
                {
                    Uuid = incident.Uuid,
                    Village = incident.Village,
                    HouseNumber = incident.HouseNumber,
                    HouseOwnerName = incident.HouseOwnerName,
                    Telephone = incident.Telephone,
                    CreatedAt = incident.CreatedAt,
                    UpdatedAt = incident.UpdatedAt
                };
            }
            Debug.WriteLine($"Incident : {Incident.Village} {Incident.HouseNumber} {Incident.HouseOwnerName}");
        }

        void Save()
        {
            Debug.WriteLine("SaveCommand called");
            var realm = Realm.GetInstance();
            realm.Write(() =>
            {
                Incident.UpdatedAt = DateTimeOffset.Now;
                realm.Add(Incident, true);

                if (_isNew)
                {
                    ReportInvestigate.Incidents.Add(Incident);
                }
            });
            Debug.WriteLine($"Saved : incident UUID : {Incident.Uuid}");

            SaveSuccessAction?.Invoke();
        }
    }
}
