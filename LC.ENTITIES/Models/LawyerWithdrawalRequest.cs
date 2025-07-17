using LC.CORE.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class LawyerWithdrawalRequest
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid LawyerId { get; set; }

        [Column(TypeName = "decimal(19,2)")]
        public decimal Amount { get; set; }
        public string UlrReceiptFileForFees { get; set; }
        public byte Status { get; set; } = ConstantHelpers.ENTITIES.WITHDRAWAL_REQUEST.STATUS.IN_PROCESS;
        public string Observation { get; set; }
        public string UrlDepositReceipt { get; set; }
        public DateTime? DepositDate { get; set; }
        public Lawyer Lawyer { get; set; }

        //Datos del deposito
        public string BankAccount { get; set; }
        public string InterbankAccount { get; set; }
        public string Dni { get; set; }
        public string FullName { get; set; }
        public byte FinancialEntity { get; set; }
    }
}
