using System;
using System.Collections.Generic;
using System.Text;

namespace LC.CORE.VIEW.Services.Models.Email
{
    public class StandardEmailModel
    {
        public string Title { get; set; }
        public string SubHeader { get; set; }
        public string LinkName { get; set; }
        public string LinkRedirect { get; set; }
    }
}
