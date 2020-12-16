using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Web.Models
{
    public class ModalidadAcademica
    {       
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual List<EducacionSuperior> ListEducacionSuperior { get; set; }
    }
}
