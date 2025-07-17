using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Models.SectionItems
{
    public class SectionItemsViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "ESTADO", Prompt = "ESTADO")]
        public SelectList ListStatus { get; set; }

        [Display(Name = "TITULAR", Prompt = "Escriba el título aquí")]
        public string Headline { get; set; }
        [Display(Name = "Estado", Prompt = "Estado")]
        public SelectList ListSequenceOrder { set; get; }
        [Display(Name = "MOSTAR EL ELEMENTO")]
        public bool Status { get; set; }

        [Display(Name = "Sección", Prompt = "Sección")]
        public SelectList ListSection { set; get; }

        public string Description { get; set; }
        public string UrlCropImg { get; set; }
        public string UrlImage { get; set; }
        public byte Type { get; set; }
        public IFormFile Image { get; set; }
    }
}
