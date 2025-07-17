using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Models.Regiser
{
    public class UserRegisterViewModel
    {
        [Display(Name = "Apellidos", Prompt = "Ingresar Apellidos")]
        public string Surnames { get; set; }
        [Display(Name = "Correo Electrónico", Prompt = "Ingresar Correo Electrónico")]
        public string Email { get; set; }
        [Display(Name = "Documento", Prompt = "Documento")]
        public string Dni { get; set; }
        [Display(Name = "Tipo de documento", Prompt = "Tipo de documento")]
        public byte DocumentType { get; set; }
        [Display(Name = "Contraseña", Prompt = "Ingresar Contraseña")]
        public string Password { get; set; }
        [Display(Name = "Confirmar Contraseña", Prompt = "Confirmar Contraseña")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Celular", Prompt = "Ingresar Celular")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Departamento", Prompt = "Seleccionar Departamento")]
        public Guid DepartmentId { get; set; }
        [Display(Name = "Provincia", Prompt = "Seleccionar Provincia")]
        public Guid ProvinceId { get; set; }
        [Display(Name = "Distrito", Prompt = "Seleccionar Distrito")]
        public Guid DistrictId { get; set; }
        [Display(Name = "Terminos y Condiciones")]
        public bool TermsAndConditions { get; set; }
    }
    public class LawyerViewModel : UserRegisterViewModel
    {
        public int MaxSpecialities { get; set; }
        public int MaxThemesBySpeciality { get; set; }
        [Display(Name = "Nombre", Prompt = "Ingresar Nombre")]
        public string NameOrOffice { get; set; }
        public List<SpecialityViewModel> SpecialitiesData { get; set; }
        public List<Guid> Specialities { get; set; }
        public List<Guid> SpecialityThemes { get; set; }
    }
    public class SpecialityViewModel
    {
        public Guid Id { get; set; }
        public bool Selected { get; set; }
        public string Specialty { get; set; }
    }
}
