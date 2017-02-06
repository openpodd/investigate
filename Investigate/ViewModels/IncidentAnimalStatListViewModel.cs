using System.Collections.Generic;
using System.Threading.Tasks;

namespace Investigate
{
    public class IncidentAnimalStatListViewModel
    {
        public Incident Incident { get; set; }
        public IEnumerable<IncidentAnimalStat> IncidentAnimalStatList { get; set; }

        public static async Task<IncidentAnimalStatListViewModel> Create(string uuid)
        {
            var instance =
                new IncidentAnimalStatListViewModel
                {
                    Incident = await App.Repository.GetIncidentByUUID(uuid),
                    IncidentAnimalStatList = await App.Repository.GetIncidentAnimalStatListByIncidentUuid(uuid)
                };
            return instance;
        }


    }
}