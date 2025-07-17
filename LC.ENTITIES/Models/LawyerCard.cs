using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class LawyerCard
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Id { get; set; }
        public Guid LawyerId { get; set; }
        public Lawyer Lawyer { get; set; }
        public string Owner { get; set; }
        public string CardBrand { get; set; }
        public string LastCardDigits { get; set; }
        public bool Default { get; set; }
    }
}
