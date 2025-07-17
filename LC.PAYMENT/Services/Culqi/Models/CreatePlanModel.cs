using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.PAYMENT.Services.Culqi.Models
{
    public class CreatePlanModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("amount")]
        public int Amount { get; set; }
        [JsonProperty("currency_code")]
        public string CurrencyCode => "PEN";
        [JsonProperty("interval")]
        public string Interval { get; set; }
        [JsonProperty("interval_count")]
        public int IntervalCount { get; set; }
        [JsonProperty("trial_days")]
        public int TrialDays { get; set; }
    }
}
