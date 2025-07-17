using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace LC.WEB.Areas.Admin.Models.MissionVision
{
    public class MissionVisionViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "TÍTULO", Prompt = "Escriba el título aquí")]
        public string Title { get; set; }

        [Display(Name = "DESCRIPCIÓN", Prompt = "Escriba la descripción aquí")]
        public string Content { get; set; }
        
        public string UrlImage { get; set; }
        public byte Type { get; set; }

        [Display(Name = "Imagen", Prompt = "Imagen")]
        public IFormFile Image { get; set; }
        public string urlCropImg { get; set; }
    }
}
