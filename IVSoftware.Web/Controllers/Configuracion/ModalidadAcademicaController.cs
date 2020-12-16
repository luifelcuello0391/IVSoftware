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
    public class ModalidadAcademicaController : Controller
    {
        private readonly IVSoftwareContext _context;

        public ModalidadAcademicaController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: ModalidadAcademica
        public async Task<IActionResult> Index()
        {
            return View(await _context.ModalidadAcademica.ToListAsync());
        }

        // GET: ModalidadAcademica/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modalidadAcademica = await _context.ModalidadAcademica
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modalidadAcademica == null)
            {
                return NotFound();
            }

            return View(modalidadAcademica);
        }

        // GET: ModalidadAcademica/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ModalidadAcademica/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] ModalidadAcademica modalidadAcademica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modalidadAcademica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(modalidadAcademica);
        }

        // GET: ModalidadAcademica/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modalidadAcademica = await _context.ModalidadAcademica.FindAsync(id);
            if (modalidadAcademica == null)
            {
                return NotFound();
            }
            return View(modalidadAcademica);
        }

        // POST: ModalidadAcademica/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] ModalidadAcademica modalidadAcademica)
        {
            if (id != modalidadAcademica.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modalidadAcademica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModalidadAcademicaExists(modalidadAcademica.Id))
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
            return View(modalidadAcademica);
        }

        // GET: ModalidadAcademica/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modalidadAcademica = await _context.ModalidadAcademica
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modalidadAcademica == null)
            {
                return NotFound();
            }

            return View(modalidadAcademica);
        }

        // POST: ModalidadAcademica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modalidadAcademica = await _context.ModalidadAcademica.FindAsync(id);
            _context.ModalidadAcademica.Remove(modalidadAcademica);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModalidadAcademicaExists(int id)
        {
            return _context.ModalidadAcademica.Any(e => e.Id == id);
        }
    }
}
