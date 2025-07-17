using Microsoft.AspNetCore.Http;
using System;

namespace LC.WEB.Areas.Admin.Models.MissionVision
{
    public class CreateMissionVisionViewModel
    {
        public IFormFile MisionImage { get; set; }
        public Guid MisionId { get; set; }
        public string MisionTitle { get; set; }
        public string MisionDescription { get; set; }

        public string VisionTitle { get; set; }
        public IFormFile VisionImage { get; set; }
        public Guid VisionId { get; set; }
        public string VisionDescription { get; set; }
    }
}
