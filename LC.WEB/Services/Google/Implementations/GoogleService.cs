using LC.WEB.Services.Google.Interfaces;
using LC.WEB.Services.Google.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LC.WEB.Services.Google.Implementations
{

    public class GoogleService : IGoogleService
    {
        private const string GoogleApiTokenInfoUrl = "https://www.googleapis.com/oauth2/v3/tokeninfo?id_token={0}";
        public async Task<GoogleUser> GetUserByGoogleTokenId(string googleTokenId)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var uri = new Uri(string.Format(GoogleApiTokenInfoUrl, googleTokenId));
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    using (var response = await client.GetAsync(uri))
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<GoogleUser>(responseString);
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
