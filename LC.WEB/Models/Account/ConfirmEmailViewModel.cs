using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Models.Account
{
    public class ConfirmEmailViewModel
    {
        public string FullName { get; set; }
        public int ProccessValidationMaxHours { get; set; }
        public string Role { get; set; }
    }
}
