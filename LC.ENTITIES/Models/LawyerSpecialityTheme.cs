using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class LawyerSpecialityTheme
    {
        public Guid Id { get; set; }
        public Guid SpecialityThemeId { get; set; }
        public Guid LawyerId { get; set; }
        public Lawyer Lawyer { get; set; }
        public SpecialityTheme SpecialityTheme { get; set; }
    }
}
