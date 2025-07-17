using LC.WEB.Models.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Client.Models.LegalCase
{
    public class ApplicantViewModel : PaymentViewModel
    {
        public Guid LawyerId { get; set; }
        public Guid LegalCaseId { get; set; }
        public bool IsFreeFee { get; set; } = false;
    }
}
