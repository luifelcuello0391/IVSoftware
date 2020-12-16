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
    public class TipoContratoController : Controller
    {
        private readonly IVSoftwareContext _context;

        public TipoContratoController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: TipoContrato
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoContrato.ToListAsync());
        }

        // GET: TipoContrato/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoContrato = await _context.TipoContrato
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoContrato == null)
            {
                return NotFound();
            }

            return View(tipoContrato);
        }

        // GET: TipoContrato/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoContrato/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] TipoContrato tipoContrato)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoContrato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoContrato);
        }

        // GET: TipoContrato/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoContrato = await _context.TipoContrato.FindAsync(id);
            if (tipoContrato == null)
            {
                return NotFound();
            }
            return View(tipoContrato);
        }

        // POST: TipoContrato/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] TipoContrato tipoContrato)
        {
            if (id != tipoContrato.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoContrato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoContratoExists(tipoContrato.Id))
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
            return View(tipoContrato);
        }

        // GET: TipoContrato/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoContrato = await _context.TipoContrato
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoContrato == null)
            {
                return NotFound();
            }

            return View(tipoContrato);
        }

        // POST: TipoContrato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoContrato = await _context.TipoContrato.FindAsync(id);
            _context.TipoContrato.Remove(tipoContrato);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoContratoExists(int id)
        {
            return _context.TipoContrato.Any(e => e.Id == id);
        }
    }
}
