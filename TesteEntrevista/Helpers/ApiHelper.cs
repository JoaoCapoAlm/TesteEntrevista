using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace TesteEntrevista.Helpers
{
    public class ApiHelper
    {
        const string baseUrl = "https://camposdealer.dev/Sites/TesteAPI/";
        public async Task<MatchCollection?> CallApiAsync(string pathUrl)
        {
            var converted = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(baseUrl + pathUrl);

                var responseBody = await response.Content.ReadAsStringAsync();
                converted = JsonConvert.DeserializeObject<string>(responseBody);
            }

            var regex = new Regex(@"\{([^}]*)\}");
            return regex.Matches(converted ?? "");
        }
    }
}
