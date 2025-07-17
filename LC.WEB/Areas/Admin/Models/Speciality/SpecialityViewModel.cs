using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Models.Speciality
{
    public class SpecialityViewModel
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string OfficialName { get; set; }
        public string ColloquialName { get; set; }
    }
}
