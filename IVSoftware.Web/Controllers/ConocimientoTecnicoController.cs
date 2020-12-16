using IVSoftware.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.Controllers
{
    public class ConocimientoTecnicoController : Controller
    {
        private readonly IVSoftwareContext _context;

        public ConocimientoTecnicoController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: ConocimientoTecnico
        public async Task<IActionResult> Index()
        {
            var iVSoftwareContext = _context.ConocimientoTecnico
                .Include(e => e.Persona);
            return View(await iVSoftwareContext.ToListAsync());
        }

        // GET: ConocimientoTecnico/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conocimientoTecnico = await _context.ConocimientoTecnico
                .Include(e => e.Persona)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conocimientoTecnico == null)
            {
                return NotFound();
            }

            return View(conocimientoTecnico);
        }

        // GET: ConocimientoTecnico/Create
        public async Task<IActionResult> Create(string personaId)
        {
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id");
            Persona persona = await _context.Persona.FirstOrDefaultAsync(p => p.Id == personaId);
            ViewBag.persona = persona;

            return View();
        }

        // POST: ConocimientoTecnico/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Analisis,TecnicaAnalitica,Matriz,Tiempo,PersonaId")] ConocimientoTecnico conocimientoTecnico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conocimientoTecnico);
                await _context.SaveChangesAsync();

                var persona = _context.Persona.Find(conocimientoTecnico.PersonaId);

                return RedirectToAction("EditarPerfil", "Persona", new { userName = persona.Email });
            }
            return View(conocimientoTecnico);
        }

        // GET: ConocimientoTecnico/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conocimientoTecnico = await _context.ConocimientoTecnico
                .Include(e => e.Persona)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conocimientoTecnico == null)
            {
                return NotFound();
            }
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id", conocimientoTecnico.PersonaId);
            return View(conocimientoTecnico);
        }

        // POST: ConocimientoTecnico/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Analisis,TecnicaAnalitica,Matriz,Tiempo,PersonaId")] ConocimientoTecnico conocimientoTecnico)
        {
            if (id != conocimientoTecnico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conocimientoTecnico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConocimientoTecnicoExists(conocimientoTecnico.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                var persona = _context.Persona.Find(conocimientoTecnico.PersonaId);

                return RedirectToAction("EditarPerfil", "Persona", new { userName = persona.Email });
            }
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id", conocimientoTecnico.PersonaId);
            return View(conocimientoTecnico);
        }

        // GET: ConocimientoTecnico/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conocimientoTecnico = await _context.ConocimientoTecnico
                .Include(e => e.Persona)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conocimientoTecnico == null)
            {
                return NotFound();
            }

            return View(conocimientoTecnico);
        }

        // POST: ConocimientoTecnico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var conocimientoTecnico = await _context.ConocimientoTecnico.FindAsync(id);
            _context.ConocimientoTecnico.Remove(conocimientoTecnico);
            await _context.SaveChangesAsync();

            var persona = _context.Persona.Find(conocimientoTecnico.PersonaId);

            return RedirectToAction("EditarPerfil", "Persona", new { userName = persona.Email });
        }

        private bool ConocimientoTecnicoExists(long id)
        {
            return _context.ConocimientoTecnico.Any(e => e.Id == id);
        }
    }
}
