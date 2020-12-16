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
    public class EducacionSuperiorController : Controller
    {
        private readonly IVSoftwareContext _context;

        public EducacionSuperiorController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: EducacionSuperior
        public async Task<IActionResult> Index()
        {
            var iVSoftwareContext = _context.EducacionSuperior
                .Include(e => e.Persona)
                .Include(e => e.ModalidadAcademica);
            return View(await iVSoftwareContext.ToListAsync());
        }

        // GET: EducacionSuperior/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educacionSuperior = await _context.EducacionSuperior
                .Include(e => e.Persona)
                .Include(e => e.ModalidadAcademica)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educacionSuperior == null)
            {
                return NotFound();
            }

            return View(educacionSuperior);
        }

        // GET: EducacionSuperior/Create
        public async Task<IActionResult> Create(string personaId)
        {
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id");
            ViewData["ModalidadAcademicaId"] = new SelectList(_context.ModalidadAcademica, "Id", "Nombre");
            Persona persona = await _context.Persona.FirstOrDefaultAsync(p => p.Id == personaId);
            ViewBag.persona = persona;

            return View();
        }

        // POST: EducacionSuperior/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreInstitucion,SemestresAprobados,EsGraduado,FechaGrado,NombreEstudios,NumeroTarjetaProfesional,ModalidadAcademicaId,PersonaId")] EducacionSuperior educacionSuperior, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var ruta = Guid.NewGuid() + "__" + file.FileName;
                    educacionSuperior.ArchivoAdjunto = ruta;
                    var filePath = "Uploads/" + ruta;

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

                _context.Add(educacionSuperior);
                await _context.SaveChangesAsync();

                var persona = _context.Persona.Find(educacionSuperior.PersonaId);

                return RedirectToAction("EditarPerfil", "Persona", new { userName = persona.Email });
            }
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id", educacionSuperior.PersonaId);
            ViewData["ModalidadAcademicaId"] = new SelectList(_context.ModalidadAcademica, "Id", "Nombre", educacionSuperior.ModalidadAcademicaId);
            return View(educacionSuperior);
        }

        // GET: EducacionSuperior/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educacionSuperior = await _context.EducacionSuperior
                .Include(e => e.Persona)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educacionSuperior == null)
            {
                return NotFound();
            }
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id", educacionSuperior.PersonaId);
            ViewData["ModalidadAcademicaId"] = new SelectList(_context.ModalidadAcademica, "Id", "Nombre", educacionSuperior.ModalidadAcademicaId);
            return View(educacionSuperior);
        }

        // POST: EducacionSuperior/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,NombreInstitucion,SemestresAprobados,EsGraduado,FechaGrado,NombreEstudios,NumeroTarjetaProfesional,ModalidadAcademicaId,PersonaId,ArchivoAdjunto")] EducacionSuperior educacionSuperior, IFormFile file)
        {
            if (id != educacionSuperior.Id)
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
                        educacionSuperior.ArchivoAdjunto = ruta;
                        var filePath = "Uploads/" + ruta;

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }

                    _context.Update(educacionSuperior);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducacionSuperiorExists(educacionSuperior.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                var persona = _context.Persona.Find(educacionSuperior.PersonaId);

                return RedirectToAction("EditarPerfil", "Persona", new { userName = persona.Email });
            }
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id", educacionSuperior.PersonaId);
            ViewData["ModalidadAcademicaId"] = new SelectList(_context.ModalidadAcademica, "Id", "Nombre", educacionSuperior.ModalidadAcademicaId);
            return View(educacionSuperior);
        }

        // GET: EducacionSuperior/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educacionSuperior = await _context.EducacionSuperior
                .Include(e => e.Persona)
                .Include(e => e.ModalidadAcademica)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educacionSuperior == null)
            {
                return NotFound();
            }

            return View(educacionSuperior);
        }

        // POST: EducacionSuperior/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var educacionSuperior = await _context.EducacionSuperior.FindAsync(id);
            _context.EducacionSuperior.Remove(educacionSuperior);
            await _context.SaveChangesAsync();

            var persona = _context.Persona.Find(educacionSuperior.PersonaId);

            return RedirectToAction("EditarPerfil", "Persona", new { userName = persona.Email });
        }

        private bool EducacionSuperiorExists(long id)
        {
            return _context.EducacionSuperior.Any(e => e.Id == id);
        }
    }
}
