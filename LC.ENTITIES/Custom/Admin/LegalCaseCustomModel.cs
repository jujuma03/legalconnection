using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Custom.Admin
{
    public class LegalCaseCustomModel
    {
        public Guid Id { get; set; }
        public string CreatedAt { get; set; }
        public string ValidatedAt { get; set; }
        public string Department { get; set; }
        public string Province { get; set; }
        public string Description { get; set; }
        public Guid SpecialityId { get; set; }
        public string Speciality { get; set; }
        public General.ClientCustomModel Client { get; set; }
        public List<General.SpecialityThemesCustomModel> SpecialityThemes { get; set; }
        public List<General.LawyerCustomModel> Lawyers { get; set; }
        public byte Status { get; set; }
        public byte Type { get; set; }
        public string FileUrl { get; set; }
    }
}
