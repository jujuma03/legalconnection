using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.PAYMENT.Services.Culqi.Models.Base.Chargue
{
    public class FeeDetailsModel
    {
        [JsonProperty("fixed_fee")]
        public string[] FixedFee { get; set; }
        [JsonProperty("variable_fee")]
        public VariableFeeModel VariableFee { get; set; }
    }
}
