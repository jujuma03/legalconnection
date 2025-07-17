using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.PAYMENT.Services.Culqi.Models.Base.Chargue
{
    public class ChargueModel
    {
        public string Object { get; set; }
        public string Id { get; set; }
        [JsonProperty("creation_date")]
        public Int64 CretionDate { get; set; }
        public int Amount { get; set; }
        [JsonProperty("amount_refunded")]
        public int AmountRefunded { get; set; }
        [JsonProperty("current_amount")]
        public int CurrentAmount { get; set; }
        public int Installments { get; set; }
        [JsonProperty("installments_amount")]
        public int InstallmentsAmount { get; set; }
        public string Currency { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public SourceModel Source { get; set; }
    }
}
