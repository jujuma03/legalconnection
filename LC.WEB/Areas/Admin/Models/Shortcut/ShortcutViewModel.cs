using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Models.Shortcut
{
    public class ShortcutViewModel
    {
        [Display(Name = "ESTADO", Prompt = "ESTADO")]
        public SelectList ListStatus { get;  set; }
        [Display(Name = "TIPOS", Prompt = "TIPOS")]
        public SelectList ListTypes { get;  set; }
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public byte Type { get;  set; }
        public string UrlDirection { get;  set; }
        public string Title { get; set; }
    }
}
