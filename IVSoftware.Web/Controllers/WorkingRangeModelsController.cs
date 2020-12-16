using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IVSoftware.Models;
using IVSoftware.Web.Models;

namespace IVSoftware.Web.Controllers
{
    public class WorkingRangeModelsController : Controller
    {
        private readonly IVSoftwareContext _context;

        public WorkingRangeModelsController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: WorkingRangeModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.WorkingRangeModel.ToListAsync());
        }

        // GET: WorkingRangeModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workingRangeModel = await _context.WorkingRangeModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workingRangeModel == null)
            {
                return NotFound();
            }

            return View(workingRangeModel);
        }

        // GET: WorkingRangeModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorkingRangeModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Precondition,MinimumValue,MaximumValue,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] WorkingRangeModel workingRangeModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workingRangeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workingRangeModel);
        }

        // GET: WorkingRangeModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workingRangeModel = await _context.WorkingRangeModel.FindAsync(id);
            if (workingRangeModel == null)
            {
                return NotFound();
            }
            return View(workingRangeModel);
        }

        // POST: WorkingRangeModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Precondition,MinimumValue,MaximumValue,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] WorkingRangeModel workingRangeModel)
        {
            if (id != workingRangeModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workingRangeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkingRangeModelExists(workingRangeModel.Id))
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
            return View(workingRangeModel);
        }

        // GET: WorkingRangeModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workingRangeModel = await _context.WorkingRangeModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workingRangeModel == null)
            {
                return NotFound();
            }

            return View(workingRangeModel);
        }

        // POST: WorkingRangeModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workingRangeModel = await _context.WorkingRangeModel.FindAsync(id);
            _context.WorkingRangeModel.Remove(workingRangeModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkingRangeModelExists(int id)
        {
            return _context.WorkingRangeModel.Any(e => e.Id == id);
        }
    }
}
