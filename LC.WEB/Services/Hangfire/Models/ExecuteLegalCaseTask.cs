using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Services.Hangfire.Models
{
    public class ExecuteLegalCaseTask
    {
        public Guid LegalCaseId { get; set; }
        public byte Task { get; set; }
    }
}
