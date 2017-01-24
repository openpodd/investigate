using System;
using Realms;

namespace Investigate
{
	public class ReportInvestigate : RealmObject
	{
		public int ReportId { get; set; }

		public DateTimeOffset ReportDate { get; set; }
		public String ReportAdministrationAreaName { get; set; }
		public String ReportCreateByName { get; set; }
		public String ReportRendererFormData { get; set; }

		// Incident location
		public String address { get; set; }
		public String houseOwnerName { get; set; }
		public String telephone { get; set; }
	}
}
