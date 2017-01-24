using System;
using Realms;

namespace Investigate
{
	public class ReportInvestigate : RealmObject
	{
		public Report Report { get; set; }

		// Incident location

		public String address { get; set; }
		public String houseOwnerName { get; set; }
		public String telephone { get; set; }
	}
}
