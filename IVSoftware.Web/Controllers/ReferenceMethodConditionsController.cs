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
    public class ReferenceMethodConditionsController : Controller
    {
        private readonly IVSoftwareContext _context;

        public ReferenceMethodConditionsController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: ReferenceMethodConditions
        public async Task<IActionResult> Index()
        {
            return View(await _context.ReferenceMethodCondition.ToListAsync());
        }

        // GET: ReferenceMethodConditions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var referenceMethodCondition = await _context.ReferenceMethodCondition
                .FirstOrDefaultAsync(m => m.Id == id);
            if (referenceMethodCondition == null)
            {
                return NotFound();
            }

            return View(referenceMethodCondition);
        }

        // GET: ReferenceMethodConditions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReferenceMethodConditions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,QuotationCode,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] ReferenceMethodCondition referenceMethodCondition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(referenceMethodCondition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(referenceMethodCondition);
        }

        // GET: ReferenceMethodConditions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var referenceMethodCondition = await _context.ReferenceMethodCondition.FindAsync(id);
            if (referenceMethodCondition == null)
            {
                return NotFound();
            }
            return View(referenceMethodCondition);
        }

        // POST: ReferenceMethodConditions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,QuotationCode,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] ReferenceMethodCondition referenceMethodCondition)
        {
            if (id != referenceMethodCondition.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(referenceMethodCondition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReferenceMethodConditionExists(referenceMethodCondition.Id))
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
            return View(referenceMethodCondition);
        }

        // GET: ReferenceMethodConditions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var referenceMethodCondition = await _context.ReferenceMethodCondition
                .FirstOrDefaultAsync(m => m.Id == id);
            if (referenceMethodCondition == null)
            {
                return NotFound();
            }

            return View(referenceMethodCondition);
        }

        // POST: ReferenceMethodConditions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var referenceMethodCondition = await _context.ReferenceMethodCondition.FindAsync(id);
            _context.ReferenceMethodCondition.Remove(referenceMethodCondition);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReferenceMethodConditionExists(int id)
        {
            return _context.ReferenceMethodCondition.Any(e => e.Id == id);
        }
    }
}
