using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Custom
{
    public class LegalCaseItemCustomModel
    {
        public Guid Id { get; set; }
        public string SpecialityTheme { get; set; }
        public string Speciality { get; set; }
        public string ClientFullName { get; set; }
        public string ValidatedAt { get; set; }
        public byte Status { get; set; }
        public string Department { get; set; }
        public string Province { get; set; }
        public string Description { get; set; }
        public int TotalApplicants { get; set; }
        public int TotalVacancies { get; set; }
        public int Vacancies => TotalVacancies - TotalApplicants;
        public byte TypeSearch { get; set; }
        public bool IsDirected { get; set; }
    }
}
