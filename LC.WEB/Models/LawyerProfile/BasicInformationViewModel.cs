using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Models.LawyerProfile
{
    public class BasicInformationViewModel
    {
        public string CAL { get; set; }
        public List<SpecialityViewModel> Specialities { get; set; }
        public List<SpecialityThemeViewModel> SpecialityThemes { get; set; }
        public decimal Fee { get; set; }
        public bool FreeFirst { get; set; }
        public int HiredCases { get; set; }
    }
}
