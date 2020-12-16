using IVSoftware.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;


namespace IVSoftware.Web.Controllers
{
    public class OtroConocimientoController : Controller
    {
        private readonly IVSoftwareContext _context;

        public OtroConocimientoController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: OtroConocimiento
        public async Task<IActionResult> Index()
        {
            var iVSoftwareContext = _context.OtroConocimiento
                .Include(e => e.Persona);
            return View(await iVSoftwareContext.ToListAsync());
        }

        // GET: OtroConocimiento/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var otroConocimiento = await _context.OtroConocimiento
                .Include(e => e.Persona)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (otroConocimiento == null)
            {
                return NotFound();
            }

            return View(otroConocimiento);
        }

        // GET: OtroConocimiento/Create
        public async Task<IActionResult> Create(string personaId)
        {
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id");
            Persona persona = await _context.Persona.FirstOrDefaultAsync(p => p.Id == personaId);
            ViewBag.persona = persona;

            return View();
        }

        // POST: OtroConocimiento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Tiempo,PersonaId")] OtroConocimiento otroConocimiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(otroConocimiento);
                await _context.SaveChangesAsync();


                var persona = _context.Persona.Find(otroConocimiento.PersonaId);

                return RedirectToAction("EditarPerfil", "Persona", new { userName = persona.Email });
            }
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id", otroConocimiento.PersonaId);
            return View(otroConocimiento);
        }

        // GET: OtroConocimiento/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var otroConocimiento = await _context.OtroConocimiento
                .Include(e => e.Persona)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (otroConocimiento == null)
            {
                return NotFound();
            }
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id", otroConocimiento.PersonaId);
            return View(otroConocimiento);
        }

        // POST: OtroConocimiento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nombre,Tiempo,PersonaId")] OtroConocimiento otroConocimiento)
        {
            if (id != otroConocimiento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(otroConocimiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OtroConocimientoExists(otroConocimiento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                var persona = _context.Persona.Find(otroConocimiento.PersonaId);

                return RedirectToAction("EditarPerfil", "Persona", new { userName = persona.Email });
            }
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id", otroConocimiento.PersonaId);
            return View(otroConocimiento);
        }

        // GET: OtroConocimiento/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var otroConocimiento = await _context.OtroConocimiento
                .Include(e => e.Persona)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (otroConocimiento == null)
            {
                return NotFound();
            }

            return View(otroConocimiento);
        }

        // POST: OtroConocimiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var otroConocimiento = await _context.OtroConocimiento.FindAsync(id);
            _context.OtroConocimiento.Remove(otroConocimiento);
            await _context.SaveChangesAsync();

            var persona = _context.Persona.Find(otroConocimiento.PersonaId);

            return RedirectToAction("EditarPerfil", "Persona", new { userName = persona.Email });
        }

        private bool OtroConocimientoExists(long id)
        {
            return _context.OtroConocimiento.Any(e => e.Id == id);
        }
    }
}
