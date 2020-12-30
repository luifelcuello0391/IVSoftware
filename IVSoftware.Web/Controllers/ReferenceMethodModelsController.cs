using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IVSoftware.Models;
using IVSoftware.Web.Models;
using IVSoftware.Data.Models;

namespace IVSoftware.Web.Controllers
{
    public class ReferenceMethodModelsController : Controller
    {
        private readonly IVSoftwareContext _context;

        public ReferenceMethodModelsController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: ReferenceMethodModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.ReferenceMethodModel.ToListAsync());
        }

        // GET: ReferenceMethodModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var referenceMethodModel = await _context.ReferenceMethodModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (referenceMethodModel == null)
            {
                return NotFound();
            }

            return View(referenceMethodModel);
        }

        // GET: ReferenceMethodModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReferenceMethodModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,MeasurementUncertaintyLowerValue,MeasurementUncertaintyUpperValue,IsPercentage,HasUncertainty,Name,CreationDatetime,ModificationDatetime,RegisterStatus")] ReferenceMethodModel referenceMethodModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(referenceMethodModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(referenceMethodModel);
        }

        // GET: ReferenceMethodModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var referenceMethodModel = await _context.ReferenceMethodModel.FindAsync(id);
            if (referenceMethodModel == null)
            {
                return NotFound();
            }
            return View(referenceMethodModel);
        }

        // POST: ReferenceMethodModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,MeasurementUncertaintyLowerValue,MeasurementUncertaintyUpperValue,IsPercentage,HasUncertainty,Name,CreationDatetime,ModificationDatetime,RegisterStatus")] ReferenceMethodModel referenceMethodModel)
        {
            if (id != referenceMethodModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(referenceMethodModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReferenceMethodModelExists(referenceMethodModel.Id))
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
            return View(referenceMethodModel);
        }

        // GET: ReferenceMethodModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var referenceMethodModel = await _context.ReferenceMethodModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (referenceMethodModel == null)
            {
                return NotFound();
            }

            return View(referenceMethodModel);
        }

        // POST: ReferenceMethodModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var referenceMethodModel = await _context.ReferenceMethodModel.FindAsync(id);
            _context.ReferenceMethodModel.Remove(referenceMethodModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReferenceMethodModelExists(int id)
        {
            return _context.ReferenceMethodModel.Any(e => e.Id == id);
        }
    }
}
