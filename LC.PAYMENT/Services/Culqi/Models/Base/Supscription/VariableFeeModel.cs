using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.PAYMENT.Services.Culqi.Models.Base.Supscription
{
    public class VariableFeeModel
    {
        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }
        public decimal Commision { get; set; }
        public int Total { get; set; }
    }
}
