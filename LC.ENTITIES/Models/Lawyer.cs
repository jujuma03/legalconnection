using LC.CORE.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class Lawyer
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string CAL { get; set; }
        [Column(TypeName = "decimal(19,2)")]
        public decimal Fee { get; set; }
        public byte Status { get; set; } = ConstantHelpers.ENTITIES.LAWYER.STATUS.PENDING;
        public bool FreeFirst { get; set; }
        public string AboutMe { get; set; }
        public bool ProfileWithChanges { get; set; }
        public int FreeLegalCases { get; set; }
        public bool FreeUser { get; set; }
        public bool PublicProfile { get; set; }
        public bool TermsAndConditions { get; set; }
        public DateTime? ValidationDate { get; set; }
        [Required]
        public string UserId { get; set; }
        public string CustomerId { get; set; }
        public LawyerPlanDetail LawyerPlanDetail { get; set; }
        public LawyerWithdrawalInfo LawyerWithdrawalInfo { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<LawyerSpecialityTheme> LawyerSpecialityThemes { get; set; }
        public ICollection<LawyerExperience> LawyerExperiences { get; set; }
        public ICollection<LawyerStudy> LawyerStudies { get; set; }
        public ICollection<LawyerPublication> LawyerPublications { get; set; }
        public ICollection<LawyerLanguage> LawyerLanguages { get; set; }
        public ICollection<LegalCaseApplicantLawyer> LegalCaseApplicantLawyers { get; set; }
        public ICollection<LegalCaseLawyer> LegalCaseLawyers { get; set; }
        public ICollection<LawyerObservation> LawyerObservations { get; set; }
        public ICollection<LegalCaseFiledLawyer> LegalCaseFiledLawyers { get; set; }
        public ICollection<LawyerQualification> LawyerQualifications { get; set; }
        public ICollection<LawyerPlanHistory> LawyerPlanHistories { get; set; }
        public ICollection<LawyerCard> LawyerCards { get; set; }
        public ICollection<LawyerWithdrawalRequest> WithdrawalRequests { get; set; }
        public ICollection<LawyerInterview> LawyerInterviews { get; set; }
        public ICollection<LawyerWithdrawal> LawyerWithdrawals { get; set; }

        [NotMapped]
        public int HiredCases { get; set; }
    }
}
