using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SQLite;

namespace Investigate
{
	public class ReportInvestigate : BaseModel
	{
		[PrimaryKey, AutoIncrement]
		public long Id { get; set; }

		public int ReportId { get; set; }

		public string ReportTypeName { get; set; }
		public string ReportStateName { get; set; }

		public DateTimeOffset ReportDate { get; set; }
		public DateTimeOffset ReportIncidentDate { get; set; }

		public string ReportAdministrationAreaName { get; set; }
		public string ReportCreateByName { get; set; }
		public string ReportCreateByContact { get; set; }
		public string ReportCreateByTelephone { get; set; }
		public string ReportRendererFormData { get; set; }

	    public string ReportRendererFromDataNoTag => Regex.Replace(ReportRendererFormData, "<.*?>", string.Empty);
	    public string ReportRendererFromDataShortFormat => ReportRendererFromDataNoTag.Substring(0, 90) + "...";
	}

	public class Incident : BaseModel
	{
	    [PrimaryKey]
	    public string Uuid { get; set; }

		[Indexed]
		public long ReportInvestigateId { get; set; }

	    private string _village;

	    public string Village
	    {
	        get { return _village;  }
	        set { _village = value; Debug.WriteLine($"Value Village is set to {value}"); }
	    }

	    public string HouseNumber { get; set; }
		public string HouseOwnerName { get; set; }
		public string Telephone { get; set; }

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
	    public string Uuid { get; set; }

	    [Indexed]
	    public string IncidentUuid { get; set; }

	    private string _animalType;

	    public string AnimalType
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
