using System;
using SQLite;

namespace Investigate
{
	public class Report
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		public DateTimeOffset Date { get; set; }

		public String AdministrationAreaName { get; set; }

		public String CreateByName { get; set; }

		public String RendererFormData { get; set; }
	}
}
