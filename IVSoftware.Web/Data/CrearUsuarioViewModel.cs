using IVSoftware.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.Data
{
    public class CrearUsuarioViewModel
    {
        [DisplayName("Tipo de documento")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string  TipoDocumento { get; set; }

        [DisplayName("Número de identificación")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string NumeroIdentificacion { get; set; }

        [DisplayName("Primer nombre")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string PrimerNombre { get; set; }

        [DisplayName("Segundo nombre")]
        public string SegundoNombre { get; set; }

        [DisplayName("Primer apellido")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string PrimerApellido { get; set; }

        [DisplayName("Segundo apellido")]
        public string SegundoApellido { get; set; }

        [DisplayName("Correo electrónico")]
        [EmailAddress(ErrorMessage = "El campo no tiene un formato de E-mail válido")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string CorreoElectronico { get; set; }

        [DisplayName("Contraseña")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Error en la confirmación de la contraseña")]
        [DisplayName("Confirmar contraseña")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string Confirm { get; set; }

        [DisplayName("Tipo de contrato")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string TipoContrato { get; set; }

        public IList<RolSistemaGestion> SelectedTiposRolesGestion;
        public IList<TipoRolGestion> AvailableTiposRolesGestion;

        public CrearUsuarioViewModel()
        {
            SelectedTiposRolesGestion = new List<RolSistemaGestion>();
            AvailableTiposRolesGestion = new List<TipoRolGestion>();
        }
    }
}
