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
    public class EvaluacionController : Controller
    {
        private readonly IVSoftwareContext _context;

        public EvaluacionController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: Evaluacion
        public async Task<IActionResult> Index()
        {
            return View(await _context.Evaluacion.ToListAsync());
        }

        // GET: Evaluacion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluacion = await _context.Evaluacion
                .Include(p => p.ListPregunta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evaluacion == null)
            {
                return NotFound();
            }

            return View(evaluacion);
        }

        // GET: Evaluacion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Evaluacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Evaluacion evaluacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evaluacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(evaluacion);
        }

        // GET: Evaluacion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluacion = await _context.Evaluacion.FindAsync(id);
            if (evaluacion == null)
            {
                return NotFound();
            }
            return View(evaluacion);
        }

        // POST: Evaluacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Evaluacion evaluacion)
        {
            if (id != evaluacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evaluacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvaluacionExists(evaluacion.Id))
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
            return View(evaluacion);
        }

        // GET: Evaluacion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluacion = await _context.Evaluacion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evaluacion == null)
            {
                return NotFound();
            }

            return View(evaluacion);
        }

        // POST: Evaluacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evaluacion = await _context.Evaluacion.FindAsync(id);
            _context.Evaluacion.Remove(evaluacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvaluacionExists(int id)
        {
            return _context.Evaluacion.Any(e => e.Id == id);
        }
    }
}
