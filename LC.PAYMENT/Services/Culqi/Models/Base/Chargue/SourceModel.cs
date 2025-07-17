using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.PAYMENT.Services.Culqi.Models.Base.Chargue
{
    public class SourceModel
    {
        public string Object { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }
        public string Email { get; set; }
        [JsonProperty("creation_date")]
        public string CreationDate { get; set; }
        [JsonProperty("card_number")]
        public string CardNumber { get; set; }
        [JsonProperty("last_four")]
        public string LastFour { get; set; }
        public bool Active { get; set; }
        public IinModel Iin { get; set; }
        public ClientModel Client { get; set; }
        public OutcomeModel Outcome { get; set; }
        [JsonProperty("fraud_score")]
        public double FraudScore { get; set; }
        [JsonProperty("antifraud_details")]
        public AntifraudDetailsModel AntifraudDetails { get; set; }
        public bool Dispute { get; set; }
        public bool Capture { get; set; }
        [JsonProperty("reference_code")]
        public string ReferenceCode { get; set; }
        public object Metadata { get; set; }
        [JsonProperty("total_fee")]
        public int TotalFee { get; set; }
        [JsonProperty("fee_details")]
        public FeeDetailsModel FeeDetails { get; set; }
        public bool Paid { get; set; }
        [JsonProperty("statement_descriptor")]
        public string StatementDescriptor { get; set; }
        [JsonProperty("total_fee_taxes")]
        public int TotalFeeTaxes { get; set; }
        [JsonProperty("transfer_amount")]
        public int TransferAmount { get; set; }
        public bool Dupicated { get; set; }
    }
}
