using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Lawyer.Models.Profile
{
    public class PublicationViewModel
    {
        public Guid? Id { get; set; }
        public IFormFile Photo { get; set; }
        public string PhotoUrl { get; set; }
        public string urlPhotoCropImg { get; set; }
        public string Title { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public string PublicationDate { get; set; }
    }
}
