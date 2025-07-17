using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.PAYMENT.Services.Culqi.Models.Base
{
    public class ErrorModel
    {
        public string Object { get; set; }
        public string Type { get; set; }
        [JsonProperty("charge_id")]
        public string ChargueId { get; set; }
        public string Code { get; set; }
        [JsonProperty("decline_code")]
        public string DeclineCode { get; set; }
        [JsonProperty("merchant_message")]
        public string MerchantMessage { get; set; }
        [JsonProperty("user_message")]
        public string UserMessage { get; set; }
        public string Param { get; set; }
    }
}
