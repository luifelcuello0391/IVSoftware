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

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder
        //.UseLazyLoadingProxies();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);

            // AplicacionEvaluacion **************************************************************************************************************
            modelBuilder.Entity<AplicacionEvaluacion>(entity =>
            {
                entity.Property(e => e.PersonaId)
                    .IsRequired();
            });

            // Arl **************************************************************************************************************
            new ArlConfiguration("Arl", "Id").Map(modelBuilder);

            // ConocimientoTecnico **************************************************************************************************************

            modelBuilder.Entity<ConocimientoTecnico>(entity =>
            {
                entity.Property(e => e.Analisis)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Matriz)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PersonaId)
                    .IsRequired();

                entity.Property(e => e.TecnicaAnalitica)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            // Departamento **************************************************************************************************************
            new DepartmentConfiguration("Department", "Id").Map(modelBuilder);

            // EducacionBasica **************************************************************************************************************
            new BasicEducationConfiguration("BasicEducation", "Id").Map(modelBuilder);

            // EducacionSuperior **************************************************************************************************************
            new HigherEducationConfiguration("HigherEducation", "Id").Map(modelBuilder);

            // Eps **************************************************************************************************************
            new EpsConfiguration("Eps", "Id").Map(modelBuilder);

            // ExperienciaLaboral **************************************************************************************************************

            modelBuilder.Entity<ExperienciaLaboral>(entity =>
            {
                entity.Property(e => e.CargoContrato)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Dependencia)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaIngreso).HasColumnType("datetime");

                entity.Property(e => e.FechaRetiro).HasColumnType("datetime");

                entity.Property(e => e.NombreEmpresa)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PersonaId)
                    .IsRequired();

                entity.Property(e => e.Responsabilidades)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            // Formacion **************************************************************************************************************

            modelBuilder.Entity<Formacion>(entity =>
            {
                entity.Property(e => e.Entidad)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaFinalizacion).HasColumnType("datetime");

                entity.Property(e => e.NombreCurso)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PersonaId)
                    .IsRequired();

                entity.Property(e => e.Tema)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            // ModalidadAcademica **************************************************************************************************************
            new AcademicLevelConfiguration("AcademicLevel", "Id").Map(modelBuilder);

            // Municipio **************************************************************************************************************
            new MunicipalityConfiguration("Municipality", "Id").Map(modelBuilder);

            // OtroConocimiento **************************************************************************************************************

            modelBuilder.Entity<OtroConocimiento>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PersonaId)
                    .IsRequired();
            });

            // Pais **************************************************************************************************************
            new CountryConfiguration("Country", "Id").Map(modelBuilder);

            // Persona **************************************************************************************************************
            new PersonConfiguration("Person", "Id").Map(modelBuilder);

            // PersonaInduccion **************************************************************************************************************

            modelBuilder.Entity<PersonaInduccion>(entity =>
            {
                entity.Property(e => e.PersonaId)
                    .IsRequired();
            });

            // TipoDocumento **************************************************************************************************************
            new IdentificationTypeConfiguration("IdentificationType", "Id").Map(modelBuilder);

            // TipoIdioma **************************************************************************************************************

            modelBuilder.Entity<TipoIdioma>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            // TipoRolGestion **************************************************************************************************************

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

            // Gender
            new GenderConfiguration("Gender", "Id").Map(modelBuilder);

            // Contract Type
            new ContractTypeConfiguration("ContractType", "Id").Map(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<IVSoftware.Models.ClientModel> ClientModel { get; set; }

        public DbSet<IVSoftware.Models.ClientTypeModel> ClientTypeModel { get; set; }

        public DbSet<IVSoftware.Models.MatrixModel> MatrixModel { get; set; }

        public DbSet<IVSoftware.Models.MeasurementUnitModel> MeasurementUnitModel { get; set; }

        public DbSet<IVSoftware.Models.QuotationRequest> QuotationRequest { get; set; }

        public DbSet<IVSoftware.Models.QuotationModel> QuotationModel { get; set; }

        public DbSet<IVSoftware.Models.ReferenceMethodCondition> ReferenceMethodCondition { get; set; }

        public DbSet<IVSoftware.Models.ReferenceMethodModel> ReferenceMethodModel { get; set; }

        public DbSet<IVSoftware.Models.ServiceModel> ServiceModel { get; set; }

        public DbSet<IVSoftware.Models.VariableModel> VariableModel { get; set; }

        public DbSet<IVSoftware.Models.WorkingRangeModel> WorkingRangeModel { get; set; }

        public DbSet<IVSoftware.Web.Models.TypeOfService> TypeOfService { get; set; }

        public DbSet<IVSoftware.Web.Models.ServiceGroupModel> ServiceGroupModel { get; set; }

        public DbSet<IVSoftware.Web.Models.QuotationStatus> QuotationStatus { get; set; }

        public DbSet<IVSoftware.Web.Models.Brand> Brand { get; set; }

        public DbSet<IVSoftware.Web.Models.Provider> Provider { get; set; }

        public DbSet<IVSoftware.Web.Models.Equipment> Equipment { get; set; }

        public DbSet<IVSoftware.Web.Models.CheckListQuestion> CheckListQuestion { get; set; }

        public DbSet<IVSoftware.Web.Models.CheckListSection> CheckListSection { get; set; }

        public DbSet<IVSoftware.Web.Models.EquipmentCheckList> EquipmentCheckList { get; set; }

        public DbSet<IVSoftware.Web.Models.ChecklistResponseHeader> ChecklistResponseHeader { get; set; }

        public DbSet<IVSoftware.Web.Models.IncentiveModel> IncentiveModel { get; set; }

        public DbSet<IVSoftware.Web.Models.ClientContact> ClientContact { get; set; }

        public DbSet<IVSoftware.Web.Models.ServiceGroupServicesRelation> ServiceGroupServicesRelation { get; set; }

    }
}