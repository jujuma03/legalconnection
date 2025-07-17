using System;
using System.Collections.Generic;
using System.Text;

namespace LC.REPOSITORY.Repositories.Template
{
    public class LegalCaseExcelTemplate
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
