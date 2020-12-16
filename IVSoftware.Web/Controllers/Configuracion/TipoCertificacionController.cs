using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IVSoftware.Web.Models;

namespace IVSoftware.Web.Controllers.Configuracion
{
    public class TipoCertificacionController : Controller
    {
        private readonly IVSoftwareContext _context;

        public TipoCertificacionController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: TipoCertificacion
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoCertificacion.ToListAsync());
        }

        // GET: TipoCertificacion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCertificacion = await _context.TipoCertificacion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoCertificacion == null)
            {
                return NotFound();
            }

            return View(tipoCertificacion);
        }

        // GET: TipoCertificacion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoCertificacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] TipoCertificacion tipoCertificacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoCertificacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoCertificacion);
        }

        // GET: TipoCertificacion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCertificacion = await _context.TipoCertificacion.FindAsync(id);
            if (tipoCertificacion == null)
            {
                return NotFound();
            }
            return View(tipoCertificacion);
        }

        // POST: TipoCertificacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] TipoCertificacion tipoCertificacion)
        {
            if (id != tipoCertificacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoCertificacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoCertificacionExists(tipoCertificacion.Id))
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
            return View(tipoCertificacion);
        }

        // GET: TipoCertificacion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCertificacion = await _context.TipoCertificacion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoCertificacion == null)
            {
                return NotFound();
            }

            return View(tipoCertificacion);
        }

        // POST: TipoCertificacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoCertificacion = await _context.TipoCertificacion.FindAsync(id);
            _context.TipoCertificacion.Remove(tipoCertificacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoCertificacionExists(int id)
        {
            return _context.TipoCertificacion.Any(e => e.Id == id);
        }
    }
}
