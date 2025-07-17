using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Models.HowItWorks
{
    public class HowItWorksViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "TITULAR", Prompt = "Escriba el título aquí")]
        public string Headline { get; set; }

        [Display(Name = "CONTENIDO", Prompt = "Escriba el contenido aquí")]
        public string Content { get; set; }

        [Display(Name = "MOSTAR")]
        public string Status { get; set; }

        public string UrlImage { get; set; }

        [Display(Name = "ORDEN DE SECUENCIA")]
        public string SequenceOrder { get; set; }
        [Display(Name = "TIPO")]
        public string Type { get; set; }

        [Display(Name = "IMAGEN")]
        public IFormFile Image { get; set; }

        public bool StatusId { get; set; }

        [Display(Name = "ESTADO", Prompt = "ESTADO")]
        public SelectList ListStatus { set; get; }

        [Display(Name = "ORDEN", Prompt = "ORDEN")]
        public SelectList ListSequenceOrder { set; get; }
    }
    public class CreateHowItWorksViewModel
    {
        public IFormFile Image { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string UrlImage { get; set; }
        public bool Status { get; set; }
        public byte Type { get; set; }
        public byte Order { get; set; }
        public string UrlCropImg { get; set; }
    }
}
