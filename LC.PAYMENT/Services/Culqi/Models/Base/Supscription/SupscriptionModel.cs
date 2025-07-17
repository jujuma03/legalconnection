using LC.PAYMENT.Services.Culqi.Models.Base.Card;
using LC.PAYMENT.Services.Culqi.Models.Base.Plan;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.PAYMENT.Services.Culqi.Models.Base.Supscription
{
    public class SupscriptionModel
    {
        public string Object { get; set; }
        public string Id { get; set; }
        [JsonProperty("creation_date")]
        public string CreationDate { get; set; }
        public string Status { get; set; }
        [JsonProperty("current_period")]
        public int CurrentPeriod { get; set; }
        [JsonProperty("total_period")]
        public int? TotalPeriod { get; set; }
        [JsonProperty("current_period_start")]
        public string CurrentPeriodStart { get; set; }
        [JsonProperty("current_period_end")]
        public string CurrentPeriodEnd { get; set; }
        [JsonProperty("cancel_at_period_end")]
        public bool CancelAtPeriodEnd { get; set; }
        [JsonProperty("cancel_at")]
        public string CancelAt { get; set; }
        [JsonProperty("ended_at")]
        public string EndedAt { get; set; }
        [JsonProperty("next_billing_date")]
        public string NextBillingDate { get; set; }
        [JsonProperty("trial_start")]
        public string TrialStart { get; set; }
        [JsonProperty("trial_end")]
        public string TrialEnd { get; set; }
        public List<ChargeModel> Charges { get; set; }
        public PlanModel Plan { get; set; }
        public CardModel CardModel { get; set; }
        public object Metadata { get; set; }
    }
}
