using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class ExternalPublication
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Title { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public string PhotoUrl { get; set; }
        public string LawyerFullName { get; set; }
        public string LawyerPhotoUrl { get; set; }
    }
}
