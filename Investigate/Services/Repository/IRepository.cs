using System.Collections.Generic;
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
	}
}
