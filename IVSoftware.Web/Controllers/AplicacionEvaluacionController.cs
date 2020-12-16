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
    public class AplicacionEvaluacionController : Controller
    {
        private readonly IVSoftwareContext _context;

        public AplicacionEvaluacionController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: AplicacionEvaluacion
        public async Task<IActionResult> Index(string userName)
        {
            if (userName == null)
                userName = this.User.Identity.Name;

            var persona = await _context.Persona
                .FirstOrDefaultAsync(p => p.Email == userName);

            var aplicacionEvaluacion = await _context.AplicacionEvaluacion
                .Include(a => a.Evaluacion)
                .Include(a => a.Persona)
                .Where(a => a.PersonaId == persona.Id).ToListAsync();
            
            return View(aplicacionEvaluacion);
        }

        // GET: AplicacionEvaluacion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aplicacionEvaluacion = await _context.AplicacionEvaluacion
                .Include(a => a.Evaluacion)
                .Include(a => a.Persona)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aplicacionEvaluacion == null)
            {
                return NotFound();
            }

            var preguntas = await _context.Pregunta
                .Where(p => p.EvaluacionId == aplicacionEvaluacion.EvaluacionId).ToListAsync();
            ViewBag.Preguntas = preguntas;

            var respuestas = await _context.Respuesta
                .Where(r => r.Pregunta.EvaluacionId == aplicacionEvaluacion.EvaluacionId).ToListAsync();
            ViewBag.Respuestas = respuestas;

            return View(aplicacionEvaluacion);
        }

        // GET: AplicacionEvaluacion/Create
        public async Task<IActionResult> Create(string userName)
        {
            var persona = await _context.Persona
                .FirstOrDefaultAsync(p => p.Email == userName);

            ViewData["EvaluacionId"] = new SelectList(_context.Evaluacion, "Id", "Nombre");
            //ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id");
            ViewData["PersonaId"] = persona.Id;

            return View();
        }

        // POST: AplicacionEvaluacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaAplicacion,PersonaId,EvaluacionId")] AplicacionEvaluacion aplicacionEvaluacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aplicacionEvaluacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EvaluacionId"] = new SelectList(_context.Evaluacion, "Id", "Nombre", aplicacionEvaluacion.EvaluacionId);
            ViewData["PersonaId"] = aplicacionEvaluacion.PersonaId;
            return View(aplicacionEvaluacion);
        }

        // GET: AplicacionEvaluacion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aplicacionEvaluacion = await _context.AplicacionEvaluacion.FindAsync(id);
            if (aplicacionEvaluacion == null)
            {
                return NotFound();
            }
            ViewData["EvaluacionId"] = new SelectList(_context.Evaluacion, "Id", "Id", aplicacionEvaluacion.EvaluacionId);
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id", aplicacionEvaluacion.PersonaId);
            return View(aplicacionEvaluacion);
        }

        // POST: AplicacionEvaluacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FechaAplicacion,PersonaId,EvaluacionId")] AplicacionEvaluacion aplicacionEvaluacion)
        {
            if (id != aplicacionEvaluacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aplicacionEvaluacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AplicacionEvaluacionExists(aplicacionEvaluacion.Id))
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
            ViewData["EvaluacionId"] = new SelectList(_context.Evaluacion, "Id", "Id", aplicacionEvaluacion.EvaluacionId);
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id", aplicacionEvaluacion.PersonaId);
            return View(aplicacionEvaluacion);
        }

        // GET: AplicacionEvaluacion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aplicacionEvaluacion = await _context.AplicacionEvaluacion
                .Include(a => a.Evaluacion)
                .Include(a => a.Persona)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aplicacionEvaluacion == null)
            {
                return NotFound();
            }

            return View(aplicacionEvaluacion);
        }

        // POST: AplicacionEvaluacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aplicacionEvaluacion = await _context.AplicacionEvaluacion.FindAsync(id);
            _context.AplicacionEvaluacion.Remove(aplicacionEvaluacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AplicacionEvaluacionExists(int id)
        {
            return _context.AplicacionEvaluacion.Any(e => e.Id == id);
        }
    }
}
