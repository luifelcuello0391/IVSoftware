using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.Models
{
    public class DetalleEvaluacion
    {
        public int Id { get; set; }

        public virtual AplicacionEvaluacion AplicacionEvaluacion { get; set; }
        public int AplicacionEvaluacionId { get; set; }

        public virtual Respuesta Pregunta { get; set; }
        public int PreguntaId { get; set; }
    }
}