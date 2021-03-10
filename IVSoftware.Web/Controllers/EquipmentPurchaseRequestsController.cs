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
using Microsoft.AspNetCore.Identity;

namespace IVSoftware.Web.Controllers
{
    public class EquipmentPurchaseRequestsController : Controller
    {
        private readonly IVSoftwareContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IEntityService<Person, Guid> _personService;

        public EquipmentPurchaseRequestsController(IVSoftwareContext context, UserManager<User> userManager, IEntityService<Person, Guid> PersonService)
        {
            this._userManager = userManager;
            this._personService = PersonService;
            _context = context;
        }

        // GET: EquipmentPurchaseRequests
        public async Task<IActionResult> Index(string filter = "All")
        {
            ViewBag.filter = filter;

            switch(filter)
            {
                case "1": // News
                    var iVSoftwareContextNew = _context.EquipmentPurchaseRequests.Include(e => e.ResponsibleAnalyst).Include(e => e.StatusChangePerson).Where(x => x.RequestStatus == 1).OrderByDescending(x => x.RequestDate);
                    return View(await iVSoftwareContextNew.ToListAsync());

                case "2": // Approved
                    var iVSoftwareContextApproved = _context.EquipmentPurchaseRequests.Include(e => e.ResponsibleAnalyst).Include(e => e.StatusChangePerson).Where(x => x.RequestStatus == 2).OrderByDescending(x => x.RequestDate);
                    return View(await iVSoftwareContextApproved.ToListAsync());

                case "3": // Rejected
                    var iVSoftwareContextRejected = _context.EquipmentPurchaseRequests.Include(e => e.ResponsibleAnalyst).Include(e => e.StatusChangePerson).Where(x => x.RequestStatus == 3).OrderByDescending(x => x.RequestDate);
                    return View(await iVSoftwareContextRejected.ToListAsync());

                default: // All
                    var iVSoftwareContext = _context.EquipmentPurchaseRequests.Include(e => e.ResponsibleAnalyst).Include(e => e.StatusChangePerson).OrderByDescending(x => x.RequestDate);
                    return View(await iVSoftwareContext.ToListAsync());
            }
        }

        // GET: EquipmentPurchaseRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentPurchaseRequest = await _context.EquipmentPurchaseRequests
                .Include(e => e.ResponsibleAnalyst)
                .Include(e => e.StatusChangePerson)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipmentPurchaseRequest == null)
            {
                return NotFound();
            }

            return View(equipmentPurchaseRequest);
        }

        // GET: EquipmentPurchaseRequests/Create
        public IActionResult Create()
        {
            ViewData["AnalystPersonId"] = new SelectList(_context.Set<Person>(), "Id", "Email");
            ViewData["ResponsePersonId"] = new SelectList(_context.Set<Person>(), "Id", "Email");
            return View();
        }

        // POST: EquipmentPurchaseRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequestDate,EquipmentName,Parameters,Location,Justification,AnalystPersonId,WorkRange,Voltage,Accuracy,MinimumRead,NetworkCompatibility,Frequency,EnvironmentConditions,Accessories,RequiresLearning,HourlyIntensity,Warranty,Observations,RequestStatus,RequestStatusDate,ResponsePersonId,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] EquipmentPurchaseRequest equipmentPurchaseRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipmentPurchaseRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnalystPersonId"] = new SelectList(_context.Set<Person>(), "Id", "Email", equipmentPurchaseRequest.AnalystPersonId);
            ViewData["ResponsePersonId"] = new SelectList(_context.Set<Person>(), "Id", "Email", equipmentPurchaseRequest.ResponsePersonId);
            return View(equipmentPurchaseRequest);
        }

        // GET: EquipmentPurchaseRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentPurchaseRequest = await _context.EquipmentPurchaseRequests.FindAsync(id);
            if (equipmentPurchaseRequest == null)
            {
                return NotFound();
            }
            ViewData["AnalystPersonId"] = new SelectList(_context.Set<Person>(), "Id", "Email", equipmentPurchaseRequest.AnalystPersonId);
            ViewData["ResponsePersonId"] = new SelectList(_context.Set<Person>(), "Id", "Email", equipmentPurchaseRequest.ResponsePersonId);
            return View(equipmentPurchaseRequest);
        }

        // POST: EquipmentPurchaseRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RequestDate,EquipmentName,Parameters,Location,Justification,AnalystPersonId,WorkRange,Voltage,Accuracy,MinimumRead,NetworkCompatibility,Frequency,EnvironmentConditions,Accessories,RequiresLearning,HourlyIntensity,Warranty,Observations,RequestStatus,RequestStatusDate,ResponsePersonId,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] EquipmentPurchaseRequest equipmentPurchaseRequest)
        {
            if (id != equipmentPurchaseRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipmentPurchaseRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentPurchaseRequestExists(equipmentPurchaseRequest.Id))
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
            ViewData["AnalystPersonId"] = new SelectList(_context.Set<Person>(), "Id", "Email", equipmentPurchaseRequest.AnalystPersonId);
            ViewData["ResponsePersonId"] = new SelectList(_context.Set<Person>(), "Id", "Email", equipmentPurchaseRequest.ResponsePersonId);
            return View(equipmentPurchaseRequest);
        }

        // GET: EquipmentPurchaseRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentPurchaseRequest = await _context.EquipmentPurchaseRequests
                .Include(e => e.ResponsibleAnalyst)
                .Include(e => e.StatusChangePerson)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipmentPurchaseRequest == null)
            {
                return NotFound();
            }

            return View(equipmentPurchaseRequest);
        }

        // POST: EquipmentPurchaseRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipmentPurchaseRequest = await _context.EquipmentPurchaseRequests.FindAsync(id);
            _context.EquipmentPurchaseRequests.Remove(equipmentPurchaseRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentPurchaseRequestExists(int id)
        {
            return _context.EquipmentPurchaseRequests.Any(e => e.Id == id);
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

        public async Task<string> AproveRequest (int id)
        {
            try
            {
                if(id > 0)
                {
                    EquipmentPurchaseRequest request = await _context.EquipmentPurchaseRequests.FirstOrDefaultAsync(x => x.Id == id && x.RegisterStatus > 0);
                    if(request != null)
                    {
                        request.RequestStatus = 2;
                        request.RequestStatusDate = DateTime.Now;

                        User currentUser = await _userManager.GetUserAsync(User);
                        if(currentUser != null)
                        {
                            request.ResponsePersonId = Guid.Parse(currentUser.Id);
                            request.StatusChangePerson = (await _personService.FindByConditionAsync(x => x.UserId != null && x.UserId.Equals(currentUser.Id))).FirstOrDefault();
                        }

                        try
                        {
                            _context.Update(request);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException cex)
                        {
                            if (!EquipmentPurchaseRequestExists(request.Id))
                            {
                                return "Error: Request not found";
                            }
                            else
                            {
                                return "Error: " + cex.ToString(); 
                            }
                        }
                    }

                    return "Ok";
                }

                return "Error";
            }
            catch(Exception ex)
            {
                return "Error: " + ex.ToString();
            }
        }

        public async Task<string> RejectRequest(int id)
        {
            try
            {
                if (id > 0)
                {
                    EquipmentPurchaseRequest request = await _context.EquipmentPurchaseRequests.FirstOrDefaultAsync(x => x.Id == id && x.RegisterStatus > 0);
                    if (request != null)
                    {
                        request.RequestStatus = 3;
                        request.RequestStatusDate = DateTime.Now;

                        User currentUser = await _userManager.GetUserAsync(User);
                        if (currentUser != null)
                        {
                            request.ResponsePersonId = Guid.Parse(currentUser.Id);
                            request.StatusChangePerson = (await _personService.FindByConditionAsync(x => x.UserId != null && x.UserId.Equals(currentUser.Id))).FirstOrDefault();
                        }

                        try
                        {
                            _context.Update(request);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException cex)
                        {
                            if (!EquipmentPurchaseRequestExists(request.Id))
                            {
                                return "Error: Request not found";
                            }
                            else
                            {
                                return "Error: " + cex.ToString();
                            }
                        }
                    }

                    return "Ok";
                }

                return "Error";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.ToString();
            }
        }
    }
}
