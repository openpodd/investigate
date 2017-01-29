using System;
using SQLite;

namespace Investigate
{
	public interface IDatabase
	{
		SQLiteAsyncConnection DBConnect();
	}
}
