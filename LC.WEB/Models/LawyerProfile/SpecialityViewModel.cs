using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Models.LawyerProfile
{
    public class SpecialityViewModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public List<SpecialityThemeViewModel> Themes { get; set; }
    }
}
