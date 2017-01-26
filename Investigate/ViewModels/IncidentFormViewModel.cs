using System;
using System.Diagnostics;
using System.Linq;
using Realms;

namespace Investigate
{
    public class IncidentFormViewModel : BaseViewModel
    {
        public Incident Incident { get; set; }

        public IncidentFormViewModel()
        {
        }

        public IncidentFormViewModel(Incident incident)
        {
            Incident = incident;
            Debug.WriteLine($"Incident : {Incident.Village} {Incident.HouseNumber} {Incident.HouseOwnerName}");
        }
    }
}
