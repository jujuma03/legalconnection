using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Models.WithdrawalRequest
{
    public class LawyerWithdrawalInfoViewModel
    {
        public Guid LawyerId { get; set; }
        public byte FinancialEntity { get; set; }
        public string BankAccount { get; set; }
        public string InterbankAccount { get; set; }
        public string Dni { get; set; }
        public string FullName { get; set; }
    }
}
