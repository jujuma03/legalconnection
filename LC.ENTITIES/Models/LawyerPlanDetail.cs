using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class LawyerPlanDetail
    {
        [Key]
        public Guid LawyerId { get; set; }
        [ForeignKey("LawyerId")]
        public Lawyer Lawyer { get; set; }
        public string PlanId { get; set; }
        public Plan Plan { get; set; }
        public string SubscriptionId { get; set; }
        public string LawyerCardId { get; set; }
        public LawyerCard LawyerCard { get; set; }
        public DateTime? TempStartDate { get; set; }
        public DateTime? TempEndDate { get; set; }
        public bool Canceled { get; set; }
    }
}
