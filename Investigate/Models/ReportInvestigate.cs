using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

	    private string _village;

	    public String Village
	    {
	        get { return _village;  }
	        set { _village = value; Debug.WriteLine($"Value Village is set to {value}"); }
	    }

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

	public class IncidentAnimalStat : BaseModel
	{
	    [PrimaryKey]
	    public String Uuid { get; set; }

	    [Indexed]
	    public string IncidentUuid { get; set; }

	    private string _animalType;

	    public String AnimalType
	    {
	        get { return _animalType; }
	        set { _animalType = value; Debug.WriteLine($"Value AnimalType is set to {value}"); }
	    }

	    public DateTimeOffset Date { get; set; }
	    public int SickCount { get; set; }
	    public int DeathCount { get; set; }
	    public int SickAccumulatedCount { get; set; }
	    public int DeathAccumulatedCount { get; set; }

	    public IncidentAnimalStat() : base()
	    {
	        if (string.IsNullOrEmpty(Uuid))
	        {
	            Uuid = Guid.NewGuid().ToString("N");
	        }
	    }
	}
}
