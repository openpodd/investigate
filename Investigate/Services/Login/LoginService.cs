using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;


namespace Investigate
{
	public partial class PoddService
	{
		public async Task<LoginResult> Login(LoginRequest request)
		{
			var httpClient = new HttpClient();
			var uri = new Uri(ServerUrl + "/api-token-auth/");

			var postJson = JsonConvert.SerializeObject(request, Formatting.Indented,
													   new JsonSerializerSettings
													   { ContractResolver = new CamelCasePropertyNamesContractResolver() }
													  );

			var content = new StringContent(postJson, Encoding.UTF8, "application/json");
			var response = await httpClient.PostAsync(uri, content);

			if (response.IsSuccessStatusCode)
			{
				var resultContent = await response.Content.ReadAsStringAsync();
				var resultJson = JObject.Parse(resultContent);

				Settings.Token = (String)resultJson["token"];
				return new LoginResult(request.Username, true, "");
			}
			else {
				var resultContent = await response.Content.ReadAsStringAsync();
				return new LoginResult(request.Username, false, resultContent);
			}
		}
	}
}
