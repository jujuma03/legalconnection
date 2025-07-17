using System;
using System.Collections.Generic;
using System.Text;

namespace LC.CORE.Services.Models
{
    public class EmailConfig
    {
        public string MailServerAddress { get; set; }
        public string MailServerPort { get; set; }
        public string UserEmailAddress { get; set; }
        public string UserPassword { get; set; }
    }
}
