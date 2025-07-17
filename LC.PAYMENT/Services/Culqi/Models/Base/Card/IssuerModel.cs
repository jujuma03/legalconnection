using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.PAYMENT.Services.Culqi.Models.Base.Card
{
    public class IssuerModel
    {
        public string Name { get; set; }
        public string Country { get; set; }
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }
        public string Website { get; set; }
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }
    }
}
