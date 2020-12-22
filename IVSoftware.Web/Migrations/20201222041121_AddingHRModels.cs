using System;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class AddingHRModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AplicacionEvaluacion_AspNetUsers_PersonaId",
                table: "AplicacionEvaluacion");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Arl_ArlId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Eps_EpsId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Municipio_MunicipioCorrespondenciaId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Municipio_MunicipioNacimientoId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Pais_PaisCorrespondenciaId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Pais_PaisNacimientoId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TipoContrato_TipoContratoId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TipoDocumento_TipoDocumentoId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TipoSangre_TipoSangreId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistResponseHeader_AspNetUsers_ValidatedById",
                table: "ChecklistResponseHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_ConocimientoTecnico_AspNetUsers_PersonaId",
                table: "ConocimientoTecnico");

            migrationBuilder.DropForeignKey(
                name: "FK_EducacionBasica_AspNetUsers_PersonaId",
                table: "EducacionBasica");

            migrationBuilder.DropForeignKey(
                name: "FK_EducacionSuperior_AspNetUsers_PersonaId",
                table: "EducacionSuperior");

            migrationBuilder.DropForeignKey(
                name: "FK_ExperienciaLaboral_AspNetUsers_PersonaId",
                table: "ExperienciaLaboral");

            migrationBuilder.DropForeignKey(
                name: "FK_Formacion_AspNetUsers_PersonaId",
                table: "Formacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Idioma_AspNetUsers_PersonaId",
                table: "Idioma");

            migrationBuilder.DropForeignKey(
                name: "FK_OtroConocimiento_AspNetUsers_PersonaId",
                table: "OtroConocimiento");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonaInduccion_AspNetUsers_PersonaId",
                table: "PersonaInduccion");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationRequest_AspNetUsers_GenerationUsedId",
                table: "QuotationRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_RolSistemaGestion_AspNetUsers_PersonaId",
                table: "RolSistemaGestion");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ArlId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EpsId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MunicipioCorrespondenciaId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MunicipioNacimientoId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PaisCorrespondenciaId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PaisNacimientoId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TipoContratoId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TipoDocumentoId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TipoSangreId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Eps");

            migrationBuilder.DropColumn(
                name: "ArlId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DistritoMilitar",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EpsId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EsColombiano",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FechaDiligenciamiento",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FechaNacimiento",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Foto",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Habilidades",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MunicipioCorrespondenciaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MunicipioNacimientoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NumeroIdentificacion",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NumeroLibretaMilitar",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PaisCorrespondenciaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PaisNacimientoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PrimerApellido",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PrimerNombre",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SegundoApellido",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SegundoNombre",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Sexo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TipoContratoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TipoDocumentoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TipoLibretaMilitar",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TipoSangreId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Arl");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "TipoSangre",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(3)",
                oldUnicode: false,
                oldMaxLength: 3);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "TipoDocumento",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "TipoDocumento",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Pais",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Municipio",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldUnicode: false,
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Municipio",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Eps",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Departamento",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Departamento",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2)",
                oldUnicode: false,
                oldMaxLength: 2);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Arl",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "BloodType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentificationType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentificationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumeroIdentificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimerNombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SegundoNombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimerApellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SegundoApellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sexo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EsColombiano = table.Column<bool>(type: "bit", nullable: true),
                    TipoLibretaMilitar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroLibretaMilitar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistritoMilitar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaDiligenciamiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MunicipioCorrespondenciaId = table.Column<int>(type: "int", nullable: true),
                    MunicipioNacimientoId = table.Column<int>(type: "int", nullable: true),
                    PaisCorrespondenciaId = table.Column<int>(type: "int", nullable: true),
                    PaisNacimientoId = table.Column<int>(type: "int", nullable: true),
                    Habilidades = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArlId = table.Column<int>(type: "int", nullable: true),
                    EpsId = table.Column<int>(type: "int", nullable: true),
                    TipoDocumentoId = table.Column<int>(type: "int", nullable: false),
                    TipoSangreId = table.Column<int>(type: "int", nullable: true),
                    TipoContratoId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persona_Arl_ArlId",
                        column: x => x.ArlId,
                        principalTable: "Arl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persona_Eps_EpsId",
                        column: x => x.EpsId,
                        principalTable: "Eps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persona_Municipio_MunicipioCorrespondenciaId",
                        column: x => x.MunicipioCorrespondenciaId,
                        principalTable: "Municipio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persona_Municipio_MunicipioNacimientoId",
                        column: x => x.MunicipioNacimientoId,
                        principalTable: "Municipio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persona_Pais_PaisCorrespondenciaId",
                        column: x => x.PaisCorrespondenciaId,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persona_Pais_PaisNacimientoId",
                        column: x => x.PaisNacimientoId,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persona_TipoContrato_TipoContratoId",
                        column: x => x.TipoContratoId,
                        principalTable: "TipoContrato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persona_TipoDocumento_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "TipoDocumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persona_TipoSangre_TipoSangreId",
                        column: x => x.TipoSangreId,
                        principalTable: "TipoSangre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Municipality",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipality", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Municipality_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdentificationNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FirstLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SecondLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GenderId = table.Column<int>(type: "int", nullable: true),
                    IsColombian = table.Column<bool>(type: "bit", nullable: false),
                    MilitaryCardType = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    MilitaryCardId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    MilitaryDistrict = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CompletionDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CorrespondenceMunicipalityId = table.Column<int>(type: "int", nullable: true),
                    BirthMunicipalityId = table.Column<int>(type: "int", nullable: true),
                    CorrespondenceCountryId = table.Column<int>(type: "int", nullable: true),
                    BirthCountryId = table.Column<int>(type: "int", nullable: true),
                    Skills = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: true),
                    ArlId = table.Column<int>(type: "int", nullable: true),
                    EpsId = table.Column<int>(type: "int", nullable: true),
                    IdentificationTypeId = table.Column<int>(type: "int", nullable: false),
                    BloodTypeId = table.Column<int>(type: "int", nullable: true),
                    ContractTypeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.UniqueConstraint("AK_Person_IdentificationNumber", x => x.IdentificationNumber);
                    table.ForeignKey(
                        name: "FK_Person_Arl_ArlId",
                        column: x => x.ArlId,
                        principalTable: "Arl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Person_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Person_BloodType_BloodTypeId",
                        column: x => x.BloodTypeId,
                        principalTable: "BloodType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Person_ContractType_ContractTypeId",
                        column: x => x.ContractTypeId,
                        principalTable: "ContractType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Person_Country_BirthCountryId",
                        column: x => x.BirthCountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Person_Country_CorrespondenceCountryId",
                        column: x => x.CorrespondenceCountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Person_Eps_EpsId",
                        column: x => x.EpsId,
                        principalTable: "Eps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Person_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Person_IdentificationType_IdentificationTypeId",
                        column: x => x.IdentificationTypeId,
                        principalTable: "IdentificationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Person_Municipality_BirthMunicipalityId",
                        column: x => x.BirthMunicipalityId,
                        principalTable: "Municipality",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Person_Municipality_CorrespondenceMunicipalityId",
                        column: x => x.CorrespondenceMunicipalityId,
                        principalTable: "Municipality",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Municipality_DepartmentId",
                table: "Municipality",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_ArlId",
                table: "Person",
                column: "ArlId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_BirthCountryId",
                table: "Person",
                column: "BirthCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_BirthMunicipalityId",
                table: "Person",
                column: "BirthMunicipalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_BloodTypeId",
                table: "Person",
                column: "BloodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_ContractTypeId",
                table: "Person",
                column: "ContractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_CorrespondenceCountryId",
                table: "Person",
                column: "CorrespondenceCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_CorrespondenceMunicipalityId",
                table: "Person",
                column: "CorrespondenceMunicipalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_EpsId",
                table: "Person",
                column: "EpsId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_GenderId",
                table: "Person",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_IdentificationTypeId",
                table: "Person",
                column: "IdentificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_UserId",
                table: "Person",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_ArlId",
                table: "Persona",
                column: "ArlId");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_EpsId",
                table: "Persona",
                column: "EpsId");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_MunicipioCorrespondenciaId",
                table: "Persona",
                column: "MunicipioCorrespondenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_MunicipioNacimientoId",
                table: "Persona",
                column: "MunicipioNacimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_PaisCorrespondenciaId",
                table: "Persona",
                column: "PaisCorrespondenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_PaisNacimientoId",
                table: "Persona",
                column: "PaisNacimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_TipoContratoId",
                table: "Persona",
                column: "TipoContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_TipoDocumentoId",
                table: "Persona",
                column: "TipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_TipoSangreId",
                table: "Persona",
                column: "TipoSangreId");

            migrationBuilder.AddForeignKey(
                name: "FK_AplicacionEvaluacion_Persona_PersonaId",
                table: "AplicacionEvaluacion",
                column: "PersonaId",
                principalTable: "Persona",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistResponseHeader_Persona_ValidatedById",
                table: "ChecklistResponseHeader",
                column: "ValidatedById",
                principalTable: "Persona",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConocimientoTecnico_Persona_PersonaId",
                table: "ConocimientoTecnico",
                column: "PersonaId",
                principalTable: "Persona",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EducacionBasica_Persona_PersonaId",
                table: "EducacionBasica",
                column: "PersonaId",
                principalTable: "Persona",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EducacionSuperior_Persona_PersonaId",
                table: "EducacionSuperior",
                column: "PersonaId",
                principalTable: "Persona",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExperienciaLaboral_Persona_PersonaId",
                table: "ExperienciaLaboral",
                column: "PersonaId",
                principalTable: "Persona",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Formacion_Persona_PersonaId",
                table: "Formacion",
                column: "PersonaId",
                principalTable: "Persona",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Idioma_Persona_PersonaId",
                table: "Idioma",
                column: "PersonaId",
                principalTable: "Persona",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OtroConocimiento_Persona_PersonaId",
                table: "OtroConocimiento",
                column: "PersonaId",
                principalTable: "Persona",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonaInduccion_Persona_PersonaId",
                table: "PersonaInduccion",
                column: "PersonaId",
                principalTable: "Persona",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationRequest_Persona_GenerationUsedId",
                table: "QuotationRequest",
                column: "GenerationUsedId",
                principalTable: "Persona",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RolSistemaGestion_Persona_PersonaId",
                table: "RolSistemaGestion",
                column: "PersonaId",
                principalTable: "Persona",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            var sqlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"InitialScripts\InitialScriptData.sql");
            migrationBuilder.Sql(File.ReadAllText(sqlFile, Encoding.GetEncoding(28591)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AplicacionEvaluacion_Persona_PersonaId",
                table: "AplicacionEvaluacion");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistResponseHeader_Persona_ValidatedById",
                table: "ChecklistResponseHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_ConocimientoTecnico_Persona_PersonaId",
                table: "ConocimientoTecnico");

            migrationBuilder.DropForeignKey(
                name: "FK_EducacionBasica_Persona_PersonaId",
                table: "EducacionBasica");

            migrationBuilder.DropForeignKey(
                name: "FK_EducacionSuperior_Persona_PersonaId",
                table: "EducacionSuperior");

            migrationBuilder.DropForeignKey(
                name: "FK_ExperienciaLaboral_Persona_PersonaId",
                table: "ExperienciaLaboral");

            migrationBuilder.DropForeignKey(
                name: "FK_Formacion_Persona_PersonaId",
                table: "Formacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Idioma_Persona_PersonaId",
                table: "Idioma");

            migrationBuilder.DropForeignKey(
                name: "FK_OtroConocimiento_Persona_PersonaId",
                table: "OtroConocimiento");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonaInduccion_Persona_PersonaId",
                table: "PersonaInduccion");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationRequest_Persona_GenerationUsedId",
                table: "QuotationRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_RolSistemaGestion_Persona_PersonaId",
                table: "RolSistemaGestion");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Persona");

            migrationBuilder.DropTable(
                name: "BloodType");

            migrationBuilder.DropTable(
                name: "ContractType");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "IdentificationType");

            migrationBuilder.DropTable(
                name: "Municipality");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Eps");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Arl");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "TipoSangre",
                type: "varchar(3)",
                unicode: false,
                maxLength: 3,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "TipoDocumento",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "TipoDocumento",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Pais",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Municipio",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Municipio",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Eps",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Departamento",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Departamento",
                type: "varchar(2)",
                unicode: false,
                maxLength: 2,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ArlId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "AspNetUsers",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DistritoMilitar",
                table: "AspNetUsers",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EpsId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EsColombiano",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaDiligenciamiento",
                table: "AspNetUsers",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaNacimiento",
                table: "AspNetUsers",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "AspNetUsers",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Habilidades",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MunicipioCorrespondenciaId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MunicipioNacimientoId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroIdentificacion",
                table: "AspNetUsers",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NumeroLibretaMilitar",
                table: "AspNetUsers",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaisCorrespondenciaId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaisNacimientoId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimerApellido",
                table: "AspNetUsers",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimerNombre",
                table: "AspNetUsers",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SegundoApellido",
                table: "AspNetUsers",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SegundoNombre",
                table: "AspNetUsers",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sexo",
                table: "AspNetUsers",
                type: "varchar(1)",
                unicode: false,
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "AspNetUsers",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoContratoId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoDocumentoId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TipoLibretaMilitar",
                table: "AspNetUsers",
                type: "varchar(1)",
                unicode: false,
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoSangreId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Arl",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.AddForeignKey(
                name: "FK_AplicacionEvaluacion_AspNetUsers_PersonaId",
                table: "AplicacionEvaluacion",
                column: "PersonaId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Arl_ArlId",
                table: "AspNetUsers",
                column: "ArlId",
                principalTable: "Arl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Eps_EpsId",
                table: "AspNetUsers",
                column: "EpsId",
                principalTable: "Eps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Municipio_MunicipioCorrespondenciaId",
                table: "AspNetUsers",
                column: "MunicipioCorrespondenciaId",
                principalTable: "Municipio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Municipio_MunicipioNacimientoId",
                table: "AspNetUsers",
                column: "MunicipioNacimientoId",
                principalTable: "Municipio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Pais_PaisCorrespondenciaId",
                table: "AspNetUsers",
                column: "PaisCorrespondenciaId",
                principalTable: "Pais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Pais_PaisNacimientoId",
                table: "AspNetUsers",
                column: "PaisNacimientoId",
                principalTable: "Pais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TipoContrato_TipoContratoId",
                table: "AspNetUsers",
                column: "TipoContratoId",
                principalTable: "TipoContrato",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TipoDocumento_TipoDocumentoId",
                table: "AspNetUsers",
                column: "TipoDocumentoId",
                principalTable: "TipoDocumento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TipoSangre_TipoSangreId",
                table: "AspNetUsers",
                column: "TipoSangreId",
                principalTable: "TipoSangre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistResponseHeader_AspNetUsers_ValidatedById",
                table: "ChecklistResponseHeader",
                column: "ValidatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConocimientoTecnico_AspNetUsers_PersonaId",
                table: "ConocimientoTecnico",
                column: "PersonaId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EducacionBasica_AspNetUsers_PersonaId",
                table: "EducacionBasica",
                column: "PersonaId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EducacionSuperior_AspNetUsers_PersonaId",
                table: "EducacionSuperior",
                column: "PersonaId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExperienciaLaboral_AspNetUsers_PersonaId",
                table: "ExperienciaLaboral",
                column: "PersonaId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Formacion_AspNetUsers_PersonaId",
                table: "Formacion",
                column: "PersonaId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Idioma_AspNetUsers_PersonaId",
                table: "Idioma",
                column: "PersonaId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OtroConocimiento_AspNetUsers_PersonaId",
                table: "OtroConocimiento",
                column: "PersonaId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonaInduccion_AspNetUsers_PersonaId",
                table: "PersonaInduccion",
                column: "PersonaId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationRequest_AspNetUsers_GenerationUsedId",
                table: "QuotationRequest",
                column: "GenerationUsedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RolSistemaGestion_AspNetUsers_PersonaId",
                table: "RolSistemaGestion",
                column: "PersonaId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
