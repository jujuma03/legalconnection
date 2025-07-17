using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Lawyer.Models.Profile
{
    public class BasicInformationViewModel
    {
        //

        public Guid LawyerId { get; set; }
        //
        public string CAL { get; set; }
        public List<SpecialityViewModel> Specialities { get; set; }
        //Para el Select2
        public List<Guid> SpecialityThemesId { get; set; }
        public List<SpecialityThemeViewModel> SpecialityThemes { get; set; }
        public decimal Fee { get; set; }
        public bool FreeFirst { get; set; }
        public int HiredCases { get; set; }
        public int FreeLegalCases { get; set; }
    }
}
