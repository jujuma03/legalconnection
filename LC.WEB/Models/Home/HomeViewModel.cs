
using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Models.Home
{
    public class HomeViewModel
    {
        public List<BannerViewModel> Banner{ get; set; }
        public List<LawyerTemp> TopLawyers { get; set; }
        public List<string> Specialties { get; set; }
    }
    public class BannerViewModel
    {
        public int SequenceOrderId { get; set; }
        
        public bool StatusId { get; set; }
        public bool RouteTypeId { get; set; }

        public string UrlDirection { get; set; }
        public string NameDirection { get; set; }
        public string UrlImage { get; set; }
        public string Headline { get; set; }
        public Guid Id { get;  set; }
        public string Description { get;  set; }
        public bool StatusDirectionId { get;  set; }
    }
    public class SectionItemViewModel
    {
        public string UrlImage { get; set; }
        public string Headline { get; set; }
        public string Description { get; set; }
    }
}
