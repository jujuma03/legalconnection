using LC.CORE.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Name { get; set; }
        public string Surnames { get; set; }
        public string FullName { get; set; }
        public string Document { get; set; }
        public string HouseNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public byte Sex { get; set; } = ConstantHelpers.ENTITIES.USER.SEX.UNSPECIFIED;
        public string Picture { get; set; }
        public Guid? DistrictId { get; set; }
        public District  District { get; set; }
        public DateTime LastConnection { get; set; } = DateTime.UtcNow;
        public byte RegisterBy { get; set; } = ConstantHelpers.ENTITIES.USER.REGISTER_BY.LC;
        public byte DocumentType { get; set; } = ConstantHelpers.DOCUMENT_TYPE.DNI;
        public List<ApplicationUserRole> UserRoles { get; set; }
    }
}
