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
    public class TiposRolesGestionController : Controller
    {
        private readonly IVSoftwareContext _context;

        public TiposRolesGestionController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: TiposRolesGestion
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoRolGestion.ToListAsync());
        }

        // GET: TiposRolesGestion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoRolGestion = await _context.TipoRolGestion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoRolGestion == null)
            {
                return NotFound();
            }

            return View(tipoRolGestion);
        }

        // GET: TiposRolesGestion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TiposRolesGestion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] TipoRolGestion tipoRolGestion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoRolGestion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoRolGestion);
        }

        // GET: TiposRolesGestion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoRolGestion = await _context.TipoRolGestion.FindAsync(id);
            if (tipoRolGestion == null)
            {
                return NotFound();
            }
            return View(tipoRolGestion);
        }

        // POST: TiposRolesGestion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] TipoRolGestion tipoRolGestion)
        {
            if (id != tipoRolGestion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoRolGestion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoRolGestionExists(tipoRolGestion.Id))
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
            return View(tipoRolGestion);
        }

        // GET: TiposRolesGestion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoRolGestion = await _context.TipoRolGestion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoRolGestion == null)
            {
                return NotFound();
            }

            return View(tipoRolGestion);
        }

        // POST: TiposRolesGestion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoRolGestion = await _context.TipoRolGestion.FindAsync(id);
            _context.TipoRolGestion.Remove(tipoRolGestion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoRolGestionExists(int id)
        {
            return _context.TipoRolGestion.Any(e => e.Id == id);
        }
    }
}
