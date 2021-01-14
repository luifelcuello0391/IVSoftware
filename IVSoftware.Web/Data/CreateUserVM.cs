using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Web.Data
{
    public class CreateUserVM
    {
        public Guid Id { get; set; }

        [DisplayName("Correo electrónico")]
        [EmailAddress(ErrorMessage = "El campo no tiene un formato de E-mail válido")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string Email { get; set; }

        [DisplayName("Contraseña")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Error en la confirmación de la contraseña")]
        [DisplayName("Confirmar contraseña")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string CPassword { get; set; }

        [DisplayName("Rol")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string Rol { get; set; }
    }
}