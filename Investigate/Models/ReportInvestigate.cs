using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;

namespace Investigate
{
	public class ReportInvestigate : BaseModel
	{
		[PrimaryKey, AutoIncrement]
		public long Id { get; set; }

		public int ReportId { get; set; }

		public String ReportTypeName { get; set; }
		public String ReportStateName { get; set; }

		public DateTimeOffset ReportDate { get; set; }
		public DateTimeOffset ReportIncidentDate { get; set; }

		public String ReportAdministrationAreaName { get; set; }
		public String ReportCreateByName { get; set; }
		public String ReportCreateByContact { get; set; }
		public String ReportCreateByTelephone { get; set; }
		public String ReportRendererFormData { get; set; }
	}

	public class Incident : BaseModel
	{
	    [PrimaryKey]
	    public String Uuid { get; set; }

		[Indexed]
		public long ReportInvestigateId { get; set; }

		public String Village { get; set; }
		public String HouseNumber { get; set; }
		public String HouseOwnerName { get; set; }
		public String Telephone { get; set; }

		public Double Latitude { get; set; }
		public Double Longitude { get; set; }

		public Incident() : base()
		{
		    if (string.IsNullOrEmpty(Uuid))
		    {
		        Uuid = Guid.NewGuid().ToString("N");
		    }
		}
	}

	public class IncidentAnimalStat
	{
	    [Indexed]
	    public string IncidentUuid { get; set; }

	    public String AnimalType;
		public DateTimeOffset Date;
		public int SickCount;
		public int DeathCount;
		public int SickAccumulatedCount;
		public int DeathAccumulatedCount;
	}
}
