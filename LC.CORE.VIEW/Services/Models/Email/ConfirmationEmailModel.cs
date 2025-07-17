using System;
using System.Collections.Generic;
using System.Text;

namespace LC.CORE.VIEW.Services.Models.Email
{
    public class ConfirmationEmailModel
    {
        public string Title { get; set; }
        public string Lawyer { get; set; }
        public string LinkRedirect { get; set; }
        public string LinkName { get; set; }
    }
}
