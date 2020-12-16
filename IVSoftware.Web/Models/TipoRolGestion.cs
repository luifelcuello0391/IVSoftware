using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVSoftware.Web.Models
{
    public class TipoRolGestion
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
