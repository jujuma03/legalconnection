using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Models.Blog
{
    public class LawyerPublicationViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public string PublicationDate { get; set; }
        public string PhotoUrl { get; set; }

        //Lawyer
        public string Lawyer { get; set; }
        public string LawyerPhotoUrl { get; set; }
        public bool IsInBlog { get; set; }
    }
}
