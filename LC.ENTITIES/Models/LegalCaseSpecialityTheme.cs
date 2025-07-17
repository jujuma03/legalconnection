using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class LegalCaseSpecialityTheme
    {
        [Key]
        public Guid LegalCaseId { get; set; }
        [Key]
        public Guid SpecialityThemeId { get; set; }
        public LegalCase LegalCase { get; set; }
        public SpecialityTheme SpecialityTheme { get; set; }
    }
}
