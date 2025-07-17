using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.PAYMENT.Services.Culqi.Models
{
    public class CreateCustomerModel
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("address_city")]
        public string AddressCity { get; set; }
        [JsonProperty("country_code")]
        public string CountryCode => "PE";
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }
    }
}
