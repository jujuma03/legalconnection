using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Models.Regiser
{
    public class ClientViewModel : UserRegisterViewModel
    {
        [Display(Name = "Nombre", Prompt = "Ingresar Nombre")]
        public string Name { get; set; }
    }
}
