using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Client.Models.Profile
{
    public class PersonalInformationViewModel
    {
        public Guid ClientId { get;  set; }
        public string BirthDate { get;  set; }
        public string DNI { get;  set; }
        public string DocumentType { get;  set; }
        public string Department { get;  set; }
        public Guid? DepartmentId { get;  set; }
        public string District { get;  set; }
        public Guid? DistrictId { get;  set; }
        public string Email { get;  set; }
        public string HouseNumber { get;  set; }
        public string Name { get;  set; }
        public string PhoneNumber { get;  set; }
        public string Province { get;  set; }
        public Guid? ProvinceId { get;  set; }
        public byte Sex { get;  set; }
        public string Surnames { get;  set; }
    }
}
