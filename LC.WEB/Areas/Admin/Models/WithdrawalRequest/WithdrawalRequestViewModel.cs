using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Models.WithdrawalRequest
{
    public class WithdrawalRequestViewModel
    {
        public Guid Id { get; set; }
        public string CreatedAt { get; set; }
        public string DepositDate { get; set; }
        public decimal Amount { get; set; }
        public string UlrReceiptFileForFees { get; set; }
        public byte Status { get; set; }
        public IFormFile DepositReceiptFile { get; set; }
        public string UrlDepositReceipt { get; set; }
        public string Observation { get; set; }
        public LawyerWithdrawalInfoViewModel LawyerWithdrawalInfo { get; set; }
    }
}
