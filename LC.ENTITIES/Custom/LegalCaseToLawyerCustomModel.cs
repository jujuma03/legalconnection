using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Custom
{
    public class LegalCaseToLawyerCustomModel
    {
        public Guid Id { get; set; }
        public string Speciality { get; set; }
        public string SpecialityTheme { get; set; }
        public string Department { get; set; }
        public string Province { get; set; }
        public byte Status { get; set; }
        public string Description { get; set; }
        public string ValidatedAt { get; set; }
        public ClientCustomModel Client { get; set; }
        public int TotalApplicants { get; set; }
        public int TotalVacancies { get; set; }
        public int Vacancies => TotalVacancies - TotalApplicants;
        public int PublicatedDays { get; set; }
        public byte SearchType { get; set; }
        public List<LawyerCustomModel> LawyerApplicants { get; set; }
        public List<LawyerCustomModel> Lawyers { get; set; }
        public List<LegalCaseQuestionCustomModel> Questions { get; set; }
    }

}
