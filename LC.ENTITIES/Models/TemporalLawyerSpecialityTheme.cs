using LC.CORE.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class TemporalLawyerSpecialityTheme
    {
        public Guid Id { get; set; }
        public Guid? LawyerSpecialityThemeId { get; set; }
        public Guid SpecialityThemeId { get; set; }
        public Guid LawyerId { get; set; }
        public Lawyer Lawyer { get; set; }
        public SpecialityTheme SpecialityTheme { get; set; }
        public LawyerSpecialityTheme LawyerSpecialityTheme { get; set; }
    }
}
