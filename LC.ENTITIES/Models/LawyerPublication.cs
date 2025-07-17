using LC.CORE.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class LawyerPublication
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public byte Status { get; set; } = ConstantHelpers.ENTITIES.LAWYER_PUBLICATION.STATUS.PENDING;
        public DateTime? AnswerDate { get; set; }
        public Guid LawyerId { get; set; }
        public Lawyer Lawyer { get; set; }
        public string PhotoUrl { get; set; }
    }
}
