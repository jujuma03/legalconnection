using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.PAYMENT.Services.Culqi.Models.Base.Plan
{
    public class PlanModel
    {
        public string Object { get; set; }
        public string Id { get; set; }
        [JsonProperty("creation_date")]
        public Int64 CreationDate { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }
        [JsonProperty("interval_count")]
        public int IntervalCount { get; set; }
        public string Interval { get; set; }
        public int Limit { get; set; }
        [JsonProperty("trial_days")]
        public int TrialDays { get; set; }
        [JsonProperty("total_subscriptions")]
        public int TotalSubscriptions { get; set; }
        public object Metadata { get; set; }
    }
}
