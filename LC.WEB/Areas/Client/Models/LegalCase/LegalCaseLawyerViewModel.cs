using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Client.Models.LegalCase
{
    public class LegalCaseLawyerViewModel
    {
        public Guid LawyerId { get; set; }
        public bool FreeFirst { get; set; }
        public string FullName { get; set; }
        public string Specialities { get; set; }
        public List<string> SpecialityThemes { get; set; }
        public string Description { get; set; }
        public int ResponseTime { get; set; }
        public decimal Fee { get; set; }
        public bool IsDirected { get; set; }
        public byte Status { get; set; }
        public string LastConnection { get; set; }
        public string CreatedAt { get; set; }
        public int HiredCases { get; set; }
        public string PhotoUrl { get; set; }
        public int Qualification { get; set; }
        public bool IsFree { get; set; }
    }
}
