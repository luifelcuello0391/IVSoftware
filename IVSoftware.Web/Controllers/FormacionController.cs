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
    public class FormacionController : Controller
    {
        private readonly IVSoftwareContext _context;

        public FormacionController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: Formacion
        public async Task<IActionResult> Index()
        {
            var iVSoftwareContext = _context.Formacion
                .Include(f => f.Persona)
                .Include(f => f.TipoCertificacion);
            return View(await iVSoftwareContext.ToListAsync());
        }

        // GET: Formacion/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formacion = await _context.Formacion
                .Include(f => f.Persona)
                .Include(f => f.TipoCertificacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formacion == null)
            {
                return NotFound();
            }

            return View(formacion);
        }

        // GET: Formacion/Create
        public async Task<IActionResult> Create(string personaId)
        {
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id");
            ViewData["TipoCertificacionId"] = new SelectList(_context.TipoCertificacion, "Id", "Nombre");
            Persona persona = await _context.Persona.FirstOrDefaultAsync(p => p.Id == personaId);
            ViewBag.persona = persona;

            return View();
        }

        // POST: Formacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreCurso,Tema,TipoCertificacionId,Entidad,FechaFinalizacion,NumeroHoras,PersonaId")] Formacion formacion, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var ruta = Guid.NewGuid() + "__" + file.FileName;
                    formacion.ArchivoAdjunto = ruta;
                    var filePath = "Uploads/" + ruta;

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

                _context.Add(formacion);
                await _context.SaveChangesAsync();

                var persona = _context.Persona.Find(formacion.PersonaId);

                ViewData["TipoCertificacionId"] = new SelectList(_context.TipoCertificacion, "Id", "Nombre", formacion.TipoCertificacionId);
                return RedirectToAction("EditarPerfil", "Persona", new { userName = persona.Email });
            }
            return View(formacion);
        }

        // GET: Formacion/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formacion = await _context.Formacion
                .Include(e => e.Persona)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formacion == null)
            {
                return NotFound();
            }
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id", formacion.PersonaId);
            ViewData["TipoCertificacionId"] = new SelectList(_context.TipoCertificacion, "Id", "Nombre", formacion.TipoCertificacionId);
            return View(formacion);
        }

        // POST: Formacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,NombreCurso,Tema,TipoCertificacionId,Entidad,FechaFinalizacion,NumeroHoras,PersonaId,ArchivoAdjunto")] Formacion formacion, IFormFile file)
        {
            if (id != formacion.Id)
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
                        formacion.ArchivoAdjunto = ruta;
                        var filePath = "Uploads/" + ruta;

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }

                    _context.Update(formacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormacionExists(formacion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                var persona = _context.Persona.Find(formacion.PersonaId);
                ViewData["TipoCertificacionId"] = new SelectList(_context.TipoCertificacion, "Id", "Nombre", formacion.TipoCertificacionId);

                return RedirectToAction("EditarPerfil", "Persona", new { userName = persona.Email });
            }
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id", formacion.PersonaId);
            return View(formacion);
        }

        // GET: Formacion/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formacion = await _context.Formacion
                .Include(f => f.Persona)
                .Include(f => f.TipoCertificacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formacion == null)
            {
                return NotFound();
            }

            return View(formacion);
        }

        // POST: Formacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var formacion = await _context.Formacion.FindAsync(id);
            _context.Formacion.Remove(formacion);
            await _context.SaveChangesAsync();

            var persona = _context.Persona.Find(formacion.PersonaId);

            return RedirectToAction("EditarPerfil", "Persona", new { userName = persona.Email });
        }

        private bool FormacionExists(long id)
        {
            return _context.Formacion.Any(e => e.Id == id);
        }
    }
}
