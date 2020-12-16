using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.Models
{
    public class Respuesta
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public Boolean RespuestaCorrecta { get; set; }
        public int Orden { get; set; }

        public virtual Pregunta Pregunta { get; set; }
        public int PreguntaId { get; set; }
    }
}
