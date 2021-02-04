using IVSoftware.Data.Models.Core;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Data.Models
{
    public class NotificationSetting : BaseModel<int>
    {
        [DisplayName("Correo electrónico")]
        [EmailAddress(ErrorMessage = "El campo no tiene un formato de E-mail válido")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string Email { get; set; }

        [DisplayName("Contraseña")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string Password { get; set; }
    }
}