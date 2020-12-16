using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVSoftware.Web.Models
{
    public class OtroConocimiento
    {
        [Key]
        public long Id { get; set; }
        public string Nombre { get; set; }
        public int Tiempo { get; set; }

        public virtual Persona Persona { get; set; }
        public string PersonaId { get; set; }
    }
}
