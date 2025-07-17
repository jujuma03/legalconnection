using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.PAYMENT.Services.Culqi.Models.Base.Supscription
{
    public class FeeDetailModel
    {
        [JsonProperty("fixed_fee")]
        public object FixedFee { get; set; }
        [JsonProperty("variable_fee")]
        public VariableFeeModel VariableFee { get; set; }
    }
}
