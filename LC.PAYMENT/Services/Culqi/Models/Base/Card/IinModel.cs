using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.PAYMENT.Services.Culqi.Models.Base.Card
{
    public class IinModel
    {
        public string Object { get; set; }
        public string Bin { get; set; }
        [JsonProperty("card_brand")]
        public string CardBrand { get; set; }
        [JsonProperty("card_type")]
        public string CardType { get; set; }
        [JsonProperty("card_category")]
        public string CardCategory { get; set; }
        public IssuerModel Issuer { get; set; }
        [JsonProperty("installments_allowed")]
        public string[] InstallmentsAllowed { get; set; }
    }
}
