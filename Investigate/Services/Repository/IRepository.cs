﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Investigate
{
	public interface IRepository
	{
		Task initTableAsync();

		Task createAsync(ReportInvestigate ri);

		Task createAsync(Incident incident);

		Task InsertOrUpdateAsync(Incident incident);

		Task<IEnumerable<ReportInvestigate>> AllReportInvestigates();

		Task<IEnumerable<Incident>> FindIncidentsByReportInvestigateId(long investigateId);

		Task<Incident> GetIncidentByUUID(string uuid);

	    Task<IEnumerable<IncidentResult>> GetIncidentListByReportInvestigateId(long id);
	}

    public class IncidentResult
    {
        public string Uuid { get; set; }
        public string Village { get; set; }
        public string HouseNumber { get; set; }
        public string HouseOwnerName { get; set; }
        public int SickTotal { get; set; }
        public int DeathTotal { get; set; }
        public int SickDeathTotal { get; set; }
    }
}
