using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Lawyer.Models.YourAccount
{
    public class PlanDetail
    {
        public string Plan { get; set; }
        public decimal Amount { get; set; }
        public string NextBillingDate { get; set; }
        public DateTime? NextBillingDateTime { get; set; }
        public bool Canceled { get; set; }
        public LawyerCardViewModel CurrentCard { get; set; }
    }
}
