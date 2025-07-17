using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Lawyer.Models.Profile
{
    public class PersonalInformationViewModel
    {
        //
        public Guid LawyerId { get; set; }
        //
        public string Name { get; set; }
        public string Surnames { get; set; }
        public string FullName => $"{Name} {Surnames}";
        public string Email { get; set; }
        public string DNI { get; set; }
        public string PhoneNumber { get; set; }
        public string HouseNumber { get; set; }
        public string BirthDate { get; set; }
        public byte Sex { get; set; }
        public string Department { get; set; }
        public Guid DepartmentId { get; set; }
        public string Province { get; set; }
        public Guid ProvinceId { get; set; }
        public string District { get; set; }
        public Guid DistrictId { get; set; }
        public IFormFile Photo { get; set; }
        public string PhotoUrl { get; set; }
        public string urlLawyerPhotoCropImg { get; set; }
        public string FeaturedStudy { get; set; }
        public string LastConnection { get; set; }
        public string RegistrationDate { get; set; }
        public int Qualification { get; set; }
        public int QualificationQuantity { get; set; }
    }
}
