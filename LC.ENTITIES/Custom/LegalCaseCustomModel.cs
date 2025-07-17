using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Custom
{
    public class LegalCaseCustomModel
    {
        public Guid Id { get; set; }
        public string Speciality { get; set; }
        public string FinishDate { get; set; }
        public string SpecialityTheme { get; set; }
        public string CreatedAt { get; set; }
        public string ValidatedAt { get; set; }
        public string Department { get; set; }
        public string Province { get; set; }
        public byte Status { get; set; }
        public string Description { get; set; }
        public string ClientFullName { get; set; }
        public int TotalApplicants { get; set; }
        public int PublicatedDays { get; set; }
        public int MaxHoursToClientAcceptLawyer { get; set; }
        public List<LegalCasePaymentCustomModel> Payments { get; set; }
        public ClientCustomModel Client { get; set; }
        public LegalCaseObservationCustomModel LastObservation { get; set; }
        public bool TimeFinishedToAcceptApplications { get; set; }

        //para borrar
        public string PhotoMainLawyer { get; set; }
        public string MainLawyer { get; set; }
    }

    public class LegalCasePaymentCustomModel
    {
        public Guid LawyerId { get; set; }
        public decimal Amount { get; set; }
    }
}
