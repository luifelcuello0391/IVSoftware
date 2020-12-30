using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IVSoftware.Web.Models;
using IVSoftware.Data.Models;

namespace IVSoftware.Web.Controllers
{
    public class TypeOfServicesController : Controller
    {
        private readonly IVSoftwareContext _context;

        public TypeOfServicesController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: TypeOfServices
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeOfService.ToListAsync());
        }

        // GET: TypeOfServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfService = await _context.TypeOfService
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeOfService == null)
            {
                return NotFound();
            }

            return View(typeOfService);
        }

        // GET: TypeOfServices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeOfServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] TypeOfService typeOfService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeOfService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeOfService);
        }

        // GET: TypeOfServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfService = await _context.TypeOfService.FindAsync(id);
            if (typeOfService == null)
            {
                return NotFound();
            }
            return View(typeOfService);
        }

        // POST: TypeOfServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] TypeOfService typeOfService)
        {
            if (id != typeOfService.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeOfService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeOfServiceExists(typeOfService.Id))
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
            return View(typeOfService);
        }

        // GET: TypeOfServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfService = await _context.TypeOfService
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeOfService == null)
            {
                return NotFound();
            }

            return View(typeOfService);
        }

        // POST: TypeOfServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeOfService = await _context.TypeOfService.FindAsync(id);
            _context.TypeOfService.Remove(typeOfService);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeOfServiceExists(int id)
        {
            return _context.TypeOfService.Any(e => e.Id == id);
        }
    }
}
