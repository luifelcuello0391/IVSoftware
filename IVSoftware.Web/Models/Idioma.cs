using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVSoftware.Web.Models
{
    public class Idioma
    {
        [Key]
        public long Id { get; set; }
        public int Habla { get; set; }
        public int Lee { get; set; }
        public int Escribe { get; set; }

        public virtual Persona Persona { get; set; }
        public virtual TipoIdioma TipoIdioma { get; set; }
    }
}
