using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class Client
    {
        public Guid Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<LegalCase> LegalCases { get; set; }
    }
}
