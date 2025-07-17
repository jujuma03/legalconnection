using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Models.Qualification
{
    public class LawyerViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string PhotoUrl { get; set; }
    }
}
