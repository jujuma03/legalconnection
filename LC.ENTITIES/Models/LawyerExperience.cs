using LC.CORE.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class LawyerExperience
    {
        public Guid Id { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public string WorkArea { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid LawyerId { get; set; }
        public Lawyer Lawyer { get; set; }
        public byte TemporalStatus { get; set; } = ConstantHelpers.ENTITIES.LAWYER_EXPERIENCE.TEMPORAL_STATUS.VALIDATED;
        public string PhotoUrl { get; set; }
    }
}
