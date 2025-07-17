using LC.CORE.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class LawyerStudy
    {
        public Guid Id { get; set; }
        public byte Grade { get; set; }
        public string Mention { get; set; }
        public string Ubication { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public byte TemporalStatus { get; set; } = ConstantHelpers.ENTITIES.LAWYER_STUDY.TEMPORAL_STATUS.VALIDATED;
        public Guid LawyerId { get; set; }
        public Lawyer Lawyer { get; set; }
    }
}
