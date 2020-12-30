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
    public class MeasurementUnitModelsController : Controller
    {
        private readonly IVSoftwareContext _context;

        public MeasurementUnitModelsController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: MeasurementUnitModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.MeasurementUnitModel.ToListAsync());
        }

        // GET: MeasurementUnitModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measurementUnitModel = await _context.MeasurementUnitModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (measurementUnitModel == null)
            {
                return NotFound();
            }

            return View(measurementUnitModel);
        }

        // GET: MeasurementUnitModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MeasurementUnitModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] MeasurementUnitModel measurementUnitModel)
        {
            if (ModelState.IsValid)
            {
                ViewData["creation_date"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                ViewData["modification_date"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                _context.Add(measurementUnitModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(measurementUnitModel);
        }

        // GET: MeasurementUnitModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measurementUnitModel = await _context.MeasurementUnitModel.FindAsync(id);
            if (measurementUnitModel == null)
            {
                return NotFound();
            }
            return View(measurementUnitModel);
        }

        // POST: MeasurementUnitModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] MeasurementUnitModel measurementUnitModel)
        {
            if (id != measurementUnitModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(measurementUnitModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeasurementUnitModelExists(measurementUnitModel.Id))
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
            return View(measurementUnitModel);
        }

        // GET: MeasurementUnitModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measurementUnitModel = await _context.MeasurementUnitModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (measurementUnitModel == null)
            {
                return NotFound();
            }

            return View(measurementUnitModel);
        }

        // POST: MeasurementUnitModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var measurementUnitModel = await _context.MeasurementUnitModel.FindAsync(id);
            _context.MeasurementUnitModel.Remove(measurementUnitModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeasurementUnitModelExists(int id)
        {
            return _context.MeasurementUnitModel.Any(e => e.Id == id);
        }
    }
}
