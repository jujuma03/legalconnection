using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.PAYMENT.Services.Culqi.Models.Base.Chargue
{
    public class IssuerModel
    {
        public string Name { get; set; }
        public string Country { get; set; }
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }
        public string WebSite { get; set; }
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }
        [JsonProperty("installments_allowed")]
        public string InstallmentsAllowed { get; set; }
    }
}
