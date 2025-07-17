using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Models.Portal
{
    public class BlogDetailViewModel
    {
        public Guid LawyerId { get; set; }
        public string PhotoUrl { get; set; }
        public string LawyerPictureUrl { get; set; }
        public string LawyerFullName { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Description { get; set; }
        public string Disctrict { get; set; }
        public string AboutLawyer { get; set; }
        public string Department { get; set; }
        public string Title { get; set; }
        public string Topic { get; set; }
        public string SpecialtyName { get;  set; }
        public List<string>Themes { get;  set; }
        public int Qualification { get;  set; }
        public int Type { get;  set; }
    }
}
