using IVSoftware.Data.Models;
using IVSoftware.Web.Models;
using IVSoftware.Web.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace IVSoftware.Web.Data
{
    public class SeedDb
    {
        private readonly IVSoftwareContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEntityService<Person, Guid> _personService;

        public SeedDb(IVSoftwareContext context,
            UserManager<User> userManager,
            IEntityService<Person, Guid> personService,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _personService = personService;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            try
            {
                _context.Database.Migrate();
                await CreateRoles();
                var user = await _userManager.FindByEmailAsync("admin@fkconsultores.com");

                if (user == null)
                {
                    user = new User
                    {
                        Email = "admin@fkconsultores.com",
                        UserName = "admin@fkconsultores.com"
                    };

                    var result = await _userManager.CreateAsync(user, ClsCipher.Encrypt("adminfk8055", ClsCipher.PasswordKey));
                    if (result != IdentityResult.Success)
                    {
                        throw new InvalidOperationException("No se pudo crear el usuario en el Seed.");
                    }
                    else
                    {
                        var userCreated = await _userManager.FindByEmailAsync("admin@fkconsultores.com");
                        var person = new Person
                        {
                            Email = "admin@fkconsultores.com",
                            FirstName = "FK",
                            FirstLastName = "Consultores",
                            GenderId = 1,
                            IsColombian = true,
                            Address = "No hay datos",
                            PhoneNumber = "Sin datos",
                            CompletionDateTime = DateTime.Today,
                            IdentificationNumber = "8055",
                            IdentificationTypeId = 1,
                            ContractTypeId = 1,
                            UserId = userCreated != null ? userCreated.Id : Guid.NewGuid().ToString()
                        };

                        var personCreated = await _personService.CreateAsync(person);
                        if (personCreated == null)
                        {
                            throw new InvalidOperationException("No se pudo crear la persona en el Seed.");
                        }
                    }
                }

                if(!await _userManager.IsInRoleAsync(user, "Administrador"))
                {
                    IdentityResult result = await _userManager.AddToRoleAsync(user, "Administrador");
                    if(result != IdentityResult.Success)
                    {
                        throw new InvalidOperationException("No se pudo asignar el usuario en el rol administrador en el Seed.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task CreateRoles()
        {
            if (!await _roleManager.RoleExistsAsync("Administrador"))
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Administrador"
                };

                await _roleManager.CreateAsync(role);
            }

            if (!await _roleManager.RoleExistsAsync("Funcionario"))
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Funcionario"
                };

                await _roleManager.CreateAsync(role);
            }
        }
    }
}