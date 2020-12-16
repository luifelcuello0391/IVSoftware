using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Web.Models
{
    public class ConocimientoTecnico
    {
        [Key]
        public long Id { get; set; }

        [DisplayName("Análisis")]
        public string Analisis { get; set; }

        [DisplayName("Técnica analítica")]
        public string TecnicaAnalitica { get; set; }
        public string Matriz { get; set; } 
        public int Tiempo { get; set; }

        public virtual Persona Persona { get; set; }
        public string PersonaId { get; set; }
    }
}