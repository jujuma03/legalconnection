using System;
using System.Collections.Generic;
using System.Text;

namespace LC.CORE.VIEW.Services.Models.Email
{
    public class NewLawyerPaymentEmailModel
    {
        public string ClientFullName { get; set; }
        public string ClientEmail { get; set; }
        public string ClientPhoneNumber { get; set; }
        public decimal Fee { get; set; }
    }
}
