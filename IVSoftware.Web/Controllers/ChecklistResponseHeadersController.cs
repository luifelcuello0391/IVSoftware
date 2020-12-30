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
    public class ChecklistResponseHeadersController : Controller
    {
        private readonly IVSoftwareContext _context;

        public ChecklistResponseHeadersController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: ChecklistResponseHeaders
        public async Task<IActionResult> Index(int? id)
        {
            try
            {
                ViewBag.CheckLists = await _context.EquipmentCheckList.ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error on ChecklistResponseHeader.Index >> " + ex.ToString());
            }

            if(id != null)
            {
                try
                {
                    ViewBag.Equipment = await _context.Equipment.FirstOrDefaultAsync<Equipment>(x => x.Id == id.Value);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error on ChecklistResponseHeadersController.Index >> " + ex.ToString());
                }

                return View(await _context.ChecklistResponseHeader.Where(x => x.Equipment != null && x.Equipment.Id == id.Value).ToListAsync());
            }
            else
            {
                return View(await _context.ChecklistResponseHeader.ToListAsync());
            }
        }

        // GET: ChecklistResponseHeaders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklistResponseHeader = await _context.ChecklistResponseHeader
                .FirstOrDefaultAsync(m => m.Id == id);
            if (checklistResponseHeader == null)
            {
                return NotFound();
            }

            return View(checklistResponseHeader);
        }

        public async Task<IActionResult> CaptureCheckListResponse(int equipId, int checklistId, string answers, string observation, string fillupDate)
        {
            if(equipId > 0 && checklistId > 0 && answers != null && fillupDate != null)
            {
                // TODO: Save answers

                ChecklistResponseHeader header = new ChecklistResponseHeader();
                header.EquipmentId = equipId;
                header.Equipment = await _context.Equipment.FirstOrDefaultAsync<Equipment>(x => x.Id == equipId);

                header.CheckListId = checklistId;
                header.CheckList = await _context.EquipmentCheckList.FirstOrDefaultAsync<EquipmentCheckList>(x => x.Id == checklistId);

                header.Observation = observation;

                try
                {
                    header.FillUpDate = DateTime.Parse(fillupDate);
                }
                catch(Exception ex)
                {
                    header.FillUpDate = DateTime.Now;
                }
                
            }

            return NotFound();
        }

        // GET: ChecklistResponseHeaders/Create
        public async Task<IActionResult> Create(int? _id, int? _equipId)
        {
            if (_id > 0)
            {
                if (_equipId > 0)
                {
                    try
                    {
                        ViewBag.Equipment = await _context.Equipment.FirstOrDefaultAsync<Equipment>(x => x.Id == _equipId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error on ChecklistResponseHeader.Create.Equipment >> " + ex.ToString());
                    }

                    try
                    {
                        ViewBag.CheckList = await _context.EquipmentCheckList.FirstOrDefaultAsync<EquipmentCheckList>(x => x.Id == _id);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error on ChecklistResponseHeader.Create.Checklist >> " + ex.ToString());
                    }
                }
            }

            return View();
        }

        // POST: ChecklistResponseHeaders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FillUpDate,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] ChecklistResponseHeader checklistResponseHeader)
        {
            if (ModelState.IsValid)
            {
                _context.Add(checklistResponseHeader);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(checklistResponseHeader);
        }

        // GET: ChecklistResponseHeaders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklistResponseHeader = await _context.ChecklistResponseHeader.FindAsync(id);
            if (checklistResponseHeader == null)
            {
                return NotFound();
            }
            return View(checklistResponseHeader);
        }

        // POST: ChecklistResponseHeaders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FillUpDate,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] ChecklistResponseHeader checklistResponseHeader)
        {
            if (id != checklistResponseHeader.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checklistResponseHeader);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChecklistResponseHeaderExists(checklistResponseHeader.Id))
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
            return View(checklistResponseHeader);
        }

        // GET: ChecklistResponseHeaders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklistResponseHeader = await _context.ChecklistResponseHeader
                .FirstOrDefaultAsync(m => m.Id == id);
            if (checklistResponseHeader == null)
            {
                return NotFound();
            }

            return View(checklistResponseHeader);
        }

        // POST: ChecklistResponseHeaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var checklistResponseHeader = await _context.ChecklistResponseHeader.FindAsync(id);
            _context.ChecklistResponseHeader.Remove(checklistResponseHeader);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChecklistResponseHeaderExists(int id)
        {
            return _context.ChecklistResponseHeader.Any(e => e.Id == id);
        }
    }
}
