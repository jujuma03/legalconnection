using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Models.Banner
{
    public class BannerViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "TITULAR", Prompt = "Escriba el título aquí")]
        public string Headline { get; set; }

        [Display(Name = "DESCRIPCIÓN", Prompt = "Escriba la descripción aquí")]
        public string Description { get; set; }

        [Display(Name = "FECHA DE PUBLICACIÓN")]
        public string PublicationDate { get; set; }

        [Display(Name = "MOSTAR EL BANNER")]
        public string Status { get; set; }
        [Display(Name = "TIPO DE RUTA DEL BOTÓN")]
        public string RouteType { get; set; }

        public string UrlImage { get; set; }

        [Display(Name = "ORDEN DE SECUENCIA")]
        public string SequenceOrder { get; set; }

        [Display(Name = "BOTÓN", Prompt = "www.ejemplo.com")]
        public string UrlDirection { get; set; }

        [Display(Name = "MOSTRAR BOTÓN")]
        public string StatusDirection { get; set; }

        [Display(Name = "CONTENIDO DE BOTÓN", Prompt = "Escribe el nombre del bóton (Ver más>>, Ver Detalle>>, ir a la página...)")]
        public string NameDirection { get; set; }

        [Display(Name = "IMAGEN")]
        public IFormFile Image { get; set; }

        public bool StatusId { get; set; }
        public bool RouteTypeId { get; set; }
        public bool StatusDirectionId { get; set; }
        public int SequenceOrderId { get; set; }

        [Display(Name = "ESTADO", Prompt = "ESTADO")]
        public SelectList ListStatus { set; get; }

        [Display(Name = "Estado", Prompt = "Estado")]
        public SelectList ListSequenceOrder { set; get; }
        public string urlCropImg { get; set; }
    }
}
