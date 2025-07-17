using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Models.Lawyer
{
    public class LawyerObservationViewModel
    {
        public Guid LawyerId { get; set; }
        public string Observations { get; set; }
        public bool HasObservations { get; set; }
        public bool HasBeenCorrected { get; set; }
    }
}
