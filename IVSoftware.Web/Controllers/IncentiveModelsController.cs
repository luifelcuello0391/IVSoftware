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
    public class IncentiveModelsController : Controller
    {
        private readonly IVSoftwareContext _context;

        public IncentiveModelsController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: IncentiveModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.IncentiveModel.ToListAsync());
        }

        // GET: IncentiveModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incentiveModel = await _context.IncentiveModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incentiveModel == null)
            {
                return NotFound();
            }

            return View(incentiveModel);
        }

        // GET: IncentiveModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IncentiveModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value,IsPercentage,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] IncentiveModel incentiveModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(incentiveModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(incentiveModel);
        }

        // GET: IncentiveModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incentiveModel = await _context.IncentiveModel.FindAsync(id);
            if (incentiveModel == null)
            {
                return NotFound();
            }
            return View(incentiveModel);
        }

        // POST: IncentiveModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Value,IsPercentage,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] IncentiveModel incentiveModel)
        {
            if (id != incentiveModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(incentiveModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncentiveModelExists(incentiveModel.Id))
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
            return View(incentiveModel);
        }

        // GET: IncentiveModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incentiveModel = await _context.IncentiveModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incentiveModel == null)
            {
                return NotFound();
            }

            return View(incentiveModel);
        }

        // POST: IncentiveModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var incentiveModel = await _context.IncentiveModel.FindAsync(id);
            _context.IncentiveModel.Remove(incentiveModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncentiveModelExists(int id)
        {
            return _context.IncentiveModel.Any(e => e.Id == id);
        }
    }
}
