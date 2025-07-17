using LC.ENTITIES.Custom.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Custom.Client
{
    public class LegalCaseCustomModel
    {
        public Guid Id { get; set; }
        public string CreatedAt { get; set; }
        public string Department { get; set; }
        public string Province { get; set; }
        public string Description { get; set; }
        public string Speciality { get; set; }
        public List<General.SpecialityThemesCustomModel> SpecialityThemes { get; set; }
        public List<General.LawyerCustomModel> Lawyers { get; set; }
        public byte Status { get; set; }
        public byte Type { get; set; }
        public string FileUrl { get; set; }
        public DateTime? ValidationDateUtc { get; set; }
        public bool AnyLawyerApplicant { get; set; }
        public DateTime? DerivatedDateUtc { get; set; }
        public DateTime? ApplicationDeadlineUtc { get; set; }
        public DateTime? SelectionDeadlineUtc { get; set; }
        public int MaxHoursToClientAcceptAndPayLawyer { get; set; }
        public int DescriptionMaxLength { get; set; }
        //Para el detalle
        public string Observation { get; set; }
    }
}
