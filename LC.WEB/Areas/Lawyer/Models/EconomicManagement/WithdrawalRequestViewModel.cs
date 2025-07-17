using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Lawyer.Models.EconomicManagement
{
    public class WithdrawalRequestViewModel
    {
        public bool CanRequest { get; set; }
        public byte FinancialEntity { get; set; }
        public string BankAccount { get; set; }
        public string InterbankAccount { get; set; }
        public string Dni { get; set; }
        public string FullName { get; set; }
        public IFormFile ReceiptFileForFees { get; set; }
        public decimal Amount  { get; set; }
        public string Observation { get; set; }
        public string UrlReceiptFileForFees { get; set; }
        public string UrlDepositReceipt { get; set; }
    }
}
