using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVSoftware.Web.Models
{
    public class Pregunta
    {
        [Key]
        public int Id { get; set; }
        public int TipoPregunta { get; set; }
        public string Enunciado { get; set; }
        public int Orden { get; set; }

        public virtual Evaluacion Evaluacion { get; set; }
        public int EvaluacionId { get; set; }

        [DisplayName("Tipo de pregunta")]
        [NotMapped]
        public string DescripcionTipoPregunta
        {
            get
            {
                string Descripcion = "";

                switch(TipoPregunta)
                {
                    case (int)TIPOPREGUNTA.SELECCION_UNICA:
                        Descripcion = "Selección única";
                        break;
                    case (int)TIPOPREGUNTA.SELECCION_MULTIPLE:
                        Descripcion = "Selección múltiple";
                        break;
                    case (int)TIPOPREGUNTA.PREGUNTA_ABIERTA:
                        Descripcion = "Pregunta abierta";
                        break;
                };

                return Descripcion;
            }
        }

        public virtual List<Respuesta> ListRespuesta { get; set; }
    }
}
