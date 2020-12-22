using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IVSoftware.Web.Models;
using IVSoftware.Data.Models;

namespace IVSoftware.Web.Controllers.Configuracion
{
    public class EpsController : Controller
    {
        private readonly IVSoftwareContext _context;

        public EpsController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: Eps
        public async Task<IActionResult> Index()
        {
            return View(await _context.Eps.ToListAsync());
        }

        // GET: Eps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eps = await _context.Eps
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eps == null)
            {
                return NotFound();
            }

            return View(eps);
        }

        // GET: Eps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Eps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Eps eps)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eps);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eps);
        }

        // GET: Eps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eps = await _context.Eps.FindAsync(id);
            if (eps == null)
            {
                return NotFound();
            }
            return View(eps);
        }

        // POST: Eps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Eps eps)
        {
            if (id != eps.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eps);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EpsExists(eps.Id))
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
            return View(eps);
        }

        // GET: Eps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eps = await _context.Eps
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eps == null)
            {
                return NotFound();
            }

            return View(eps);
        }

        // POST: Eps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eps = await _context.Eps.FindAsync(id);
            _context.Eps.Remove(eps);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EpsExists(int id)
        {
            return _context.Eps.Any(e => e.Id == id);
        }
    }
}
