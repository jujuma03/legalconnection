using LC.CORE.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class Blog
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public byte Type { get; set; } = ConstantHelpers.ENTITIES.BLOG.TYPES.LAWYERPUBLICATION;
        public Guid? ExternalPublicationId { get; set; }
        public ExternalPublication ExternalPublication { get; set; }
        public Guid? LawyerPublicationId { get; set; }
        public LawyerPublication LawyerPublication { get; set; }
    }
}
