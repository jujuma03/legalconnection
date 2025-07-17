using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.PAYMENT.Services.Culqi.Models.Base.Supscription
{
    public class ChargeModel
    {
        public string Duplicated { get; set; }
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
        [JsonProperty("currency_code")]
        public string CurrecyCode { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public SourceModel Source { get; set; }
        public OutcomeModel Outcome { get; set; }
        public string FraudScore { get; set; }
        [JsonProperty("antifraud_details")]
        public AntifraudDetail AntifraudDetail { get; set; }
        public bool Dispute { get; set; }
        public string capture { get; set; }
        [JsonProperty("reference_code")]
        public string ReferenceCode { get; set; }
        [JsonProperty("authorization_code")]
        public string AuthorizationCode { get; set; }
        public object Metadata { get; set; }
        [JsonProperty("total_fee")]
        public int TotalFee { get; set; }
        [JsonProperty("fee_details")]
        public FeeDetailModel FeeDetails { get; set; }
        [JsonProperty("total_fee_taxes")]
        public int TotalFeeTaxes { get; set; }
        [JsonProperty("transfer_amount")]
        public int TransferAmount { get; set; }
        public bool Paid { get; set; }
        [JsonProperty("statement_descriptor")]
        public string StatementDescriptor { get; set; }
        [JsonProperty("transfer_id")]
        public string TransferId { get; set; }
        [JsonProperty("capture_date")]
        public string CaptureDate { get; set; }
    }
}
