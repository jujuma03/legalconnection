using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.PAYMENT.Services.Culqi.Models.Base.Chargue
{
    public class VariableFeeModel
    {
        [JsonProperty("currency_code")]
        public string CurreyncyCode { get; set; }
        public double Commision { get; set; }
        public double Total { get; set; }
    }
}
