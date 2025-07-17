using System;
using System.Collections.Generic;
using System.Text;

namespace LC.CORE.VIEW.Services.Models.Email
{
    public class ReceiptEmailModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string DocumentType { get; set; }
        public string NumberSerie { get; set; }
        public string RUC { get; set; }
        public string ClientDNI { get; set; }
        public string IssueDate { get; set; }
        public string Currency { get; set; }
        public string TotalAmount { get; set; }
        public string LawyerName { get; set; }
        public string LawyerEmail { get; set; }
        public string LawyerPhoneNumber { get; set; }
        public string GoodbyeMessage { get; set; }
    }
}
