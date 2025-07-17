using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.PAYMENT.Services.Culqi.Models
{
    public class CreateCardModel
    {
        [JsonProperty("validate")]
        public bool Validate => true;
        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }
        [JsonProperty("token_id")]
        public string TokenId { get; set; }
    }
}
