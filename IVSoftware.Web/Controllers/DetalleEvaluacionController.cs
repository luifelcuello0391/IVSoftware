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
    public class DetalleEvaluacionController : Controller
    {
        private readonly IVSoftwareContext _context;

        public DetalleEvaluacionController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: DetalleEvaluacion
        public async Task<IActionResult> Index()
        {
            var detalleEvaluacion = await _context.DetalleEvaluacion
                .Include(d => d.AplicacionEvaluacion)
                .Include(d => d.Pregunta).ToListAsync();

            return View(detalleEvaluacion);
        }

        // GET: DetalleEvaluacion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleEvaluacion = await _context.DetalleEvaluacion
                .Include(d => d.AplicacionEvaluacion)
                .Include(d => d.Pregunta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalleEvaluacion == null)
            {
                return NotFound();
            }

            return View(detalleEvaluacion);
        }

        // GET: DetalleEvaluacion/Create
        public IActionResult Create()
        {
            ViewData["AplicacionEvaluacionId"] = new SelectList(_context.AplicacionEvaluacion, "Id", "PersonaId");
            ViewData["PreguntaId"] = new SelectList(_context.Respuesta, "Id", "Id");
            return View();
        }

        // POST: DetalleEvaluacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AplicacionEvaluacionId,PreguntaId")] DetalleEvaluacion detalleEvaluacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleEvaluacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AplicacionEvaluacionId"] = new SelectList(_context.AplicacionEvaluacion, "Id", "PersonaId", detalleEvaluacion.AplicacionEvaluacionId);
            ViewData["PreguntaId"] = new SelectList(_context.Respuesta, "Id", "Id", detalleEvaluacion.PreguntaId);
            return View(detalleEvaluacion);
        }

        // GET: DetalleEvaluacion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleEvaluacion = await _context.DetalleEvaluacion.FindAsync(id);
            if (detalleEvaluacion == null)
            {
                return NotFound();
            }
            ViewData["AplicacionEvaluacionId"] = new SelectList(_context.AplicacionEvaluacion, "Id", "PersonaId", detalleEvaluacion.AplicacionEvaluacionId);
            ViewData["PreguntaId"] = new SelectList(_context.Respuesta, "Id", "Id", detalleEvaluacion.PreguntaId);
            return View(detalleEvaluacion);
        }

        // POST: DetalleEvaluacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AplicacionEvaluacionId,PreguntaId")] DetalleEvaluacion detalleEvaluacion)
        {
            if (id != detalleEvaluacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleEvaluacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleEvaluacionExists(detalleEvaluacion.Id))
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
            ViewData["AplicacionEvaluacionId"] = new SelectList(_context.AplicacionEvaluacion, "Id", "PersonaId", detalleEvaluacion.AplicacionEvaluacionId);
            ViewData["PreguntaId"] = new SelectList(_context.Respuesta, "Id", "Id", detalleEvaluacion.PreguntaId);
            return View(detalleEvaluacion);
        }

        // GET: DetalleEvaluacion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleEvaluacion = await _context.DetalleEvaluacion
                .Include(d => d.AplicacionEvaluacion)
                .Include(d => d.Pregunta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalleEvaluacion == null)
            {
                return NotFound();
            }

            return View(detalleEvaluacion);
        }

        // POST: DetalleEvaluacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalleEvaluacion = await _context.DetalleEvaluacion.FindAsync(id);
            _context.DetalleEvaluacion.Remove(detalleEvaluacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleEvaluacionExists(int id)
        {
            return _context.DetalleEvaluacion.Any(e => e.Id == id);
        }
    }
}
