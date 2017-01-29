using System.IO;
using Investigate.Droid;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(Database))]
namespace Investigate.Droid
{
	public class Database : IDatabase
	{
		public SQLiteAsyncConnection DBConnect()
		{
			var filename = "investigate.db3";
			string folder =
				System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			var path = Path.Combine(folder, filename);
			var connection = new SQLiteAsyncConnection(path);
			return connection;
		}
	}
}
