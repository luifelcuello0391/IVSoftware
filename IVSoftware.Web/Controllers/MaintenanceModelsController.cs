using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IVSoftware.Data.Models;
using IVSoftware.Web.Models;
using IVSoftware.Web.Service;

namespace IVSoftware.Web.Controllers
{
    public class MaintenanceModelsController : Controller
    {
        private readonly IVSoftwareContext _context;
        private readonly IEntityService<Person, Guid> _personService;

        public MaintenanceModelsController(IVSoftwareContext context, IEntityService<Person, Guid> PersonService)
        {
            this._personService = PersonService;
            _context = context;
        }

        // GET: MaintenanceModels
        public async Task<IActionResult> Index(string filter = "All")
        {
            ViewBag.filter = filter;

            switch(filter)
            {
                case "M":
                    var iVSoftwareContextM = _context.Maintenances.Include(m => m.Equip).Include(m => m.ServiceProvider).Include(m => m.Responsable).Where(m => "M".Equals(m.TypeOfMaintenanceId));
                    return View(await iVSoftwareContextM.ToListAsync());

                case "C":
                    var iVSoftwareContextC = _context.Maintenances.Include(m => m.Equip).Include(m => m.ServiceProvider).Include(m => m.Responsable).Where(m => "C".Equals(m.TypeOfMaintenanceId));
                    return View(await iVSoftwareContextC.ToListAsync());

                default:
                    var iVSoftwareContext = _context.Maintenances.Include(m => m.Equip).Include(m => m.ServiceProvider).Include(m => m.Responsable);
                    return View(await iVSoftwareContext.ToListAsync());
            }
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
                .Include(m => m.Responsable)
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
        public async Task<IActionResult> Create([Bind("TypeOfMaintenanceId,EquipId,MaintenanceDate,Diagnostic,WorkExecuted,NextCalibrationDate,Observations,ProviderId,Name,RegisterStatus,CreationDatetime,ModificationDatetime, PersonId")] MaintenanceModel maintenanceModel)
        {
            if (ModelState.IsValid)
            {
                Equipment equipment = null;
                if(maintenanceModel.EquipId > 0)
                {
                    equipment = await _context.Equipment.FirstOrDefaultAsync<Equipment>(x => x.Id == maintenanceModel.EquipId && x.RegisterStatus > 0);
                }
                maintenanceModel.Equip = equipment;

                Provider provider = null;
                if(maintenanceModel.ProviderId > 0)
                {
                    provider = await _context.Provider.FirstOrDefaultAsync<Provider>(x => x.Id == maintenanceModel.ProviderId && x.RegisterStatus > 0);
                }
                maintenanceModel.ServiceProvider = provider;

                Person responsable = null;
                if(maintenanceModel.PersonId != null)
                {
                    IEnumerable<Person> people = await _personService.FindByConditionAsync(x => x.Id.ToString().Equals(maintenanceModel.PersonId.Value.ToString()));
                    if(people != null && people.Count() > 0)
                    {
                        responsable = people.FirstOrDefault();
                    }
                }
                maintenanceModel.Responsable = responsable;

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
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeOfMaintenanceId,EquipId,MaintenanceDate,Diagnostic,WorkExecuted,NextCalibrationDate,Observations,ProviderId,Name,RegisterStatus,CreationDatetime,ModificationDatetime, PersonId")] MaintenanceModel maintenanceModel)
        {
            if (id != maintenanceModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Equipment equipment = null;
                    if (maintenanceModel.EquipId > 0)
                    {
                        equipment = await _context.Equipment.FirstOrDefaultAsync<Equipment>(x => x.Id == maintenanceModel.EquipId && x.RegisterStatus > 0);
                    }
                    maintenanceModel.Equip = equipment;

                    Provider provider = null;
                    if (maintenanceModel.ProviderId > 0)
                    {
                        provider = await _context.Provider.FirstOrDefaultAsync<Provider>(x => x.Id == maintenanceModel.ProviderId && x.RegisterStatus > 0);
                    }
                    maintenanceModel.ServiceProvider = provider;

                    Person responsable = null;
                    if (maintenanceModel.PersonId != null)
                    {
                        IEnumerable<Person> people = await _personService.FindByConditionAsync(x => x.Id.ToString().Equals(maintenanceModel.PersonId.Value.ToString()));
                        if (people != null && people.Count() > 0)
                        {
                            responsable = people.FirstOrDefault();
                        }
                    }
                    maintenanceModel.Responsable = responsable;

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

        public async Task<IActionResult> GetPersons()
        {
            try
            {
                IEnumerable<Person> persons = await _personService.GetAllAsync();
                return PartialView("personSelectionList", persons);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on ServiceModelsController.GetPersons >> " + ex.ToString());
            }

            return null;
        }

        public async Task<IActionResult> GetProviders()
        {
            try
            {
                List<Provider> providers = await _context.Provider.ToListAsync();
                return PartialView("providerSelectionList", providers);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on ServiceModelsController.GetProviders >> " + ex.ToString());
            }

            return null;
        }

        public async Task<IActionResult> GetEquipments()
        {
            try
            {
                List<Equipment> equipments = await _context.Equipment.ToListAsync();
                return PartialView("equipmentSelectionList", equipments);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on ServiceModelsController.GetEquipments >> " + ex.ToString());
            }

            return null;
        }

        public async Task<IActionResult> GetPerson(string identification)
        {
            try
            {
                IEnumerable<Person> persons = await _personService.FindByConditionAsync(x => x.IdentificationNumber.Equals(identification));
                if (persons != null && persons.Count() > 0)
                {
                    return PartialView("ResponsableData", persons.First());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on MaintenanceModelsController.GetPerson >> " + ex.ToString());
            }

            return PartialView("ResponsableData", null);
        }

        public async Task<IActionResult> GetPersonByGuid(string identification)
        {
            try
            {
                IEnumerable<Person> persons = await _personService.FindByConditionAsync(x => x.Id.ToString().Equals(identification));
                if (persons != null && persons.Count() > 0)
                {
                    return PartialView("ResponsableData", persons.First());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on MaintenanceModelsController.GetPerson >> " + ex.ToString());
            }

            return PartialView("ResponsableData", null);
        }

        public async Task<IActionResult> GetProvider(int identification)
        {
            try
            {
                Provider provider = await _context.Provider.FirstOrDefaultAsync<Provider>(x => x.Id == identification && x.RegisterStatus > 0);
                if (provider != null)
                {
                    return PartialView("ProviderData", provider);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on MaintenanceModelsController.GetProvider >> " + ex.ToString());
            }

            return PartialView("ProviderData", null);
        }

        public async Task<IActionResult> GetEquipment(int identification)
        {
            try
            {
                Equipment equipment = await _context.Equipment.FirstOrDefaultAsync<Equipment>(x => x.Id == identification && x.RegisterStatus > 0);
                if (equipment != null)
                {
                    return PartialView("EquipmentData", equipment);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on MaintenanceModelsController.GetEquipment >> " + ex.ToString());
            }

            return PartialView("EquipmentData", null);
        }
    }
}
