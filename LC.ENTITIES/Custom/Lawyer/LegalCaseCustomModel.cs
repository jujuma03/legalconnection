using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Custom.Lawyer
{
    public class LegalCaseCustomModel
    {
        public Guid Id { get; set; }
        public byte Status { get; set; }
        public string DerivatedAt { get; set; }
        public string PostulationDate { get; set; }
        public string FinishDate { get; set; }
        public DateTime DerivatedAtUtc { get; set; }
        public string Province { get; set; }
        public string Department { get; set; }
        public byte Type { get; set; }
        public string Description { get; set; }
        public string Speciality { get; set; }
        public List<General.SpecialityThemesCustomModel> SpecialityThemes { get; set; }
        public int TotalApplicants { get; set; }
        public int TotalVacancies { get; set; }
        public int Vacancies => TotalVacancies - TotalApplicants;
        public byte SearchType { get; set; }
        public decimal Payment { get; set; }
        public bool IsFreeUser { get; set; }
        public bool IsFiledCase { get; set; }
        public bool IsFreeFee { get; set; }
        public General.ClientCustomModel Client { get; set; }
        public List<General.QuestionCustomModel> Questions { get; set; }

    }
}
