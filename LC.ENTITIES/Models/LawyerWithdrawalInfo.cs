using LC.CORE.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class LawyerWithdrawalInfo
    {
        [Key]
        public Guid LawyerId { get; set; }
        public Lawyer Lawyer { get; set; }
        public byte FinancialEntity { get; set; } = ConstantHelpers.ENTITIES.LAWYER_WITHDRAWAL_INFO.FINANCIAL_ENTITY.NONE;
        public string BankAccount { get; set; }
        public string InterbankAccount { get; set; }
        public string Dni { get; set; }
        public string FullName { get; set; }
    }
}
