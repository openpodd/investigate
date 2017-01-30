﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace Investigate
{
	public class RepositoryImpl : IRepository
	{
		SQLiteAsyncConnection database;

		public RepositoryImpl()
		{
			database = DependencyService.Get<IDatabase>().DBConnect();
		}

		public async Task initTableAsync()
		{
			await database.CreateTableAsync<ReportInvestigate>();
			await database.CreateTableAsync<Incident>();
		    await database.CreateTableAsync<IncidentAnimalStat>();
		}

		public async Task createAsync(ReportInvestigate ri)
		{
			await database.InsertAsync(ri);
		}

		public async Task createAsync(Incident incident)
		{
			await database.InsertAsync(incident);
		}

		public async Task InsertOrUpdateAsync(Incident incident)
		{
			await database.InsertOrReplaceAsync(incident);
		}

		public async Task<IEnumerable<ReportInvestigate>> AllReportInvestigates()
		{
			return await database.QueryAsync<ReportInvestigate>("select * from ReportInvestigate order by CreatedAt");
		}

		public async Task<IEnumerable<Incident>> FindIncidentsByReportInvestigateId(long investigateId)
		{
			return await database.QueryAsync<Incident>("select * from Incident where ReportInvestigateId = ?", investigateId);
		}

		public async Task<Incident> GetIncidentByUUID(string uuid)
		{
			return await database.GetAsync<Incident>(uuid);
		}

	    public async Task<IEnumerable<IncidentResult>> GetIncidentListByReportInvestigateId(long id)
	    {
//	        const string query = " select * from Incident where ReportInvestigateId = ? ";
	        const string query = " select" +
	                             "   Uuid, Village, HouseNumber, HouseOwnerName," +
	                             "   sum('SickCount') as 'SickTotal'," +
	                             "   sum('DeathCount') as 'DeathTotal'," +
	                             "   'SickTotal' + 'DeathTotal' as 'SickDeathTotal'" +
	                             " from Incident i" +
	                             " left outer join IncidentAnimalStat s" +
	                             " where i.ReportInvestigateId = ?" +
	                             " group by i.Uuid" +
	                             " order by CreatedAt desc";

	        return await database.QueryAsync<IncidentResult>(query, id);
	    }
	}
}
