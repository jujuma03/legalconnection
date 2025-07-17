using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Models.Payment
{
    public class PaymentViewModel
    {
        public string Token { get; set; }
        public int Amount { get; set; }
        public string Email { get; set; }
    }
}
