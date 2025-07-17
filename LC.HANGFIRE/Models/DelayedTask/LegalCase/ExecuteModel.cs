using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.HANGFIRE.Models.DelayedTask.LegalCase
{
    public class ExecuteModel
    {
        public Guid LegalCaseId { get; set; }
        public byte Task { get; set; }
    }
}
