using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Models.LawyerProfile
{
    public class PublicationViewModel
    {
        public string Title { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public string PublicationDate { get; set; }
        public string PhotoUrl { get; set; }
    }
}
