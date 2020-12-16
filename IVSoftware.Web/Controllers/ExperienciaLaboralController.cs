using IVSoftware.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.Controllers
{
    public class ExperienciaLaboralController : Controller
    {
        private readonly IVSoftwareContext _context;

        public ExperienciaLaboralController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: ExperienciaLaboral
        public async Task<IActionResult> Index()
        {
            var iVSoftwareContext = _context.ExperienciaLaboral
                .Include(e => e.Persona)
                .Include(e => e.TipoEmpresa);
            return View(await iVSoftwareContext.ToListAsync());
        }

        // GET: ExperienciaLaboral/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experienciaLaboral = await _context.ExperienciaLaboral
                .Include(e => e.Persona)
                .Include(e => e.TipoEmpresa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (experienciaLaboral == null)
            {
                return NotFound();
            }

            return View(experienciaLaboral);
        }

        // GET: ExperienciaLaboral/Create
        public async Task<IActionResult> Create(string personaId)
        {
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id");
            ViewData["TipoEmpresaId"] = new SelectList(_context.TipoEmpresa, "Id", "Nombre");
            Persona persona = await _context.Persona.FirstOrDefaultAsync(p => p.Id == personaId);
            ViewBag.persona = persona;

            return View();
        }

        // POST: ExperienciaLaboral/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreEmpresa,TipoEmpresaId,CorreoElectronico,Telefono,FechaIngreso,FechaRetiro,EsActual,CargoContrato,Dependencia,Direccion,Responsabilidades,PersonaId")] ExperienciaLaboral experienciaLaboral, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var ruta = Guid.NewGuid() + "__" + file.FileName;
                    experienciaLaboral.ArchivoAdjunto = ruta;
                    var filePath = "Uploads/" + ruta;

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

                _context.Add(experienciaLaboral);
                await _context.SaveChangesAsync();

                var persona = _context.Persona.Find(experienciaLaboral.PersonaId);
                ViewData["TipoEmpresaId"] = new SelectList(_context.TipoEmpresa, "Id", "Nombre", experienciaLaboral.TipoEmpresaId);

                return RedirectToAction("EditarPerfil", "Persona", new { userName = persona.Email });
            }
            return View(experienciaLaboral);
        }

        // GET: ExperienciaLaboral/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experienciaLaboral = await _context.ExperienciaLaboral
                .Include(e => e.Persona)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (experienciaLaboral == null)
            {
                return NotFound();
            }
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id", experienciaLaboral.PersonaId);
            ViewData["TipoEmpresaId"] = new SelectList(_context.TipoEmpresa, "Id", "Nombre", experienciaLaboral.TipoEmpresaId);
            return View(experienciaLaboral);
        }

        // POST: ExperienciaLaboral/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,NombreEmpresa,TipoEmpresaId,CorreoElectronico,Telefono,FechaIngreso,FechaRetiro,EsActual,CargoContrato,Dependencia,Direccion,Responsabilidades,PersonaId,ArchivoAdjunto")] ExperienciaLaboral experienciaLaboral, IFormFile file)
        {
            if (id != experienciaLaboral.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null)
                    {
                        var ruta = Guid.NewGuid() + "__" + file.FileName;
                        experienciaLaboral.ArchivoAdjunto = ruta;
                        var filePath = "Uploads/" + ruta;

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }

                    _context.Update(experienciaLaboral);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExperienciaLaboralExists(experienciaLaboral.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                var persona = _context.Persona.Find(experienciaLaboral.PersonaId);
                ViewData["TipoEmpresaId"] = new SelectList(_context.TipoEmpresa, "Id", "Nombre", experienciaLaboral.TipoEmpresaId);

                return RedirectToAction("EditarPerfil", "Persona", new { userName = persona.Email });
            }
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id", experienciaLaboral.PersonaId);
            return View(experienciaLaboral);
        }

        // GET: ExperienciaLaboral/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experienciaLaboral = await _context.ExperienciaLaboral
                .Include(e => e.Persona)
                .Include(e => e.TipoEmpresa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (experienciaLaboral == null)
            {
                return NotFound();
            }

            return View(experienciaLaboral);
        }

        // POST: ExperienciaLaboral/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var experienciaLaboral = await _context.ExperienciaLaboral.FindAsync(id);
            _context.ExperienciaLaboral.Remove(experienciaLaboral);
            await _context.SaveChangesAsync();

            var persona = _context.Persona.Find(experienciaLaboral.PersonaId);

            return RedirectToAction("EditarPerfil", "Persona", new { userName = persona.Email });
        }

        private bool ExperienciaLaboralExists(long id)
        {
            return _context.ExperienciaLaboral.Any(e => e.Id == id);
        }
    }
}
