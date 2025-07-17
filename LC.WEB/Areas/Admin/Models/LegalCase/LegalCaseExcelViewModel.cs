using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Models.LegalCase
{
    public class LegalCaseExcelViewModel
    {
        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Logo { get; set; }
        public string JsPath { get; set; }
        public string Time { get; set; }
        public DateTime Today { get; set; }
        public List<CaseViewModel> Result { get; set; }

        public LegalCaseExcelViewModel()
        {
            Result = new List<CaseViewModel> { };
        }
    }

    public class CaseViewModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string ClientFullName { get; set; }
        public string Dni { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string CreatedAt { get; set; }
        public string Speciality { get; set; }
        public string SpecialityThemes { get; set; }
        public string Lawyer { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
    }
}
