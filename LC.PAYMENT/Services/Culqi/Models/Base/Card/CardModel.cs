using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.PAYMENT.Services.Culqi.Models.Base.Card
{
    public class CardModel
    {
        public string Object { get; set; }
        public string Id { get; set; }
        [JsonProperty("creation_date")]
        public string Date { get; set; }
        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }
        public SourceModel Source { get; set; }
        public object Metadata { get; set; }
    }
}
