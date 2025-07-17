using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Custom.Admin
{
    public class LawyerCustomModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surnames { get; set; }
        public string FullName { get; set; }
        public string Code { get; set; }
        public string IsPublic { get; set; }
        public string Ubigeo { get; set; }
        public List<string> SpecialitiesList { get; set; }
        public string Specialties { get; set; }
        public string RegisterDate { get; set; }
        public string ValidationDate { get; set; }
        public int LegalCasesFinalized { get; set; }
        public int LegalCasesReceived { get; set; }
        public int LegalCasesRejected { get; set; }
        public int LegalCasesApplied { get; set; }
        public string Status { get; set; }
        public string PaymentDate { get; set; }
        public string Plan { get; set; }
    }
}
