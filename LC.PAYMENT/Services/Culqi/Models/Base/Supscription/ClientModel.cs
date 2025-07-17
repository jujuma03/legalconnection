using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.PAYMENT.Services.Culqi.Models.Base.Supscription
{
    public class ClientModel
    {
        public string Ip { get; set; }
        [JsonProperty("ip_country")]
        public string IpCountry { get; set; }
        [JsonProperty("ip_country_code")]
        public string IpCountryCode { get; set; }
        public string Browser { get; set; }
        [JsonProperty("device_fingerprint")]
        public string DeviceFingerprint { get; set; }
        [JsonProperty("device_type")]
        public string DeviceType { get; set; }
    }
}
