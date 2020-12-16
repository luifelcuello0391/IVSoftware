using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IVSoftware.Web.Models;
using IVSoftware.Models;

namespace IVSoftware.Web.Controllers
{
    public class ServiceGroupModelsController : Controller
    {
        private readonly IVSoftwareContext _context;

        public ServiceGroupModelsController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: ServiceGroupModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.ServiceGroupModel.ToListAsync());
        }

        // GET: ServiceGroupModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceGroupModel = await _context.ServiceGroupModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceGroupModel == null)
            {
                return NotFound();
            }

            return View(serviceGroupModel);
        }

        // GET: ServiceGroupModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ServiceGroupModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Description,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] ServiceGroupModel serviceGroupModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceGroupModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceGroupModel);
        }

        // GET: ServiceGroupModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                ViewBag.Services = await _context.ServiceModel.ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error on ServiceGroupModel.Edit >> " + ex.ToString());
            }

            var serviceGroupModel = await _context.ServiceGroupModel.FindAsync(id);
            if (serviceGroupModel == null)
            {
                return NotFound();
            }
            return View(serviceGroupModel);
        }

        public async Task<IActionResult> AssignServices(string servicesId, int _id)
        {
            if (servicesId != null && !string.IsNullOrEmpty(servicesId.Replace(" ", string.Empty)))
            {
                var group = await _context.ServiceGroupModel.FindAsync(_id);

                if (group == null)
                {
                    return NotFound();
                }

                string[] sections = servicesId.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                if (sections.Length > 0)
                {
                    bool added = false;

                    foreach (string id in sections)
                    {
                        try
                        {
                            int serviceId = int.Parse(id);

                            bool found = false;

                            if (group != null && group.Services != null && group.Services.Count > 0)
                            {
                                found = group.Services.FirstOrDefault<ServiceGroupServicesRelation>(x => x.ServiceId == serviceId) != null;
                            }

                            if (!found)
                            {
                                ServiceModel service = await _context.ServiceModel.FirstOrDefaultAsync<ServiceModel>(x => x.Id == serviceId);

                                if (service != null)
                                {
                                    if (group.Services == null)
                                    {
                                        group.Services = new List<ServiceGroupServicesRelation>();
                                    }

                                    ServiceGroupServicesRelation relation = new ServiceGroupServicesRelation();
                                    relation.ServiceId = service.Id;
                                    relation.Service = service;

                                    relation.ServiceGroupId = group.Id;
                                    relation.ServiceGroup = group;

                                    group.Services.Add(relation);
                                    added = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error on AssignServices >> " + ex.ToString());
                        }
                    }

                    if (added)
                    {
                        try
                        {
                            _context.Update(group);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!ServiceGroupModelExists(group.Id))
                            {
                                return NotFound();
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                }
            }

            return RedirectToAction(nameof(Edit), "ServiceGroupModels", new { id = _id });
        }

        public async Task<IActionResult> UnassignService(int service, int _id)
        {
            if (_id > 0)
            {
                var group = await _context.ServiceGroupModel.FindAsync(_id);

                if (group == null)
                {
                    return NotFound();
                }

                if (group.Services != null && group.Services.Count > 0)
                {
                    ServiceGroupServicesRelation serviceData = group.Services.FirstOrDefault<ServiceGroupServicesRelation>(x => x.ServiceId == service);
                    if (serviceData != null)
                    {
                        group.Services.Remove(serviceData);

                        try
                        {
                            _context.Update(group);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!ServiceGroupModelExists(group.Id))
                            {
                                return NotFound();
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                }
            }
            else
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Edit), "ServiceGroupModels", new { id = _id });
        }

        // POST: ServiceGroupModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Description,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] ServiceGroupModel serviceGroupModel)
        {
            if (id != serviceGroupModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceGroupModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceGroupModelExists(serviceGroupModel.Id))
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
            return View(serviceGroupModel);
        }

        // GET: ServiceGroupModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceGroupModel = await _context.ServiceGroupModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceGroupModel == null)
            {
                return NotFound();
            }

            return View(serviceGroupModel);
        }

        // POST: ServiceGroupModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceGroupModel = await _context.ServiceGroupModel.FindAsync(id);
            _context.ServiceGroupModel.Remove(serviceGroupModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult ServiceSelection()
        {
            return View();
        }

        private bool ServiceGroupModelExists(int id)
        {
            return _context.ServiceGroupModel.Any(e => e.Id == id);
        }
    }
}
