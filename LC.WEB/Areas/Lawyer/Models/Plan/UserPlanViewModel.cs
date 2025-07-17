using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Lawyer.Models.Plan
{
    public class UserPlanViewModel
    {
        public string CurrentPlanId { get; set; }
        public List<PlanViewModel> Plans { get; set; }
    }
}
