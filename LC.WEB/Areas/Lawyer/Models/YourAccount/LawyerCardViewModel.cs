using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Lawyer.Models.YourAccount
{
    public class LawyerCardViewModel
    {
        public string Id { get; set; }
        public string Owner { get; set; }
        public string CardBrand { get; set; }
        public string LastCardDigits { get; set; }
    }
}
