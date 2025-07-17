using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.PAYMENT.Services.Culqi.Models
{
    public class CreateSubscription
    {
        [JsonProperty("card_id")]
        public string CardId { get; set; }
        [JsonProperty("plan_id")]
        public string PlanId { get; set; }
    }
}
