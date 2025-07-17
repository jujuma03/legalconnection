using LC.CORE.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class TemporalLawyerStudy
    {
        [Key]
        public Guid? LawyerStudyId { get; set; }
        public byte Grade { get; set; }
        public string Mention { get; set; }
        public string Ubication { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public Guid LawyerId { get; set; }
        public Lawyer Lawyer { get; set; }
        [ForeignKey("LawyerStudyId")]
        public LawyerStudy LawyerStudy { get; set; }
    }
}
