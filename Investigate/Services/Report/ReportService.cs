using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Investigate
{
	public partial class PoddService
	{
		public async Task<SearchResult> Search(SearchRequest searchRequest)
		{
			var httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.Add("Authorization", "Token " + Settings.Token);

			var uri = new Uri(ServerUrl + "/reports/search/");

			Debug.WriteLine("Fetching reports from server");
		    var values = new Dictionary<string, string>
		    {
		        {"page", searchRequest.StartPage.ToString()},
		        {"page_size", searchRequest.ItemPerPage.ToString()},
		        {"q", "negative:true AND typeName:\"สัตว์ป่วย/ตาย\""},
		        {"tz", "7"}
		    };

		    if (searchRequest.AuthorityId != -99)
		    {
                values.Add("authorities", searchRequest.AuthorityId.ToString());
		    }

		    var content = new FormUrlEncodedContent(values);
			var query = await content.ReadAsStringAsync();

			var response = await httpClient.GetAsync(uri + "?" + query);
			if (response.IsSuccessStatusCode)
			{
				var resultContent = await response.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<SearchResult>(resultContent);
				result.Success = true;
				return result;
			}
			else {
				var result = new SearchResult();
				result.Success = false;
				result.ErrorMessage = await response.Content.ReadAsStringAsync();
				return result;
			}
		}	
	}
}
