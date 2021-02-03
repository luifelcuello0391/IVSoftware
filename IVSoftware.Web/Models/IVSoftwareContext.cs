using IVSoftware.Data.Configurations;
using IVSoftware.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IVSoftware.Web.Models
{
    public partial class IVSoftwareContext : IdentityDbContext<User>
    {
        public IVSoftwareContext()
        {
        }

        public IVSoftwareContext(DbContextOptions<IVSoftwareContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AplicacionEvaluacion> AplicacionEvaluacion { get; set; }
        public virtual DbSet<Arl> Arl { get; set; }
        public virtual DbSet<ConocimientoTecnico> ConocimientoTecnico { get; set; }
        public virtual DbSet<Departamento> Departamento { get; set; }
        public virtual DbSet<DetalleEvaluacion> DetalleEvaluacion { get; set; }
        public virtual DbSet<EducacionBasica> EducacionBasica { get; set; }
        public virtual DbSet<EducacionSuperior> EducacionSuperior { get; set; }
        public virtual DbSet<Eps> Eps { get; set; }
        public virtual DbSet<Evaluacion> Evaluacion { get; set; }
        public virtual DbSet<ExperienciaLaboral> ExperienciaLaboral { get; set; }
        public virtual DbSet<Formacion> Formacion { get; set; }
        public virtual DbSet<Idioma> Idioma { get; set; }
        public virtual DbSet<ModalidadAcademica> ModalidadAcademica { get; set; }
        public virtual DbSet<Municipio> Municipio { get; set; }
        public virtual DbSet<OtroConocimiento> OtroConocimiento { get; set; }
        public virtual DbSet<Pais> Pais { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Pregunta> Pregunta { get; set; }
        public virtual DbSet<PersonaInduccion> PersonaInduccion { get; set; }
        public virtual DbSet<Respuesta> Respuesta { get; set; }
        public virtual DbSet<RespuestaEvaluacion> RespuestaEvaluacion { get; set; }
        public virtual DbSet<RolSistemaGestion> RolSistemaGestion { get; set; }
        public virtual DbSet<TipoCertificacion> TipoCertificacion { get; set; }
        public virtual DbSet<TipoContrato> TipoContrato { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumento { get; set; }
        public virtual DbSet<TipoEmpresa> TipoEmpresa { get; set; }
        public virtual DbSet<TipoIdioma> TipoIdioma { get; set; }
        public virtual DbSet<TipoInduccion> TipoInduccion { get; set; }
        public virtual DbSet<TipoRolGestion> TipoRolGestion { get; set; }
        public virtual DbSet<TipoSangre> TipoSangre { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder
        .UseLazyLoadingProxies();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);

            // AplicacionEvaluacion ****************************************************************************************************
            modelBuilder.Entity<AplicacionEvaluacion>(entity =>
            {
                entity.Property(e => e.PersonaId)
                    .IsRequired();
            });

            // Arl *********************************************************************************************************************
            new ArlConfiguration("Arl", "Id").Map(modelBuilder);

            // ConocimientoTecnico *****************************************************************************************************
            new TechnicalKnowledgeConfiguration("TechnicalKnowledge", "Id").Map(modelBuilder);

            // Departamento ************************************************************************************************************
            new DepartmentConfiguration("Department", "Id").Map(modelBuilder);

            // EducacionBasica *********************************************************************************************************
            new BasicEducationConfiguration("BasicEducation", "Id").Map(modelBuilder);

            // EducacionSuperior *******************************************************************************************************
            new HigherEducationConfiguration("HigherEducation", "Id").Map(modelBuilder);

            // Eps *********************************************************************************************************************
            new EpsConfiguration("Eps", "Id").Map(modelBuilder);

            // ExperienciaLaboral ******************************************************************************************************
            new WorkExperienceConfiguration("WorkExperience", "Id").Map(modelBuilder);

            // Formacion ***************************************************************************************************************
            new TrainingConfiguration("Training", "Id").Map(modelBuilder);
            new CertificationTypeConfiguration("CertificationType", "Id").Map(modelBuilder);

            // ModalidadAcademica ******************************************************************************************************
            new AcademicLevelConfiguration("AcademicLevel", "Id").Map(modelBuilder);

            // Municipio ***************************************************************************************************************
            new MunicipalityConfiguration("Municipality", "Id").Map(modelBuilder);

            // OtroConocimiento ********************************************************************************************************
            new OtherTechnicalKnowledgeConfiguration("OtherTechnicalKnowledge", "Id").Map(modelBuilder);

            // Pais ********************************************************************************************************************
            new CountryConfiguration("Country", "Id").Map(modelBuilder);

            // Persona *****************************************************************************************************************
            new PersonConfiguration("Person", "Id").Map(modelBuilder);

            // PersonaInduccion ********************************************************************************************************

            modelBuilder.Entity<PersonaInduccion>(entity =>
            {
                entity.Property(e => e.PersonaId)
                    .IsRequired();
            });

            // TipoDocumento ***********************************************************************************************************
            new IdentificationTypeConfiguration("IdentificationType", "Id").Map(modelBuilder);

            // TipoIdioma **************************************************************************************************************

            modelBuilder.Entity<TipoIdioma>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            // TipoRolGestion **********************************************************************************************************

            modelBuilder.Entity<TipoRolGestion>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            // TipoSangre **************************************************************************************************************
            new BloodTypeConfiguration("BloodType", "Id").Map(modelBuilder);

            // CheckListQuestionSection Relationship ***********************************************************************************
            modelBuilder.Entity<CheckListQuestionCheckListSection>()
                .HasOne(um => um.CheckListQuestion).WithMany(g => g.QuestionSection)
                .HasForeignKey(um => um.QuestionsId)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<CheckListQuestionCheckListSection>()
                .HasOne(um => um.CheckListSection).WithMany(g => g.QuestionSections)
                .HasForeignKey(um => um.SectionsId)
                .OnDelete(DeleteBehavior.ClientCascade);

            // Service - Person relationship *******************************************************************************************
            modelBuilder.Entity<ServiceModel>()
                .HasOne(p => p.Responsable)
                .WithMany(s => s.Services)
                .HasForeignKey(p => p.PersonId)
                .OnDelete(DeleteBehavior.SetNull);

            // Maintenance - Provider relationship *************************************************************************************
            modelBuilder.Entity<MaintenanceModel>()
                .HasOne(m => m.ServiceProvider)
                .WithMany()
                .HasForeignKey(m => m.ProviderId)
                .OnDelete(DeleteBehavior.SetNull);

            //Maintenance - Equipment relationship**************************************************************************************
            modelBuilder.Entity<MaintenanceModel>()
                .HasOne(m => m.Equip)
                .WithMany(e => e.MaintenancesForEquipment)
                .HasForeignKey(m => m.EquipId)
                .OnDelete(DeleteBehavior.Cascade);

            // ChecklistResponseHeader - ChecklistDetail relationship ******************************************************************
            modelBuilder.Entity<CheckListResponseDetail>()
                .HasOne(d => d.Header)
                .WithMany(h => h.Details)
                .HasForeignKey(d => d.HeaderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Maintenance - Person relationship****************************************************************************************
            modelBuilder.Entity<MaintenanceModel>()
                .HasOne(m => m.Responsable)
                .WithMany()
                .HasForeignKey(m => m.PersonId)
                .OnDelete(DeleteBehavior.SetNull);

            // Air resource monitoring equipment maintenance - Equipment relationship***************************************************
            modelBuilder.Entity<AirResourceMonitoringDeviceMaintenance>()
                .HasOne(m => m.Equipment)
                .WithMany(e => e.MaintenancesForAirResourceMonitoringDevice)
                .HasForeignKey(m => m.EquipId)
                .OnDelete(DeleteBehavior.Cascade);

            // Air resource monitoring equipment maintenance - Person relationship****************************************************************************************
            modelBuilder.Entity<AirResourceMonitoringDeviceMaintenance>()
                .HasOne(m => m.Responsable)
                .WithMany()
                .HasForeignKey(m => m.PersonId)
                .OnDelete(DeleteBehavior.SetNull);

            // Gender
            new GenderConfiguration("Gender", "Id").Map(modelBuilder);

            // Contract Type
            new ContractTypeConfiguration("ContractType", "Id").Map(modelBuilder);

            // JobRol
            new JobRoleConfiguration("JobRole", "Id").Map(modelBuilder);
            new PersonJobRoleConfiguration().Map(modelBuilder.Entity<PersonJobRole>());

            // Evaluation
            new PeriodicityConfiguration("Periodicity", "Id").Map(modelBuilder);
            new EvaluationConfiguration("Evaluation", "Id").Map(modelBuilder);
            new PersonEvaluationConfiguration("PersonEvaluations", "Id").Map(modelBuilder);
            new EvaluationQuestionBankConfiguration("EvaluationQuestionBank", "Id").Map(modelBuilder);
            new EvaluationQuestionAnswerConfiguration("EvaluationQuestionAnswer", "Id").Map(modelBuilder);
            new QuestionEvaluationConfiguration().Map(modelBuilder.Entity<QuestionEvaluation>());

            // NotificationSetting
            new NotificationSettingConfiguration("NotificationSetting", "Id").Map(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<IVSoftware.Data.Models.ClientModel> ClientModel { get; set; }

        public DbSet<IVSoftware.Data.Models.ClientTypeModel> ClientTypeModel { get; set; }

        public DbSet<IVSoftware.Data.Models.MatrixModel> MatrixModel { get; set; }

        public DbSet<IVSoftware.Data.Models.MeasurementUnitModel> MeasurementUnitModel { get; set; }

        public DbSet<IVSoftware.Data.Models.QuotationRequest> QuotationRequest { get; set; }

        public DbSet<IVSoftware.Data.Models.QuotationModel> QuotationModel { get; set; }

        public DbSet<IVSoftware.Data.Models.ReferenceMethodCondition> ReferenceMethodCondition { get; set; }

        public DbSet<IVSoftware.Data.Models.ReferenceMethodModel> ReferenceMethodModel { get; set; }

        public DbSet<IVSoftware.Data.Models.ServiceModel> ServiceModel { get; set; }

        public DbSet<IVSoftware.Data.Models.VariableModel> VariableModel { get; set; }

        public DbSet<IVSoftware.Data.Models.WorkingRangeModel> WorkingRangeModel { get; set; }

        public DbSet<IVSoftware.Data.Models.TypeOfService> TypeOfService { get; set; }

        public DbSet<IVSoftware.Data.Models.ServiceGroupModel> ServiceGroupModel { get; set; }

        public DbSet<IVSoftware.Data.Models.QuotationStatus> QuotationStatus { get; set; }

        public DbSet<IVSoftware.Data.Models.Brand> Brand { get; set; }

        public DbSet<IVSoftware.Data.Models.Provider> Provider { get; set; }

        public DbSet<IVSoftware.Data.Models.Equipment> Equipment { get; set; }

        public DbSet<IVSoftware.Data.Models.CheckListQuestion> CheckListQuestion { get; set; }

        public DbSet<IVSoftware.Data.Models.CheckListSection> CheckListSection { get; set; }

        public DbSet<IVSoftware.Data.Models.EquipmentCheckList> EquipmentCheckList { get; set; }

        public DbSet<IVSoftware.Data.Models.ChecklistResponseHeader> ChecklistResponseHeader { get; set; }

        public DbSet<IVSoftware.Data.Models.IncentiveModel> IncentiveModel { get; set; }

        public DbSet<IVSoftware.Data.Models.ClientContact> ClientContact { get; set; }

        public DbSet<IVSoftware.Data.Models.ServiceGroupServicesRelation> ServiceGroupServicesRelation { get; set; }

        public DbSet<PersonJobRole> PersonJobRoles { get; set; }

        public DbSet<PersonEvaluation> PersonEvaluations { get; set; }

        public DbSet<MaintenanceModel> Maintenances { get; set; }

        public DbSet<QuestionEvaluation> QuestionEvaluations { get; set; }

        public DbSet<AirResourceMonitoringDeviceMaintenance> AirResourceMonitoringDevideMaintenances { get; set; }
    }
}