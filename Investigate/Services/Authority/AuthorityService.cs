using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Investigate
{
    public partial class PoddService
    {
        public async Task<AuthorityResult> GetAuthorities()
        {

            var authoritiesString = Settings.Authorities;
            if (authoritiesString != "")
            {
                Debug.WriteLine("using authority in cache");
                Debug.WriteLine(authoritiesString);
                var items = JsonConvert.DeserializeObject<AuthorityItem[]>(authoritiesString);
                Debug.WriteLine(items[0].Name);
				Debug.WriteLine(items[1].Name);
				Debug.WriteLine(items.Length);
                return new AuthorityResult()
                {
                    Success = true,
                    Items = items
                };
            }

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Token " + Settings.Token);


            var uri = new Uri(ServerUrl + "/authorities/");

            Debug.WriteLine("Fetching reports from server");
            var values = new Dictionary<string, string>
            {
                {"short", "true"}
            };

            var content = new FormUrlEncodedContent(values);
            var query = await content.ReadAsStringAsync();

            var response = await httpClient.GetAsync(uri + "?" + query);
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Error with code = {0}", response.StatusCode);

                return new AuthorityResult()
                {
                    Success = false,
                    ErrorMessage = await response.Content.ReadAsStringAsync()
                };

            }

            var resultContent = await response.Content.ReadAsStringAsync();
            // Cache authorities string
            Settings.Authorities = resultContent;
            Debug.WriteLine("success with text = {0}", resultContent);

            var result = JsonConvert.DeserializeObject<AuthorityItem[]>(resultContent);
            return new AuthorityResult()
            {
                Success = true,
                Items = result
            };

        }
    }
}