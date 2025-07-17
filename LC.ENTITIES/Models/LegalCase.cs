using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class LegalCase
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ValidationDate { get; set; }
        public DateTime? DerivedDate { get; set; }
        public DateTime? SelectionLawyerStartDate { get; set; }
        public bool Reviewed { get; set; }
        public DateTime? FinishDate { get; set; }
        public byte Status { get; set; }
        public byte Type { get; set; }
        public Guid ClientId { get; set; }
        public Guid ProvinceId { get; set; }
        public string Observation { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string UrlFile { get; set; }
        public Province Province { get; set; }
        public Client Client { get; set; }
        public ICollection<LegalCaseSpecialityTheme> LegalCaseSpecialityThemes { get; set; }
        public ICollection<LegalCaseApplicantLawyer> LegalCaseApplicantLawyers { get; set; }
        public ICollection<LegalCaseLawyer> LegalCaseLawyers { get; set; }
        public ICollection<LegalCaseResponse> LegalCaseResponses { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<LegalCaseObservation> LegalCaseObservations { get; set; }
        public ICollection<LegalCaseDelayedTask> LegalCaseDelayedTasks { get; set; }
        public ICollection<LegalCaseFiledLawyer> LegalCaseFiledLawyers { get; set; }
    }
}
