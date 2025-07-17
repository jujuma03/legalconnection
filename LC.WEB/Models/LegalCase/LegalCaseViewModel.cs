using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Models.LegalCase
{
    public class LegalCaseViewModel
    {
        public Guid Id { get; set; }
        public Guid? ProvinceId { get; set; }
        public Guid? DepartmentId { get; set; }
        public List<Guid> SpecialityThemeId { get; set; }
        public string Description { get; set; }
        public Guid SpecialitySelected { get; set; }
        public List<SpecialityViewModel> Specialities { get; set; }
        public List<SpecialityThemeViewModel> SpecialityThemes { get; set; }
        public ClientViewModel Client { get; set; }
        public string LawyerFullName { get; set; }
        public Guid LawyerId { get; set; }
        public int DescriptionMaxLength { get; set; }
        public IFormFile File { get; set; }
        public string FileUrl { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class SpecialityViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class SpecialityThemeViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class ClientViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool TermsAndConditions { get; set; }
        public string Name { get; set; }
        public string Surnames { get; set; }
        public string GoogleTokenId { get; set; }
    }
}
