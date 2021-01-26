using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IVSoftware.Data.Models;
using IVSoftware.Web.Models;

namespace IVSoftware.Web.Controllers
{
    public class MaintenanceModelsController : Controller
    {
        private readonly IVSoftwareContext _context;

        public MaintenanceModelsController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: MaintenanceModels
        public async Task<IActionResult> Index()
        {
            var iVSoftwareContext = _context.Maintenances.Include(m => m.Equip).Include(m => m.ServiceProvider);
            return View(await iVSoftwareContext.ToListAsync());
        }

        // GET: MaintenanceModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceModel = await _context.Maintenances
                .Include(m => m.Equip)
                .Include(m => m.ServiceProvider)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maintenanceModel == null)
            {
                return NotFound();
            }

            return View(maintenanceModel);
        }

        // GET: MaintenanceModels/Create
        public IActionResult Create()
        {
            ViewData["EquipId"] = new SelectList(_context.Equipment, "Id", "Name");
            ViewData["ProviderId"] = new SelectList(_context.Provider, "Id", "Name");
            return View();
        }

        // POST: MaintenanceModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeOfMaintenanceId,EquipId,MaintenanceDate,Diagnostic,WorkExecuted,NextCalibrationDate,Observations,ProviderId,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] MaintenanceModel maintenanceModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maintenanceModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipId"] = new SelectList(_context.Equipment, "Id", "Name", maintenanceModel.EquipId);
            ViewData["ProviderId"] = new SelectList(_context.Provider, "Id", "Name", maintenanceModel.ProviderId);
            return View(maintenanceModel);
        }

        // GET: MaintenanceModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceModel = await _context.Maintenances.FindAsync(id);
            if (maintenanceModel == null)
            {
                return NotFound();
            }
            ViewData["EquipId"] = new SelectList(_context.Equipment, "Id", "Name", maintenanceModel.EquipId);
            ViewData["ProviderId"] = new SelectList(_context.Provider, "Id", "Name", maintenanceModel.ProviderId);
            return View(maintenanceModel);
        }

        // POST: MaintenanceModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeOfMaintenanceId,EquipId,MaintenanceDate,Diagnostic,WorkExecuted,NextCalibrationDate,Observations,ProviderId,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] MaintenanceModel maintenanceModel)
        {
            if (id != maintenanceModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maintenanceModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaintenanceModelExists(maintenanceModel.Id))
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
            ViewData["EquipId"] = new SelectList(_context.Equipment, "Id", "Name", maintenanceModel.EquipId);
            ViewData["ProviderId"] = new SelectList(_context.Provider, "Id", "Name", maintenanceModel.ProviderId);
            return View(maintenanceModel);
        }

        // GET: MaintenanceModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceModel = await _context.Maintenances
                .Include(m => m.Equip)
                .Include(m => m.ServiceProvider)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maintenanceModel == null)
            {
                return NotFound();
            }

            return View(maintenanceModel);
        }

        // POST: MaintenanceModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maintenanceModel = await _context.Maintenances.FindAsync(id);
            _context.Maintenances.Remove(maintenanceModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaintenanceModelExists(int id)
        {
            return _context.Maintenances.Any(e => e.Id == id);
        }
    }
}
