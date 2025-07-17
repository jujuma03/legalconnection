using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class LawyerInterview
    {
        public Guid Id { get; set; }
        public Lawyer Lawyer { get; set; }
        public Guid LawyerId { get; set; }
        public bool Selected { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartRange { get; set; }
        public TimeSpan EndRange { get; set; }
    }
}
