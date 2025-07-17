using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Lawyer.Models.YourAccount
{
    public class LawyerViewModel
    {
        public string Email { get; set; }
        public BillingDataViewModel BillingData { get; set; }
    }
}
