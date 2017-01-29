using System;
using System.IO;
using Investigate.iOS;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(Database))]
namespace Investigate.iOS
{
	public class Database : IDatabase
	{
		public SQLiteAsyncConnection DBConnect()
		{
			var filename = "investigate.db3";
			string folder =
				Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			string libraryFolder = Path.Combine(folder, "..", "Library");
			var path = Path.Combine(libraryFolder, filename);
			var connection = new SQLiteAsyncConnection(path);
			return connection;
		}
	}
}
