using IVSoftware.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.Data
{
    public class PersonaViewModel : Persona
    {
        [DisplayName("Tipo de documento")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string TipoDocumentoNombre { get; set; }
    }
}
