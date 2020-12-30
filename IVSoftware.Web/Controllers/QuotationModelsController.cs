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
    public class QuotationModelsController : Controller
    {
        private readonly IVSoftwareContext _context;

        public QuotationModelsController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: QuotationModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.QuotationModel.ToListAsync());
        }

        // GET: QuotationModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotationModel = await _context.QuotationModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quotationModel == null)
            {
                return NotFound();
            }

            return View(quotationModel);
        }

        // GET: QuotationModels/Create
        public IActionResult Create(int? id)
        {
            return View();
        }

        // POST: QuotationModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientId,ClientName,ClientAddress,ClientPhoneNumber,ClientContactName,ClientContactPosition,Greetings,Exceptions,Code,ReservationDate,ReservationMessage,MeasurementSubtotal,MeasurementTotalValue,ServicesSubtotal,ServicesTotalValue,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] QuotationModel quotationModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quotationModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quotationModel);
        }

        // GET: QuotationModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotationModel = await _context.QuotationModel.FindAsync(id);
            if (quotationModel == null)
            {
                return NotFound();
            }
            return View(quotationModel);
        }

        // POST: QuotationModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,ClientName,ClientAddress,ClientPhoneNumber,ClientContactName,ClientContactPosition,Greetings,Exceptions,Code,ReservationDate,ReservationMessage,MeasurementSubtotal,MeasurementTotalValue,ServicesSubtotal,ServicesTotalValue,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] QuotationModel quotationModel)
        {
            if (id != quotationModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quotationModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuotationModelExists(quotationModel.Id))
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
            return View(quotationModel);
        }

        // GET: QuotationModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotationModel = await _context.QuotationModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quotationModel == null)
            {
                return NotFound();
            }

            return View(quotationModel);
        }

        // POST: QuotationModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quotationModel = await _context.QuotationModel.FindAsync(id);
            _context.QuotationModel.Remove(quotationModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuotationModelExists(int id)
        {
            return _context.QuotationModel.Any(e => e.Id == id);
        }
    }
}
