using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Lawyer.Models.YourAccount
{
    public class BillingDataViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressCity { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool IsNew { get; set; } = true;
    }
}
