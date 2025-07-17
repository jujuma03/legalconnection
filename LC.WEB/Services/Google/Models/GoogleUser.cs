using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Services.Google.Models
{
    public class GoogleUser
    {
        [JsonProperty("family_name")]
        public string LastName { get; set; }
        public string Name { get; set; }
        [JsonProperty("given_name")]
        public string FirstName { get; set; }
        public string Email { get; set; }
    }
}
