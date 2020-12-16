using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.Models
{
    public class RespuestaEvaluacion
    {
        public int Id { get; set; }

        public virtual DetalleEvaluacion DetalleEvaluacion { get; set; }
        public int DetalleEvaluacionId { get; set; }
        public string TextoRespuesta { get; set; }

        public virtual Pregunta Pregunta { get; set; }
        public int PreguntaId { get; set; }

        public virtual Respuesta Respuesta { get; set; }
        public int? RespuestaId { get; set; }
    }
}
