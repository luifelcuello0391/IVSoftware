using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Web.Data
{
    public class LoginViewModel
    {
        [DisplayName("Correo electrónico")]
        [EmailAddress(ErrorMessage = "El campo no tiene un formato de E-mail válido")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string UserName { get; set; }

        [DisplayName("Contraseña")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string Password { get; set; }
    }
}
