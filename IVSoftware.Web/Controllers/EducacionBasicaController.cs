using IVSoftware.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.Controllers
{
    public class EducacionBasicaController : Controller
    {
        private readonly IVSoftwareContext _context;

        public EducacionBasicaController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: EducacionBasica
        public async Task<IActionResult> Index()
        {
            var iVSoftwareContext = _context.EducacionBasica
                .Include(e => e.Persona);
            return View(await iVSoftwareContext.ToListAsync());
        }

        // GET: EducacionBasica/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educacionBasica = await _context.EducacionBasica
                .Include(e => e.Persona)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educacionBasica == null)
            {
                return NotFound();
            }

            return View(educacionBasica);
        }

        // GET: EducacionBasica/Create
        public async Task<IActionResult> Create(string personaId)
        {
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id");
            Persona persona = await _context.Persona.FirstOrDefaultAsync(p => p.Id == personaId);
            ViewBag.persona = persona;

            return View();
        }

        // POST: EducacionBasica/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreInstitucion,UltimoGradoAprobado,TituloObtenido,FechaGrado,PersonaId")] EducacionBasica educacionBasica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(educacionBasica);
                await _context.SaveChangesAsync();

                var persona = _context.Persona.Find(educacionBasica.PersonaId);

                return RedirectToAction("EditarPerfil", "Persona", new { userName = persona.Email });
            }
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id", educacionBasica.PersonaId);
            return View(educacionBasica);
        }

        // GET: EducacionBasica/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educacionBasica = await _context.EducacionBasica
                .Include(e => e.Persona)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educacionBasica == null)
            {
                return NotFound();
            }
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id", educacionBasica.PersonaId);
            return View(educacionBasica);
        }

        // POST: EducacionBasica/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,NombreInstitucion,UltimoGradoAprobado,TituloObtenido,FechaGrado,PersonaId")] EducacionBasica educacionBasica)
        {
            if (id != educacionBasica.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(educacionBasica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducacionBasicaExists(educacionBasica.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                var persona = _context.Persona.Find(educacionBasica.PersonaId);

                return RedirectToAction("EditarPerfil", "Persona", new { userName = persona.Email });
            }
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id", educacionBasica.PersonaId);
            return View(educacionBasica);
        }

        // GET: EducacionBasica/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educacionBasica = await _context.EducacionBasica
                .Include(e => e.Persona)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educacionBasica == null)
            {
                return NotFound();
            }

            return View(educacionBasica);
        }

        // POST: EducacionBasica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var educacionBasica = await _context.EducacionBasica.FindAsync(id);
            _context.EducacionBasica.Remove(educacionBasica);
            await _context.SaveChangesAsync();

            var persona = _context.Persona.Find(educacionBasica.PersonaId);

            return RedirectToAction("EditarPerfil", "Persona", new { userName = persona.Email });
        }

        private bool EducacionBasicaExists(long id)
        {
            return _context.EducacionBasica.Any(e => e.Id == id);
        }
    }
}
