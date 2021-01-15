using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVSoftware.Web.Data
{
    public class ChangePasswordVM
    {
        [DisplayName("Contraseña Actual")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string Password { get; set; }

        [DisplayName("Nueva Contraseña")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Error en la confirmación de la contraseña")]
        [DisplayName("Confirmar contraseña")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string CPassword { get; set; }
    }
}
