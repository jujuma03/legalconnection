using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class HowItWorksStep
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public byte Order { get; set; }
        public byte Status { get; set; }
        public byte Type { get; set; }//1: para cliente/ 2: para abogados
        public string UrlImage { get; set; }
    }
}
