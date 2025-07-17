using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.PAYMENT.Services.Culqi.Models.Base.Supscription
{
    public class SourceModel
    {
        public string Object { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }
        [JsonProperty("creation_date")]
        public string CreationDate { get; set; }
        public string Email { get; set; }
        [JsonProperty("creation_date")]
        public string CardNumber { get; set; }
        [JsonProperty("last_four")]
        public string LastFour { get; set; }
        public bool Active { get; set; }
        public IinModel Iin { get; set; }
        public ClientModel Client { get; set; }
        public object Metadata { get; set; }
    }
}
