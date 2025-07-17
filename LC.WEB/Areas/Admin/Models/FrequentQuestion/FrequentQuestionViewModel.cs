using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Models.FrequentQuestion
{
    public class FrequentQuestionViewModel
    {
        //public byte  Type { get; set; }
        public Guid Id { get; set; }

        [Display(Name = "ESTADO", Prompt = "ESTADO")]
        public SelectList ListStatus { get;  set; }
        [Display(Name = "TIPOS", Prompt = "TIPOS")]
        public SelectList ListTypes { get; set; }
        public SelectList ListIcons { get; set; }

        [Display(Name = "MOSTAR")]
        public bool Status { get; set; }
        public byte Type { get; set; }

        public string Headline { get; set; }
        public string Description { get; set; }
        public int Icon { get; set; }
    }
}
