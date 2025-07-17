using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class Speciality
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string OfficialName { get; set; }
        public string ColloquialName { get; set; }
        public ICollection<SpecialityTheme> SpecialityThemes { get; set; }
    }
}
