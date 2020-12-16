using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IVSoftware.Web.Models;

namespace IVSoftware.Web.Controllers
{
    public class PersonaInduccionController : Controller
    {
        private readonly IVSoftwareContext _context;

        public PersonaInduccionController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: PersonaInduccion
        public async Task<IActionResult> Index()
        {
            var iVSoftwareContext = _context.PersonaInduccion
                .Include(p => p.Persona).Include(p => p.TipoInduccion);
            return View(await iVSoftwareContext.ToListAsync());
        }

        // GET: PersonaInduccion/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personaInduccion = await _context.PersonaInduccion
                .Include(p => p.Persona)
                .Include(p => p.TipoInduccion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personaInduccion == null)
            {
                return NotFound();
            }

            return View(personaInduccion);
        }

        // GET: PersonaInduccion/Create
        public IActionResult Create()
        {
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id");
            ViewData["TipoInduccionId"] = new SelectList(_context.TipoInduccion, "Id", "Nombre");
            return View();
        }

        // POST: PersonaInduccion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaInduccion,PersonaId,TipoInduccionId")] PersonaInduccion personaInduccion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personaInduccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id", personaInduccion.PersonaId);
            ViewData["TipoInduccionId"] = new SelectList(_context.TipoInduccion, "Id", "Id", personaInduccion.TipoInduccionId);
            return View(personaInduccion);
        }

        // GET: PersonaInduccion/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personaInduccion = await _context.PersonaInduccion.FindAsync(id);
            if (personaInduccion == null)
            {
                return NotFound();
            }
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id", personaInduccion.PersonaId);
            ViewData["TipoInduccionId"] = new SelectList(_context.TipoInduccion, "Id", "Id", personaInduccion.TipoInduccionId);
            return View(personaInduccion);
        }

        // POST: PersonaInduccion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,FechaInduccion,PersonaId,TipoInduccionId")] PersonaInduccion personaInduccion)
        {
            if (id != personaInduccion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personaInduccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaInduccionExists(personaInduccion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id", personaInduccion.PersonaId);
            ViewData["TipoInduccionId"] = new SelectList(_context.TipoInduccion, "Id", "Id", personaInduccion.TipoInduccionId);
            return View(personaInduccion);
        }

        // GET: PersonaInduccion/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personaInduccion = await _context.PersonaInduccion
                .Include(p => p.Persona)
                .Include(p => p.TipoInduccion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personaInduccion == null)
            {
                return NotFound();
            }

            return View(personaInduccion);
        }

        // POST: PersonaInduccion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var personaInduccion = await _context.PersonaInduccion.FindAsync(id);
            _context.PersonaInduccion.Remove(personaInduccion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonaInduccionExists(long id)
        {
            return _context.PersonaInduccion.Any(e => e.Id == id);
        }
    }
}
