using IVSoftware.Web.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace IVSoftware.Web.Data
{
    public class SeedDb
    {
        private readonly IVSoftwareContext context;
        private readonly UserManager<Persona> userManager;

        public SeedDb(IVSoftwareContext context, UserManager<Persona> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task SeedAsync()
        {
            try
            {
                var persona = await this.userManager.FindByEmailAsync("admin@fkconsultores.com");

                if (persona == null)
                {
                    persona = new Persona
                    {
                        Email = "admin@fkconsultores.com",
                        UserName = "admin@fkconsultores.com",
                        PrimerNombre = "FK",
                        SegundoNombre = string.Empty,
                        PrimerApellido = "Consultores",
                        SegundoApellido = string.Empty,
                        Sexo = "M",
                        EsColombiano = true,
                        Direccion = "No hay datos",
                        Telefono = "Sin datos",
                        FechaDiligenciamiento = DateTime.Today,
                        NumeroIdentificacion = "8055",
                        TipoDocumentoId = 1,
                        TipoContratoId = 1
                    };

                    var result = await this.userManager.CreateAsync(persona, ClsCipher.Encrypt("adminfk8055", ClsCipher.PasswordKey));
                    if (result != IdentityResult.Success)
                    {
                        throw new InvalidOperationException("No se pudo crear el usuario en el Seed.");
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
