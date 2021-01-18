using IVSoftware.Data.Models.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace IVSoftware.Data.Models
{
    public class Person : BaseModel<Guid>
    {
        [DisplayName("Identificación")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public string IdentificationNumber { get; set; }

        [DisplayName("Primer nombre")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string FirstName { get; set; }

        [DisplayName("Segundo nombre")]
        public string MiddleName { get; set; }

        [DisplayName("Primer apellido")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string FirstLastName { get; set; }

        [DisplayName("Segundo apellido")]
        public string SecondLastName { get; set; }

        [DisplayName("Sexo")]
        public int? GenderId { get; set; }

        public virtual Gender Gender { get; set; }

        [DisplayName("Nacionalidad colombiana")]
        public bool IsColombian { get; set; }

        [DisplayName("Tipo de libreta militar")]
        public string MilitaryCardType { get; set; }

        [DisplayName("Libreta militar No.")]
        public string MilitaryCardId { get; set; }

        [DisplayName("Distrito militar")]
        public string MilitaryDistrict { get; set; }

        [DisplayName("Fecha de nacimiento")]
        public DateTime? Birthdate { get; set; }

        [DisplayName("Dirección")]
        public string Address { get; set; }

        [DisplayName("Teléfono")]
        public string PhoneNumber { get; set; }

        [DisplayName("Correo electrónico")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Email { get; set; }

        [DisplayName("Fecha de diligenciamiento")]
        public DateTime? CompletionDateTime { get; set; }

        public string Photo { get; set; }

        public int? CorrespondenceMunicipalityId { get; set; }

        public virtual Municipality CorrespondenceMunicipality { get; set; }

        public int? BirthMunicipalityId { get; set; }

        public virtual Municipality BirthMunicipality { get; set; }

        public int? CorrespondenceCountryId { get; set; }

        public virtual Country CorrespondenceCountry { get; set; }

        public int? BirthCountryId { get; set; }

        public virtual Country BirthCountry { get; set; }

        [DisplayName("Habilidades")]
        public string Skills { get; set; }

        [DisplayName("ARL")]
        public int? ArlId { get; set; }

        public virtual Arl Arl { get; set; }

        [DisplayName("EPS")]
        public int? EpsId { get; set; }

        public virtual Eps Eps { get; set; }

        [DisplayName("Tipo de identificación")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public int IdentificationTypeId { get; set; }

        public virtual IdentificationType IdentificationType { get; set; }

        [DisplayName("Tipo de sangre")]
        public int? BloodTypeId { get; set; }

        public virtual BloodType BloodType { get; set; }

        [DisplayName("Tipo de contrato")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public int ContractTypeId { get; set; }

        public virtual ContractType ContractType { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        [DisplayName("Nombre")]
        [NotMapped]
        public string FullName
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
        public virtual List<TechnicalKnowledge> TechnicalKnowledges { get; set; } = new List<TechnicalKnowledge>();

        public virtual List<BasicEducation> BasicEducations { get; set; } = new List<BasicEducation>();

        public virtual List<HigherEducation> HigherEducations { get; set; } = new List<HigherEducation>();

        public virtual List<WorkExperience> WorkExperiences { get; set; } = new List<WorkExperience>();

        public virtual List<Training> Trainings { get; set; } = new List<Training>();

        public virtual List<OtherTechnicalKnowledge> OtherTechnicalKnowledges { get; set; } = new List<OtherTechnicalKnowledge>();
        //public virtual List<PersonaInduccion> ListPersonaInduccion { get; set; }
        public virtual List<ServiceModel> Services { get; set; }

        [DisplayName("Tiempo de experiencia")]
        [NotMapped]
        public string WorkExperiencesTime {
            get
            {
                if (WorkExperiences != null && WorkExperiences.Count > 0)
                {
                    int totalDays = 0;
                    foreach (var workExperience in WorkExperiences)
                    {
                        DateTime endDate = workExperience.EndDate.HasValue ?
                            workExperience.EndDate.Value : DateTime.Today;
                        totalDays += (endDate - workExperience.StartDate).Days;
                    }

                    if (totalDays > 0)
                    {
                        var mod = totalDays % 365;
                        var years = (totalDays >= 365) ? (totalDays / 365) : 0;
                        var months = (mod > 0) ? (mod / 30) : 0;

                        return $"{years} Año(s) y {months} Mes(es)";
                    }
                }

                return "";
            }
        }

        [DisplayName("Roles o Responsabilidades")]
        public virtual ICollection<PersonJobRole> PeopleJobRole { get; set; }

        [NotMapped]
        public List<int> _roles { get; set; }
        [DisplayName("Roles o Responsabilidades")]
        [NotMapped]
        public List<int> Roles {
            get
            {
                return _roles ?? PeopleJobRole?.Select(pjr => pjr.JobRoleId).ToList();
            }

            set
            {
                _roles = value;
            }
        }

        [DisplayName("Rol")]
        [NotMapped]
        public string Role { get; set; }
    }
}