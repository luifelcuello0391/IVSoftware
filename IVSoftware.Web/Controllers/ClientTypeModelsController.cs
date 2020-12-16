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
    public class ClientTypeModelsController : Controller
    {
        private readonly IVSoftwareContext _context;

        public ClientTypeModelsController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: ClientTypeModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.ClientTypeModel.ToListAsync());
        }

        // GET: ClientTypeModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientTypeModel = await _context.ClientTypeModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientTypeModel == null)
            {
                return NotFound();
            }

            return View(clientTypeModel);
        }

        // GET: ClientTypeModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClientTypeModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MustPayServices,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] ClientTypeModel clientTypeModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientTypeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clientTypeModel);
        }

        // GET: ClientTypeModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientTypeModel = await _context.ClientTypeModel.FindAsync(id);
            if (clientTypeModel == null)
            {
                return NotFound();
            }

            try
            {
                ViewBag.Incentives = await _context.IncentiveModel.ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error on ClientTypeModel.Edit.Incentives >> " + ex.ToString());
            }

            return View(clientTypeModel);
        }

        // POST: ClientTypeModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MustPayServices,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] ClientTypeModel clientTypeModel)
        {
            if (id != clientTypeModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientTypeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientTypeModelExists(clientTypeModel.Id))
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
            return View(clientTypeModel);
        }

        public async Task<IActionResult> AssignIncentives(string incentiveIds, int _id)
        {
            if (incentiveIds != null && !string.IsNullOrEmpty(incentiveIds.Replace(" ", string.Empty)))
            {
                var clientTypeModel = await _context.ClientTypeModel.FindAsync(_id);

                if (clientTypeModel == null)
                {
                    return NotFound();
                }

                string[] incentives = incentiveIds.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                if (incentives.Length > 0)
                {
                    bool added = false;

                    foreach (string id in incentives)
                    {
                        try
                        {
                            int indentiveId = int.Parse(id);

                            bool found = false;

                            if (clientTypeModel != null && clientTypeModel.Incentives != null && clientTypeModel.Incentives.Count > 0)
                            {
                                found = clientTypeModel.Incentives.FirstOrDefault<ClientTypeIncentiveRelation>(x => x.IncentiveId == indentiveId) != null;
                            }

                            if (!found)
                            {
                                IncentiveModel incentive = await _context.IncentiveModel.FirstOrDefaultAsync<IncentiveModel>(x => x.Id == indentiveId);

                                if (incentive != null)
                                {
                                    if (clientTypeModel.Incentives == null)
                                    {
                                        clientTypeModel.Incentives = new List<ClientTypeIncentiveRelation>();
                                    }

                                    ClientTypeIncentiveRelation relation = new ClientTypeIncentiveRelation();
                                    relation.IncentiveId = incentive.Id;
                                    relation.Incentive = incentive;

                                    relation.ClientTypeId = clientTypeModel.Id;
                                    relation.ClientType = clientTypeModel;

                                    clientTypeModel.Incentives.Add(relation);
                                    added = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error on AssignIncentives >> " + ex.ToString());
                        }
                    }

                    if (added)
                    {
                        try
                        {
                            _context.Update(clientTypeModel);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!ClientTypeModelExists(clientTypeModel.Id))
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

            return RedirectToAction(nameof(Edit), "ClientTypeModels", new { id = _id });
        }

        public async Task<IActionResult> UnassignIncentive(int incentiveId, int _id)
        {
            if (_id > 0)
            {
                var clientTypeModel = await _context.ClientTypeModel.FindAsync(_id);

                if (clientTypeModel == null)
                {
                    return NotFound();
                }

                if (clientTypeModel.Incentives != null && clientTypeModel.Incentives.Count > 0)
                {
                    ClientTypeIncentiveRelation incentive = clientTypeModel.Incentives.FirstOrDefault<ClientTypeIncentiveRelation>(x => x.Id == incentiveId);
                    if (incentive != null)
                    {
                        clientTypeModel.Incentives.Remove(incentive);

                        try
                        {
                            _context.Update(clientTypeModel);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!ClientTypeModelExists(clientTypeModel.Id))
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

            return RedirectToAction(nameof(Edit), "ClientTypeModels", new { id = _id });
        }

        // GET: ClientTypeModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientTypeModel = await _context.ClientTypeModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientTypeModel == null)
            {
                return NotFound();
            }

            return View(clientTypeModel);
        }

        // POST: ClientTypeModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientTypeModel = await _context.ClientTypeModel.FindAsync(id);
            _context.ClientTypeModel.Remove(clientTypeModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientTypeModelExists(int id)
        {
            return _context.ClientTypeModel.Any(e => e.Id == id);
        }
    }
}
