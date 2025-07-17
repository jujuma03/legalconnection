using LC.CORE.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class TemporalLawyerExperience
    {
        [Key]
        public Guid LawyerExperienceId { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public string WorkArea { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string PhotoUrl { get; set; }
        public Guid LawyerId { get; set; }
        public Lawyer Lawyer { get; set; }
        [ForeignKey("LawyerExperienceId")]
        public LawyerExperience LawyerExperience { get; set; }
    }
}
