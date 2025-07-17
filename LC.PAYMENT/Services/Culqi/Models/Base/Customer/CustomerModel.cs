using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.PAYMENT.Services.Culqi.Models.Base.Customer
{
    public class CustomerModel
    {
        public string Object { get; set; }
        public string Id { get; set; }
        [JsonProperty("creation_date")]
        public Int64 CreationDate { get; set; }
        public string Email { get; set; }
        [JsonProperty("antifraud_details")]
        public AntifraudDetailModel AntifraudDetail { get; set; }
    }
}
