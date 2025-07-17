using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Models.Plan
{
    public class Benefit
    {
        public Guid Id { get; set; }
        public bool Assigned { get; set; }
        public string Description { get; set; }
    }
}
