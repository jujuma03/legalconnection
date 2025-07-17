using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class LawyerWithdrawal
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid LawyerId { get; set; }
        [Column(TypeName = "decimal(19,2)")]
        public decimal Amount { get; set; }
        public Guid WithdrawalRequestId { get; set; }
        public LawyerWithdrawalRequest WithdrawalRequest { get; set; }
        public Lawyer Lawyer { get; set; }
    }
}
