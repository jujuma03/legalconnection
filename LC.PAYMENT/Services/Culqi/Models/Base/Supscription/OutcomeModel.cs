using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.PAYMENT.Services.Culqi.Models.Base.Supscription
{
    public class OutcomeModel
    {
        public string Type { get; set; }
        public string Code { get; set; }
        [JsonProperty("merchant_message")]
        public string MerchantMessage { get; set; }
        [JsonProperty("user_message")]
        public string UserMessage { get; set; }
    }
}
