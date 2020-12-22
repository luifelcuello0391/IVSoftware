using IVSoftware.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVSoftware.Web.Models
{
    public class Persona : IdentityUser
    {
        [DisplayName("Identificación")]
        public string NumeroIdentificacion { get; set; }

        [DisplayName("Primer nombre")]
        public string PrimerNombre { get; set; }

        [DisplayName("Segundo nombre")]
        public string SegundoNombre { get; set; }

        [DisplayName("Primer apellido")]
        public string PrimerApellido { get; set; }

        [DisplayName("Segundo apellido")]
        public string SegundoApellido { get; set; }

        [DisplayName("Sexo")]
        public string Sexo { get; set; }

        [DisplayName("Nacionalidad colombiana")]
        public bool? EsColombiano { get; set; }

        [DisplayName("Tipo de libreta militar")]
        public string TipoLibretaMilitar { get; set; }

        [DisplayName("Libreta militar No.")]
        public string NumeroLibretaMilitar { get; set; }

        [DisplayName("Distrito militar")]
        public string DistritoMilitar { get; set; }

        [DisplayName("Fecha de nacimiento")]
        public DateTime? FechaNacimiento { get; set; }

        [DisplayName("Dirección")]
        public string Direccion { get; set; }

        [DisplayName("Teléfono")]
        public string Telefono { get; set; }

        [DisplayName("Fecha de diligenciamiento")]
        public DateTime? FechaDiligenciamiento { get; set; }

        [DisplayName("Correo electrónico")]
        public override string Email { get; set; }

        public string Foto { get; set; }
        public virtual Municipio MunicipioCorrespondencia { get; set; }
        public virtual Municipio MunicipioNacimiento { get; set; }
        public virtual Pais PaisCorrespondencia { get; set; }
        public virtual Pais PaisNacimiento { get; set; }
        public string Habilidades { get; set; }

        [DisplayName("ARL")]
        public virtual Arl Arl { get; set; }
        [DisplayName("ARL")]
        public int? ArlId { get; set; }

        [DisplayName("EPS")]
        public virtual Eps Eps { get; set; }
        [DisplayName("EPS")]
        public int? EpsId { get; set; }

        [DisplayName("Tipo de identificación")]
        public virtual TipoDocumento TipoDocumento { get; set; }
        [DisplayName("Tipo de identificación")]
        public int TipoDocumentoId { get; set; }     

        [DisplayName("Tipo de sangre")]
        public virtual TipoSangre TipoSangre { get; set; }
        [DisplayName("Tipo de sangre")]
        public int? TipoSangreId { get; set; }

        [DisplayName("Tipo de contrato")]
        public virtual TipoContrato TipoContrato { get; set; }
        [DisplayName("Tipo de contrato")]
        public int TipoContratoId { get; set; }

        [DisplayName("Nombre")]
        [NotMapped]
        public string NombreCompleto
        {
            get
            {
                return PrimerNombre +
                        (SegundoNombre == null ? "" : " " + SegundoNombre) +
                        (PrimerApellido == null ? "" : " " + PrimerApellido) +
                        (SegundoApellido == null ? "" : " " + SegundoApellido);
            }
        }

        public virtual List<AplicacionEvaluacion> ListAplicacionEvaluacion { get; set; }
        public virtual List<ConocimientoTecnico> ListConocimientoTecnico { get; set; }
        public virtual List<EducacionBasica> ListEducacionBasica { get; set; }
        public virtual List<EducacionSuperior> ListEducacionSuperior{ get; set; }
        public virtual List<ExperienciaLaboral> ListExperienciaLaboral { get; set; }
        public virtual List<Formacion> ListFormacion { get; set; }
        public virtual List<OtroConocimiento> ListOtroConocimiento { get; set; }
        public virtual List<PersonaInduccion> ListPersonaInduccion { get; set; }
    }
}