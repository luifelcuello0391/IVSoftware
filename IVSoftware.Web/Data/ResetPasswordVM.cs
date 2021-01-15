using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Web.Data
{
    public class ResetPasswordVM
    {
        [Required(ErrorMessage = "El campo es requerido")]
        public Guid Id { get; set; }

        [DisplayName("Contraseña")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Error en la confirmación de la contraseña")]
        [DisplayName("Confirmar contraseña")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string CPassword { get; set; }
    }
}