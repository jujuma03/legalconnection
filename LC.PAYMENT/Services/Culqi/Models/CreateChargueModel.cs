using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.PAYMENT.Services.Culqi.Models
{
    public class CreateChargueModel
    {
        [JsonProperty("amount")]
        public string Amount { get; set; }
        [JsonProperty("currency_code")]
        public string Currency_Code => "PEN";
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("source_id")]
        public string Source_Id { get; set; }
    }
}
