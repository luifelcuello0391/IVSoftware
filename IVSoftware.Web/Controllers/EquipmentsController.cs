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
    public class EquipmentsController : Controller
    {
        private readonly IVSoftwareContext _context;

        public EquipmentsController(IVSoftwareContext context)
        {
            _context = context;
        }

        public async Task<string> GetProviderName (int id)
        {
            string result = string.Empty;
            if(id > 0)
            {
                Provider provider = await _context.Provider.FirstOrDefaultAsync<Provider>(x => x.Id == id);

                if(provider != null)
                {
                    result = provider.Name;
                }
            }
            return result;
        }

        // GET: Equipments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Equipment.ToListAsync());
        }

        // GET: Equipments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // GET: Equipments/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                ViewBag.Brands = await _context.Brand.ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error on EquipmentsController.Brands >> " + ex.ToString());
            }

            try
            {
                ViewBag.Providers = await _context.Provider.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on EquipmentsController.Providers >> " + ex.ToString());
            }

            return View();
        }

        // POST: Equipments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Model,Serie,PurchaseDate,Magnitude,Range,MinimumRead,Accuracy,PowerSupply,Observation,OtherRecomendation,Name,RegisterStatus,CreationDatetime,ModificationDatetime,SelectedBrandId,RequestedProviderId,PurchaseValue")] Equipment equipment)
        {
            if (ModelState.IsValid)
            {
                Brand brand = null;
                if (equipment.SelectedBrandId > 0)
                {
                    brand = await _context.Brand.FirstOrDefaultAsync<Brand>(x => x.Id == equipment.SelectedBrandId);
                }

                equipment.EquipBrand = brand;

                Provider provider = null;
                if(equipment.RequestedProviderId > 0)
                {
                    provider = await _context.Provider.FirstOrDefaultAsync<Provider>(x => x.Id == equipment.RequestedProviderId);
                }

                equipment.EquipProvider = provider;

                _context.Add(equipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(equipment);
        }

        // GET: Equipments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipment.FindAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }

            try
            {
                ViewBag.Brands = await _context.Brand.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on EquipmentsController.Brands >> " + ex.ToString());
            }

            try
            {
                ViewBag.Providers = await _context.Provider.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on EquipmentsController.Providers >> " + ex.ToString());
            }

            if(equipment.EquipBrand != null)
            {
                equipment.SelectedBrandId = equipment.EquipBrand.Id;
            }
            else
            {
                equipment.SelectedBrandId = 0;
            }

            if(equipment.EquipProvider != null)
            {
                equipment.RequestedProviderId = equipment.EquipProvider.Id;
            }
            else
            {
                equipment.RequestedProviderId = 0;
            }

            return View(equipment);
        }

        // POST: Equipments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Model,Serie,PurchaseDate,Magnitude,Range,MinimumRead,Accuracy,PowerSupply,Observation,OtherRecomendation,Name,RegisterStatus,CreationDatetime,ModificationDatetime,SelectedBrandId,RequestedProviderId,PurchaseValue")] Equipment equipment)
        {
            if (id != equipment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Brand brand = null;
                    if (equipment.SelectedBrandId > 0)
                    {
                        brand = await _context.Brand.FirstOrDefaultAsync<Brand>(x => x.Id == equipment.SelectedBrandId);
                    }

                    equipment.EquipBrand = brand;

                    Provider provider = null;
                    if (equipment.RequestedProviderId > 0)
                    {
                        provider = await _context.Provider.FirstOrDefaultAsync<Provider>(x => x.Id == equipment.RequestedProviderId);
                    }

                    equipment.EquipProvider = provider;

                    _context.Update(equipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentExists(equipment.Id))
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
            return View(equipment);
        }

        // GET: Equipments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // POST: Equipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipment = await _context.Equipment.FindAsync(id);
            _context.Equipment.Remove(equipment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentExists(int id)
        {
            return _context.Equipment.Any(e => e.Id == id);
        }
    }
}
