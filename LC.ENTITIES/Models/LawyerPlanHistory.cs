using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class LawyerPlanHistory
    {
        public Guid Id { get; set; }
        public string PlanId { get; set; }
        public Plan Plan { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Amount { get; set; }
    }
}
