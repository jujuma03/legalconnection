using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Client.Models.Profile
{
    public class ProfileViewModel
    {
        public Guid ClientId { get; set; }
        public PersonalInformationViewModel PersonalInformation { get; set; }
    }
}
