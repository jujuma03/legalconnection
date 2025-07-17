using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Models.Plan
{
    public class PlanViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DescriptionLC { get; set; }
        public decimal Amount { get; set; }
        public byte Interval { get; set; }
        public int IntervalCount { get; set; }
        public int TrialDays { get; set; }
        public List<Benefit> Benefits { get; set; }
    }
}
