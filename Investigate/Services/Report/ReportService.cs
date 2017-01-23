using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;


namespace Investigate
{
	public partial class PoddService
	{
		public async Task<SearchResult> Search(SearchRequest searchRequest)
		{
			var httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.Add("Authorization", "Token " + Settings.Token);
			//httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", Settings.Token);


			var uri = new Uri(ServerUrl + "/reports/search/");

			var values = new Dictionary<string, string>();
			values.Add("q", "negative:true");
			values.Add("tz", "7");

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
