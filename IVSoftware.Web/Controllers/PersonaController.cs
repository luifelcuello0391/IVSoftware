using IVSoftware.Web.Data;
using IVSoftware.Web.Helpers;
using IVSoftware.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace IVSoftware.Web.Controllers
{
    public class PersonaController : Controller
    {
        private readonly IUserHelper userHelper;
        private readonly IVSoftwareContext context;
        private readonly Microsoft.Extensions.Hosting.IHostEnvironment env;

        public PersonaController(IUserHelper userHelper, IVSoftwareContext context, Microsoft.Extensions.Hosting.IHostEnvironment env)
        {
            this.userHelper = userHelper;
            this.context = context;
            this.env = env;
        }

        public async Task<IActionResult> Index()
        {
            return View(await context.Persona.OrderBy(p => p.NumeroIdentificacion).ToListAsync());
        }

        public IActionResult Registrar()
        {
            var items = new List<SelectListItem>();
            items = context.TipoDocumento.Select(t => new SelectListItem()
            {
                Text = t.Nombre,
                Value = t.Id.ToString()
            }).ToList();
            ViewBag.TiposDocumento = items;

            var itemsContrato = new List<SelectListItem>();
            itemsContrato = context.TipoContrato.Select(t => new SelectListItem()
            {
                Text = t.Nombre,
                Value = t.Id.ToString()
            }).ToList();
            ViewBag.TiposContrato = itemsContrato;

            var opcionesAutorizacion = new List<SelectListItem>();

            var model = new CrearUsuarioViewModel
            {
                AvailableTiposRolesGestion = context.TipoRolGestion.ToList()
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(CrearUsuarioViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var persona = await this.userHelper.GetUserByEmailAsync(model.CorreoElectronico);
                if (persona == null)
                {
                    //persona = new Persona
                    //{
                    //    TipoDocumento = context.TipoDocumento.FirstOrDefault(t => t.Id.ToString() == model.TipoDocumento),
                    //    NumeroIdentificacion = model.NumeroIdentificacion,
                    //    PrimerNombre = model.PrimerNombre,
                    //    SegundoNombre = model.SegundoNombre,
                    //    PrimerApellido = model.PrimerApellido,
                    //    SegundoApellido = model.SegundoApellido,
                    //    Email = model.CorreoElectronico,
                    //    UserName = model.CorreoElectronico,
                    //    Sexo = "M",
                    //    TipoContratoId = context.TipoContrato.FirstOrDefault(t => t.Id.ToString() == model.TipoContrato).Id,
                    //};

                    var result = await this.userHelper.AdduserAsync(persona, model.Password);
                    if (result != IdentityResult.Success)
                    {
                        this.ModelState.AddModelError(string.Empty, "Error al crear el usuario.");
                        return this.View(model);
                    }

                    foreach (TipoRolGestion item in GetTiposRolGestion())
                    {
                        var rolSistemaGestion = new RolSistemaGestion
                        {
                            IndicadorAutorizacion = Convert.ToInt32(Request.Form[item.Id.ToString()]),
                            //Persona = persona,
                            TipoRolGestion = item
                        };

                        context.Add(rolSistemaGestion);
                    }

                    await context.SaveChangesAsync();

                    return this.RedirectToAction("Index", "Persona");
                }

                this.ModelState.AddModelError(string.Empty, "El usuario ya existe.");
            }

            return this.View(model);
        }

        public async Task<IActionResult> EditarPerfil(string userName)
        {
            if (userName == null)
                userName = this.User.Identity.Name;

            var persona = await context.Persona
                .Include(p => p.TipoDocumento)
                .Include(p => p.Arl)
                .Include(p => p.Eps)
                .Include(p => p.TipoContrato)
                .Include(p => p.ListEducacionBasica)
                .Include(p => p.ListEducacionSuperior)
                .Include(p => p.ListEducacionBasica)
                .Include(p => p.ListFormacion).ThenInclude(c => c.TipoCertificacion)
                .Include(p => p.ListExperienciaLaboral).ThenInclude(c => c.TipoEmpresa)
                .Include(p => p.ListConocimientoTecnico)
                .Include(p => p.ListOtroConocimiento)
                .Include(p => p.ListPersonaInduccion).ThenInclude(c => c.TipoInduccion)
                .FirstOrDefaultAsync(p => p.Email == userName);

            var itemsTipoDocumento = new List<SelectListItem>();
            itemsTipoDocumento = context.TipoDocumento.Select(t => new SelectListItem()
            {
                Text = t.Nombre,
                Value = t.Id.ToString()
            }).ToList();
            ViewBag.TiposDocumento = itemsTipoDocumento;

            var itemsARL = new List<SelectListItem>();
            itemsARL = context.Arl.Select(t => new SelectListItem()
            {
                Text = t.Name,
                Value = t.Id.ToString()
            }).ToList();
            ViewBag.ARLs = itemsARL;

            var itemsEPS = new List<SelectListItem>();
            itemsEPS = context.Eps.Select(t => new SelectListItem()
            {
                Text = t.Name,
                Value = t.Id.ToString()
            }).ToList();
            ViewBag.EPSs = itemsEPS;

            var itemsTipoSangre = new List<SelectListItem>();
            itemsTipoSangre = context.TipoSangre.Select(t => new SelectListItem()
            {
                Text = t.Nombre,
                Value = t.Id.ToString()
            }).ToList();
            ViewBag.TiposSangre = itemsTipoSangre;

            var itemsContrato = new List<SelectListItem>();
            itemsContrato = context.TipoContrato.Select(t => new SelectListItem()
            {
                Text = t.Nombre,
                Value = t.Id.ToString()
            }).ToList();
            ViewBag.TiposContrato = itemsContrato;

            return View(persona);
        }

        [HttpPost]
        public async Task<IActionResult> EditarPerfil(Persona persona)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Persona personaAux = await context.Persona
                        .FirstOrDefaultAsync(x => x.Email == persona.Email);

                    personaAux.TipoDocumentoId = persona.TipoDocumentoId;
                    personaAux.NumeroIdentificacion = persona.NumeroIdentificacion;
                    personaAux.PrimerNombre = persona.PrimerNombre;
                    personaAux.SegundoNombre = persona.SegundoNombre;
                    personaAux.PrimerApellido = persona.PrimerApellido;
                    personaAux.SegundoApellido = persona.SegundoApellido;
                    personaAux.ArlId = persona.ArlId;
                    personaAux.EpsId = persona.EpsId;
                    personaAux.Telefono = persona.Telefono;
                    personaAux.TipoSangreId = persona.TipoSangreId;
                    personaAux.Habilidades = persona.Habilidades;
                    personaAux.Direccion = persona.Direccion;
                    personaAux.TipoContratoId = persona.TipoContratoId;

                    context.Update(personaAux);
                    await context.SaveChangesAsync();
                    return RedirectToAction("EditarPerfil", "Persona");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaExists(persona.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(persona);
        }

        private List<TipoRolGestion> GetTiposRolGestion()
        {
            return context.TipoRolGestion.ToList();
        }

        private bool PersonaExists(string id)
        {
            return context.Persona.Any(e => e.Id == id);
        }

        [HttpGet]
        public ActionResult DescargarArchivo(string file)
        {
            if (file == null)
                return new NotFoundResult();

            var ruta = Path.Combine(env.ContentRootPath, "Uploads", file);
            if (System.IO.File.Exists(ruta))
            {
                var fs = System.IO.File.OpenRead(ruta);
                file = file.Substring(file.IndexOf("__") + 2);
                return File(fs, "application/octet-stream", file);
            }

            return new NotFoundResult();
        }
    }
}