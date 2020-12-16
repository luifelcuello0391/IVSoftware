using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Arl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientTypeModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    RegisterStatus = table.Column<int>(nullable: false),
                    CreationDatetime = table.Column<DateTime>(nullable: false),
                    ModificationDatetime = table.Column<DateTime>(nullable: true),
                    MustPayServices = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientTypeModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Codigo = table.Column<string>(unicode: false, maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Eps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Evaluacion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatrixModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    OnlyForServices = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatrixModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeasurementUnitModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    RegisterStatus = table.Column<int>(nullable: false),
                    CreationDatetime = table.Column<DateTime>(nullable: false),
                    ModificationDatetime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementUnitModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModalidadAcademica",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModalidadAcademica", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pais",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuotationModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    RegisterStatus = table.Column<int>(nullable: false),
                    CreationDatetime = table.Column<DateTime>(nullable: false),
                    ModificationDatetime = table.Column<DateTime>(nullable: true),
                    ClientId = table.Column<int>(nullable: false),
                    ClientName = table.Column<string>(nullable: true),
                    ClientAddress = table.Column<string>(nullable: true),
                    ClientPhoneNumber = table.Column<string>(nullable: true),
                    ClientContactName = table.Column<string>(nullable: true),
                    ClientContactPosition = table.Column<string>(nullable: true),
                    Greetings = table.Column<string>(nullable: true),
                    Exceptions = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    ReservationDate = table.Column<DateTime>(nullable: false),
                    ReservationMessage = table.Column<string>(nullable: true),
                    MeasurementSubtotal = table.Column<float>(nullable: false),
                    MeasurementTotalValue = table.Column<float>(nullable: false),
                    ServicesSubtotal = table.Column<float>(nullable: false),
                    ServicesTotalValue = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuotationRequest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    RegisterStatus = table.Column<int>(nullable: false),
                    CreationDatetime = table.Column<DateTime>(nullable: false),
                    ModificationDatetime = table.Column<DateTime>(nullable: true),
                    ClientRegisterId = table.Column<int>(nullable: false),
                    ClientName = table.Column<string>(nullable: true),
                    HasBeenGenerated = table.Column<bool>(nullable: false),
                    LastGenerationDate = table.Column<DateTime>(nullable: true),
                    UserIdThatGenerated = table.Column<int>(nullable: false),
                    UserNameThatGenerated = table.Column<string>(nullable: true),
                    ClientRequestDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReferenceMethodCondition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    RegisterStatus = table.Column<int>(nullable: false),
                    CreationDatetime = table.Column<DateTime>(nullable: false),
                    ModificationDatetime = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    QuotationCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceMethodCondition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoCertificacion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCertificacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoContrato",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoContrato", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoDocumento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(maxLength: 3, nullable: false),
                    Nombre = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDocumento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoEmpresa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEmpresa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoIdioma",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoIdioma", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoInduccion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoInduccion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoRolGestion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoRolGestion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoSangre",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(unicode: false, maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoSangre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfService",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    RegisterStatus = table.Column<int>(nullable: false),
                    CreationDatetime = table.Column<DateTime>(nullable: false),
                    ModificationDatetime = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfService", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VariableModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariableModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkingRangeModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    RegisterStatus = table.Column<int>(nullable: false),
                    CreationDatetime = table.Column<DateTime>(nullable: false),
                    ModificationDatetime = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Precondition = table.Column<string>(nullable: true),
                    MinimumValue = table.Column<float>(nullable: false),
                    MaximumValue = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingRangeModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    RegisterStatus = table.Column<int>(nullable: false),
                    CreationDatetime = table.Column<DateTime>(nullable: false),
                    ModificationDatetime = table.Column<DateTime>(nullable: true),
                    DocumentType = table.Column<int>(nullable: false),
                    Identification = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    ContactName = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    ClientTypeId = table.Column<int>(nullable: true),
                    IsRegular = table.Column<bool>(nullable: false),
                    LastServiceExecutionDatetime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientModel_ClientTypeModel_ClientTypeId",
                        column: x => x.ClientTypeId,
                        principalTable: "ClientTypeModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Municipio",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    Codigo = table.Column<string>(unicode: false, maxLength: 5, nullable: false),
                    DepartamentoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Municipio_Departamento_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pregunta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoPregunta = table.Column<int>(nullable: false),
                    Enunciado = table.Column<string>(nullable: true),
                    Orden = table.Column<int>(nullable: false),
                    EvaluacionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pregunta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pregunta_Evaluacion_EvaluacionId",
                        column: x => x.EvaluacionId,
                        principalTable: "Evaluacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReferenceMethodModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    MeasurementUncertaintyLowerValue = table.Column<float>(nullable: false),
                    MeasurementUncertaintyUpperValue = table.Column<float>(nullable: false),
                    IsPercentage = table.Column<bool>(nullable: false),
                    HasUncertainty = table.Column<bool>(nullable: false),
                    MeasurementUnitId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceMethodModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReferenceMethodModel_MeasurementUnitModel_MeasurementUnitId",
                        column: x => x.MeasurementUnitId,
                        principalTable: "MeasurementUnitModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    RegisterStatus = table.Column<int>(nullable: false),
                    CreationDatetime = table.Column<DateTime>(nullable: false),
                    ModificationDatetime = table.Column<DateTime>(nullable: true),
                    Code = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    UnitValue = table.Column<float>(nullable: false),
                    ServiceTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceModel_TypeOfService_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "TypeOfService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    NumeroIdentificacion = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    PrimerNombre = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    SegundoNombre = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    PrimerApellido = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    SegundoApellido = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    Sexo = table.Column<string>(unicode: false, maxLength: 1, nullable: true),
                    EsColombiano = table.Column<bool>(nullable: true),
                    TipoLibretaMilitar = table.Column<string>(unicode: false, maxLength: 1, nullable: true),
                    NumeroLibretaMilitar = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    DistritoMilitar = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime", nullable: true),
                    Direccion = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Telefono = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    FechaDiligenciamiento = table.Column<DateTime>(type: "datetime", nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    Foto = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    MunicipioCorrespondenciaId = table.Column<int>(nullable: true),
                    MunicipioNacimientoId = table.Column<int>(nullable: true),
                    PaisCorrespondenciaId = table.Column<int>(nullable: true),
                    PaisNacimientoId = table.Column<int>(nullable: true),
                    Habilidades = table.Column<string>(nullable: true),
                    ArlId = table.Column<int>(nullable: true),
                    EpsId = table.Column<int>(nullable: true),
                    TipoDocumentoId = table.Column<int>(nullable: false),
                    TipoSangreId = table.Column<int>(nullable: true),
                    TipoContratoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Arl_ArlId",
                        column: x => x.ArlId,
                        principalTable: "Arl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Eps_EpsId",
                        column: x => x.EpsId,
                        principalTable: "Eps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Municipio_MunicipioCorrespondenciaId",
                        column: x => x.MunicipioCorrespondenciaId,
                        principalTable: "Municipio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Municipio_MunicipioNacimientoId",
                        column: x => x.MunicipioNacimientoId,
                        principalTable: "Municipio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Pais_PaisCorrespondenciaId",
                        column: x => x.PaisCorrespondenciaId,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Pais_PaisNacimientoId",
                        column: x => x.PaisNacimientoId,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_TipoContrato_TipoContratoId",
                        column: x => x.TipoContratoId,
                        principalTable: "TipoContrato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_TipoDocumento_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "TipoDocumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_TipoSangre_TipoSangreId",
                        column: x => x.TipoSangreId,
                        principalTable: "TipoSangre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Respuesta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Texto = table.Column<string>(nullable: true),
                    RespuestaCorrecta = table.Column<bool>(nullable: false),
                    Orden = table.Column<int>(nullable: false),
                    PreguntaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respuesta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Respuesta_Pregunta_PreguntaId",
                        column: x => x.PreguntaId,
                        principalTable: "Pregunta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AplicacionEvaluacion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaAplicacion = table.Column<DateTime>(nullable: false),
                    PersonaId = table.Column<string>(nullable: false),
                    EvaluacionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AplicacionEvaluacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AplicacionEvaluacion_Evaluacion_EvaluacionId",
                        column: x => x.EvaluacionId,
                        principalTable: "Evaluacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AplicacionEvaluacion_AspNetUsers_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConocimientoTecnico",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Analisis = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    TecnicaAnalitica = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Matriz = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Tiempo = table.Column<int>(nullable: false),
                    PersonaId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConocimientoTecnico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConocimientoTecnico_AspNetUsers_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EducacionBasica",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreInstitucion = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    UltimoGradoAprobado = table.Column<int>(nullable: false),
                    TituloObtenido = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    FechaGrado = table.Column<DateTime>(type: "datetime", nullable: false),
                    PersonaId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducacionBasica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducacionBasica_AspNetUsers_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EducacionSuperior",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreInstitucion = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    SemestresAprobados = table.Column<int>(nullable: false),
                    EsGraduado = table.Column<bool>(nullable: false),
                    FechaGrado = table.Column<DateTime>(type: "datetime", nullable: true),
                    NombreEstudios = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    NumeroTarjetaProfesional = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    ModalidadAcademicaId = table.Column<int>(nullable: false),
                    ArchivoAdjunto = table.Column<string>(nullable: true),
                    PersonaId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducacionSuperior", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducacionSuperior_ModalidadAcademica_ModalidadAcademicaId",
                        column: x => x.ModalidadAcademicaId,
                        principalTable: "ModalidadAcademica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EducacionSuperior_AspNetUsers_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExperienciaLaboral",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEmpresa = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    CorreoElectronico = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Telefono = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaRetiro = table.Column<DateTime>(type: "datetime", nullable: true),
                    EsActual = table.Column<bool>(nullable: false),
                    CargoContrato = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Dependencia = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Direccion = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Responsabilidades = table.Column<string>(type: "text", nullable: false),
                    MunicipioId = table.Column<int>(nullable: true),
                    PaisId = table.Column<int>(nullable: true),
                    TipoEmpresaId = table.Column<int>(nullable: false),
                    ArchivoAdjunto = table.Column<string>(nullable: true),
                    PersonaId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienciaLaboral", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperienciaLaboral_Municipio_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Municipio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExperienciaLaboral_Pais_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExperienciaLaboral_AspNetUsers_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExperienciaLaboral_TipoEmpresa_TipoEmpresaId",
                        column: x => x.TipoEmpresaId,
                        principalTable: "TipoEmpresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Formacion",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCurso = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Tema = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    TipoCertificacionId = table.Column<int>(nullable: false),
                    Entidad = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    FechaFinalizacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    NumeroHoras = table.Column<int>(nullable: false),
                    ArchivoAdjunto = table.Column<string>(nullable: true),
                    PersonaId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Formacion_AspNetUsers_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Formacion_TipoCertificacion_TipoCertificacionId",
                        column: x => x.TipoCertificacionId,
                        principalTable: "TipoCertificacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Idioma",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Habla = table.Column<int>(nullable: false),
                    Lee = table.Column<int>(nullable: false),
                    Escribe = table.Column<int>(nullable: false),
                    PersonaId = table.Column<string>(nullable: true),
                    TipoIdiomaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idioma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Idioma_AspNetUsers_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Idioma_TipoIdioma_TipoIdiomaId",
                        column: x => x.TipoIdiomaId,
                        principalTable: "TipoIdioma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OtroConocimiento",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Tiempo = table.Column<int>(nullable: false),
                    PersonaId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtroConocimiento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtroConocimiento_AspNetUsers_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonaInduccion",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaInduccion = table.Column<DateTime>(nullable: false),
                    PersonaId = table.Column<string>(nullable: false),
                    TipoInduccionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaInduccion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonaInduccion_AspNetUsers_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonaInduccion_TipoInduccion_TipoInduccionId",
                        column: x => x.TipoInduccionId,
                        principalTable: "TipoInduccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolSistemaGestion",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IndicadorAutorizacion = table.Column<int>(nullable: false),
                    PersonaId = table.Column<string>(nullable: true),
                    TipoRolGestionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolSistemaGestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolSistemaGestion_AspNetUsers_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolSistemaGestion_TipoRolGestion_TipoRolGestionId",
                        column: x => x.TipoRolGestionId,
                        principalTable: "TipoRolGestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetalleEvaluacion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AplicacionEvaluacionId = table.Column<int>(nullable: false),
                    PreguntaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleEvaluacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleEvaluacion_AplicacionEvaluacion_AplicacionEvaluacionId",
                        column: x => x.AplicacionEvaluacionId,
                        principalTable: "AplicacionEvaluacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetalleEvaluacion_Respuesta_PreguntaId",
                        column: x => x.PreguntaId,
                        principalTable: "Respuesta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RespuestaEvaluacion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DetalleEvaluacionId = table.Column<int>(nullable: false),
                    TextoRespuesta = table.Column<string>(nullable: true),
                    PreguntaId = table.Column<int>(nullable: false),
                    RespuestaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespuestaEvaluacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RespuestaEvaluacion_DetalleEvaluacion_DetalleEvaluacionId",
                        column: x => x.DetalleEvaluacionId,
                        principalTable: "DetalleEvaluacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RespuestaEvaluacion_Pregunta_PreguntaId",
                        column: x => x.PreguntaId,
                        principalTable: "Pregunta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RespuestaEvaluacion_Respuesta_RespuestaId",
                        column: x => x.RespuestaId,
                        principalTable: "Respuesta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AplicacionEvaluacion_EvaluacionId",
                table: "AplicacionEvaluacion",
                column: "EvaluacionId");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacionEvaluacion_PersonaId",
                table: "AplicacionEvaluacion",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ArlId",
                table: "AspNetUsers",
                column: "ArlId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EpsId",
                table: "AspNetUsers",
                column: "EpsId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MunicipioCorrespondenciaId",
                table: "AspNetUsers",
                column: "MunicipioCorrespondenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MunicipioNacimientoId",
                table: "AspNetUsers",
                column: "MunicipioNacimientoId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PaisCorrespondenciaId",
                table: "AspNetUsers",
                column: "PaisCorrespondenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PaisNacimientoId",
                table: "AspNetUsers",
                column: "PaisNacimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TipoContratoId",
                table: "AspNetUsers",
                column: "TipoContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TipoDocumentoId",
                table: "AspNetUsers",
                column: "TipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TipoSangreId",
                table: "AspNetUsers",
                column: "TipoSangreId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientModel_ClientTypeId",
                table: "ClientModel",
                column: "ClientTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ConocimientoTecnico_PersonaId",
                table: "ConocimientoTecnico",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleEvaluacion_AplicacionEvaluacionId",
                table: "DetalleEvaluacion",
                column: "AplicacionEvaluacionId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleEvaluacion_PreguntaId",
                table: "DetalleEvaluacion",
                column: "PreguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_EducacionBasica_PersonaId",
                table: "EducacionBasica",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_EducacionSuperior_ModalidadAcademicaId",
                table: "EducacionSuperior",
                column: "ModalidadAcademicaId");

            migrationBuilder.CreateIndex(
                name: "IX_EducacionSuperior_PersonaId",
                table: "EducacionSuperior",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienciaLaboral_MunicipioId",
                table: "ExperienciaLaboral",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienciaLaboral_PaisId",
                table: "ExperienciaLaboral",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienciaLaboral_PersonaId",
                table: "ExperienciaLaboral",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienciaLaboral_TipoEmpresaId",
                table: "ExperienciaLaboral",
                column: "TipoEmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Formacion_PersonaId",
                table: "Formacion",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Formacion_TipoCertificacionId",
                table: "Formacion",
                column: "TipoCertificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Idioma_PersonaId",
                table: "Idioma",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Idioma_TipoIdiomaId",
                table: "Idioma",
                column: "TipoIdiomaId");

            migrationBuilder.CreateIndex(
                name: "IX_Municipio_DepartamentoId",
                table: "Municipio",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_OtroConocimiento_PersonaId",
                table: "OtroConocimiento",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaInduccion_PersonaId",
                table: "PersonaInduccion",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaInduccion_TipoInduccionId",
                table: "PersonaInduccion",
                column: "TipoInduccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Pregunta_EvaluacionId",
                table: "Pregunta",
                column: "EvaluacionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferenceMethodModel_MeasurementUnitId",
                table: "ReferenceMethodModel",
                column: "MeasurementUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Respuesta_PreguntaId",
                table: "Respuesta",
                column: "PreguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestaEvaluacion_DetalleEvaluacionId",
                table: "RespuestaEvaluacion",
                column: "DetalleEvaluacionId");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestaEvaluacion_PreguntaId",
                table: "RespuestaEvaluacion",
                column: "PreguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestaEvaluacion_RespuestaId",
                table: "RespuestaEvaluacion",
                column: "RespuestaId");

            migrationBuilder.CreateIndex(
                name: "IX_RolSistemaGestion_PersonaId",
                table: "RolSistemaGestion",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_RolSistemaGestion_TipoRolGestionId",
                table: "RolSistemaGestion",
                column: "TipoRolGestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceModel_ServiceTypeId",
                table: "ServiceModel",
                column: "ServiceTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ClientModel");

            migrationBuilder.DropTable(
                name: "ConocimientoTecnico");

            migrationBuilder.DropTable(
                name: "EducacionBasica");

            migrationBuilder.DropTable(
                name: "EducacionSuperior");

            migrationBuilder.DropTable(
                name: "ExperienciaLaboral");

            migrationBuilder.DropTable(
                name: "Formacion");

            migrationBuilder.DropTable(
                name: "Idioma");

            migrationBuilder.DropTable(
                name: "MatrixModel");

            migrationBuilder.DropTable(
                name: "OtroConocimiento");

            migrationBuilder.DropTable(
                name: "PersonaInduccion");

            migrationBuilder.DropTable(
                name: "QuotationModel");

            migrationBuilder.DropTable(
                name: "QuotationRequest");

            migrationBuilder.DropTable(
                name: "ReferenceMethodCondition");

            migrationBuilder.DropTable(
                name: "ReferenceMethodModel");

            migrationBuilder.DropTable(
                name: "RespuestaEvaluacion");

            migrationBuilder.DropTable(
                name: "RolSistemaGestion");

            migrationBuilder.DropTable(
                name: "ServiceModel");

            migrationBuilder.DropTable(
                name: "VariableModel");

            migrationBuilder.DropTable(
                name: "WorkingRangeModel");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ClientTypeModel");

            migrationBuilder.DropTable(
                name: "ModalidadAcademica");

            migrationBuilder.DropTable(
                name: "TipoEmpresa");

            migrationBuilder.DropTable(
                name: "TipoCertificacion");

            migrationBuilder.DropTable(
                name: "TipoIdioma");

            migrationBuilder.DropTable(
                name: "TipoInduccion");

            migrationBuilder.DropTable(
                name: "MeasurementUnitModel");

            migrationBuilder.DropTable(
                name: "DetalleEvaluacion");

            migrationBuilder.DropTable(
                name: "TipoRolGestion");

            migrationBuilder.DropTable(
                name: "TypeOfService");

            migrationBuilder.DropTable(
                name: "AplicacionEvaluacion");

            migrationBuilder.DropTable(
                name: "Respuesta");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Pregunta");

            migrationBuilder.DropTable(
                name: "Arl");

            migrationBuilder.DropTable(
                name: "Eps");

            migrationBuilder.DropTable(
                name: "Municipio");

            migrationBuilder.DropTable(
                name: "Pais");

            migrationBuilder.DropTable(
                name: "TipoContrato");

            migrationBuilder.DropTable(
                name: "TipoDocumento");

            migrationBuilder.DropTable(
                name: "TipoSangre");

            migrationBuilder.DropTable(
                name: "Evaluacion");

            migrationBuilder.DropTable(
                name: "Departamento");
        }
    }
}
