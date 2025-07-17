using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Lawyer.Models.Profile
{
    public class LanguageViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LevelName { get; set; }
        public byte Level { get; set; }
        public Guid LanguageId { get; set; }
    }
}
