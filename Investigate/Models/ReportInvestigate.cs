using System;
using System.Collections.Generic;
using System.Linq;

using Realms;

namespace Investigate
{
	public class ReportInvestigate : RealmObject
	{
		[PrimaryKey]
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

		public IList<Incident> Incidents { get; }

		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset UpdatedAt { get; set; }

		public ReportInvestigate() : base()
		{
			CreatedAt = System.DateTimeOffset.Now;
			UpdatedAt = System.DateTimeOffset.Now;
		}
	}

	public class Incident : RealmObject
	{
	    [PrimaryKey]
	    public String Uuid { get; set; }

		public String Village { get; set; }
		public String HouseNumber { get; set; }
		public String HouseOwnerName { get; set; }
		public String Telephone { get; set; }

		public Double Latitude { get; set; }
		public Double Longitude { get; set; }

		public IList<IncidentAnimalStat> IncidentAnimalStats { get; }

		[Ignored]
		public int SickTotal
		{
			get { return IncidentAnimalStats.Sum(s => s.SickAccumulatedCount); }
		}

		[Ignored]
		public int DeathTotal
		{
			get { return IncidentAnimalStats.Sum(s => s.DeathAccumulatedCount); }
		}

		[Ignored]
		public int SickDeathTotal
		{
			get { return SickTotal + DeathTotal; }
		}

		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset UpdatedAt { get; set; }

		public Incident() : base()
		{
		    if (string.IsNullOrEmpty(Uuid))
		    {
		        Uuid = Guid.NewGuid().ToString("N");
		    }
			CreatedAt = System.DateTimeOffset.Now;
			UpdatedAt = System.DateTimeOffset.Now;
		}
	}

	public class IncidentAnimalStat : RealmObject
	{
		public String AnimalType;
		public DateTimeOffset Date;
		public int SickCount;
		public int DeathCount;
		public int SickAccumulatedCount;
		public int DeathAccumulatedCount;

		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset UpdatedAt { get; set; }

		public IncidentAnimalStat() : base()
		{
			CreatedAt = System.DateTimeOffset.Now;
			UpdatedAt = System.DateTimeOffset.Now;
		}
	}
}
