using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Client.Models.LegalCase
{
    public class LegalCaseViewModel
    {
        public Guid Id { get; set; }
        public Guid? ProvinceId { get; set; }
        public Guid? DepartmentId { get; set; }
        public string Department { get; set; }
        public string Province { get; set; }
        public List<Guid> SpecialityThemeId { get; set; }
        public string Description { get; set; }
        public Guid SpecialitySelected { get; set; }
        public List<SpecialityViewModel> Specialities { get; set; }
        public List<SpecialityThemeViewModel> SpecialityThemes { get; set; }
        public Guid LawyerId { get; set; }
        public string PhoneNumber { get; set; }
        public int DescriptionMaxLength { get; set; }
        public IFormFile File { get; set; }
        public string FileUrl { get; set; }
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
}
