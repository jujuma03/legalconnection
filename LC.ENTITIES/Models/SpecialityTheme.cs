using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class SpecialityTheme
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string OfficialName { get; set; }
        public string ColloquialName { get; set; }
        public Guid SpecialityId { get; set; }
        public Speciality Speciality { get; set; }

        public ICollection<LawyerSpecialityTheme> LawyerSpecialityThemes { get; set; }
    }
}
