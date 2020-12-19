using IVSoftware.Data.Models.Core;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVSoftware.Data.Models
{
    public class Person : BaseModel<Guid>
    {
        public string IdentificationNumber { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string FirstLastName { get; set; }

        public string SecondLastName { get; set; }

        public int GenderId { get; set; }

        public virtual Gender Gender { get; set; }

        public bool IsColombian { get; set; }

        public string MilitaryCardType { get; set; }

        public string MilitaryCardId { get; set; }

        public string MilitaryDistrict { get; set; }

        public DateTime? Birthdate { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public DateTime? CompletionDateTime { get; set; }

        public string Photo { get; set; }

        public int CorrespondenceMunicipalityId { get; set; }

        public virtual Municipality CorrespondenceMunicipality { get; set; }

        public int BirthMunicipalityId { get; set; }

        public virtual Municipality BirthMunicipality { get; set; }

        public int CorrespondenceCountryId { get; set; }

        public virtual Country CorrespondenceCountry { get; set; }

        public int BirthCountryId { get; set; }

        public virtual Country BirthCountry { get; set; }

        public string Skills { get; set; }

        public int? ArlId { get; set; }

        public virtual Arl Arl { get; set; }

        public int? EpsId { get; set; }

        public virtual Eps Eps { get; set; }

        public int IdentificationTypeId { get; set; }

        public virtual IdentificationType IdentificationType { get; set; }

        public int? BloodTypeId { get; set; }

        public virtual BloodType BloodType { get; set; }

        public int ContractTypeId { get; set; }

        public virtual ContractType ContractType { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        [NotMapped]
        public string NombreCompleto
        {
            get
            {
                return FirstName +
                        (MiddleName == null ? "" : " " + MiddleName) +
                        (FirstLastName == null ? "" : " " + FirstLastName) +
                        (SecondLastName == null ? "" : " " + SecondLastName);
            }
        }

        //public virtual List<AplicacionEvaluacion> ListAplicacionEvaluacion { get; set; }
        //public virtual List<ConocimientoTecnico> ListConocimientoTecnico { get; set; }
        //public virtual List<EducacionBasica> ListEducacionBasica { get; set; }
        //public virtual List<EducacionSuperior> ListEducacionSuperior { get; set; }
        //public virtual List<ExperienciaLaboral> ListExperienciaLaboral { get; set; }
        //public virtual List<Formacion> ListFormacion { get; set; }
        //public virtual List<OtroConocimiento> ListOtroConocimiento { get; set; }
        //public virtual List<PersonaInduccion> ListPersonaInduccion { get; set; }
    }
}