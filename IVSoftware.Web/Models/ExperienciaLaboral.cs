using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVSoftware.Web.Models
{
    public class ExperienciaLaboral
    {
        [Key]
        public long Id { get; set; }

        [DisplayName("Nombre de la empresa")]
        public string NombreEmpresa { get; set; }

        [DisplayName("Correo electrónico")]
        public string CorreoElectronico { get; set; }

        [DisplayName("Teléfono")]
        public string Telefono { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [DisplayName("Fecha de ingreso")]
        public DateTime FechaIngreso { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [DisplayName("Fecha de retiro")]
        public DateTime? FechaRetiro { get; set; }

        [DisplayName("Trabaja actualmente")]
        public bool EsActual { get; set; }

        [DisplayName("Cargo o contrato")]
        public string CargoContrato { get; set; }

        public string Dependencia { get; set; }

        [DisplayName("Dirección")]
        public string Direccion { get; set; }
        public string Responsabilidades { get; set; }

        public virtual Municipio Municipio { get; set; }
        public virtual Pais Pais { get; set; }

        [DisplayName("Tipo de empresa")]
        public virtual TipoEmpresa TipoEmpresa { get; set; }

        [DisplayName("Tipo de empresa")]
        public int TipoEmpresaId { get; set; }

        [DisplayName("Archivo adjunto")]
        public string ArchivoAdjunto { get; set; }

        public virtual Persona Persona { get; set; }
        public string PersonaId { get; set; }
    }
}
