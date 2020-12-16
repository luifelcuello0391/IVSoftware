using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Web.Models
{
    public class RolSistemaGestion
    {
        [Key]
        public long Id { get; set; }
        public int IndicadorAutorizacion { get; set; }

        public virtual Persona Persona { get; set; }
        public virtual TipoRolGestion TipoRolGestion { get; set; }
    }
}