using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Custom
{
    public class PaymentCustomModel
    {
        public string Token { get; set; }
        public int Amount { get; set; }
        public string Email { get; set; }
    }
    public class WithdrawalRequestCustomModel
    {
        public Guid Id { get; set; }
        public string LawyerFullName { get; set; }
        public string RegisterDate { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string BankAccount { get; set; }
        public string InterBankAccount { get; set; }
        public string UrlDepositReceipt { get; set; }
    }
}
