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
    public class AirResourceMonitoringDeviceMaintenancesController : Controller
    {
        private readonly IVSoftwareContext _context;

        public AirResourceMonitoringDeviceMaintenancesController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: AirResourceMonitoringDeviceMaintenances
        public async Task<IActionResult> Index()
        {
            var iVSoftwareContext = _context.AirResourceMonitoringDevideMaintenances.Include(a => a.Equipment);
            return View(await iVSoftwareContext.ToListAsync());
        }

        // GET: AirResourceMonitoringDeviceMaintenances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airResourceMonitoringDeviceMaintenance = await _context.AirResourceMonitoringDevideMaintenances
                .Include(a => a.Equipment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (airResourceMonitoringDeviceMaintenance == null)
            {
                return NotFound();
            }

            return View(airResourceMonitoringDeviceMaintenance);
        }

        // GET: AirResourceMonitoringDeviceMaintenances/Create
        public IActionResult Create()
        {
            ViewData["EquipId"] = new SelectList(_context.Equipment, "Id", "Name");
            return View();
        }

        // POST: AirResourceMonitoringDeviceMaintenances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeOfMaintenanceId,EquipId,MaintenanceDate,ProviderName,ProviderIdentificaton,ProviderAddress,ProviderPhoneNumber,ProviderContactName,SparePartsChanged,NextMaintenanceDate,Observations,Location,Description,StockNumber,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] AirResourceMonitoringDeviceMaintenance airResourceMonitoringDeviceMaintenance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(airResourceMonitoringDeviceMaintenance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipId"] = new SelectList(_context.Equipment, "Id", "Name", airResourceMonitoringDeviceMaintenance.EquipId);
            return View(airResourceMonitoringDeviceMaintenance);
        }

        // GET: AirResourceMonitoringDeviceMaintenances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airResourceMonitoringDeviceMaintenance = await _context.AirResourceMonitoringDevideMaintenances.FindAsync(id);
            if (airResourceMonitoringDeviceMaintenance == null)
            {
                return NotFound();
            }
            ViewData["EquipId"] = new SelectList(_context.Equipment, "Id", "Name", airResourceMonitoringDeviceMaintenance.EquipId);
            return View(airResourceMonitoringDeviceMaintenance);
        }

        // POST: AirResourceMonitoringDeviceMaintenances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeOfMaintenanceId,EquipId,MaintenanceDate,ProviderName,ProviderIdentificaton,ProviderAddress,ProviderPhoneNumber,ProviderContactName,SparePartsChanged,NextMaintenanceDate,Observations,Location,Description,StockNumber,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] AirResourceMonitoringDeviceMaintenance airResourceMonitoringDeviceMaintenance)
        {
            if (id != airResourceMonitoringDeviceMaintenance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(airResourceMonitoringDeviceMaintenance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AirResourceMonitoringDeviceMaintenanceExists(airResourceMonitoringDeviceMaintenance.Id))
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
            ViewData["EquipId"] = new SelectList(_context.Equipment, "Id", "Name", airResourceMonitoringDeviceMaintenance.EquipId);
            return View(airResourceMonitoringDeviceMaintenance);
        }

        // GET: AirResourceMonitoringDeviceMaintenances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airResourceMonitoringDeviceMaintenance = await _context.AirResourceMonitoringDevideMaintenances
                .Include(a => a.Equipment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (airResourceMonitoringDeviceMaintenance == null)
            {
                return NotFound();
            }

            return View(airResourceMonitoringDeviceMaintenance);
        }

        // POST: AirResourceMonitoringDeviceMaintenances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var airResourceMonitoringDeviceMaintenance = await _context.AirResourceMonitoringDevideMaintenances.FindAsync(id);
            _context.AirResourceMonitoringDevideMaintenances.Remove(airResourceMonitoringDeviceMaintenance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AirResourceMonitoringDeviceMaintenanceExists(int id)
        {
            return _context.AirResourceMonitoringDevideMaintenances.Any(e => e.Id == id);
        }
    }
}
