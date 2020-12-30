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
    public class VariableModelsController : Controller
    {
        private readonly IVSoftwareContext _context;

        public VariableModelsController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: VariableModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.VariableModel.ToListAsync());
        }

        // GET: VariableModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var variableModel = await _context.VariableModel.FirstOrDefaultAsync(m => m.Id == id);

            if (variableModel == null)
            {
                return NotFound();
            }

            return View(variableModel);
        }

        // GET: VariableModels/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                ViewBag.MeasurementUnits = await _context.MeasurementUnitModel.ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error on Create.View >> " + ex.ToString());
            }            

            return View();
        }

        // POST: VariableModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Name,CreationDatetime,ModificationDatetime,RegisterStatus,MeasurementUnitId,SelectedMeasurementUnitId")] VariableModel variableModel)
        {
            if (ModelState.IsValid)
            {
                MeasurementUnitModel unit = null;

                if (variableModel.SelectedMeasurementUnitId > 0)
                {
                    unit = await _context.MeasurementUnitModel.FindAsync(variableModel.SelectedMeasurementUnitId);
                }

                variableModel.MeasurementUnit = unit;

                _context.Add(variableModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(variableModel);
        }

        // GET: VariableModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            List<MeasurementUnitModel> units = null;
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                units = await _context.MeasurementUnitModel.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on Create.View >> " + ex.ToString());
            }

            var variableModel = await _context.VariableModel.FindAsync(id);
            if (variableModel == null)
            {
                return NotFound();
            }

            ViewBag.MeasurementUnits = units;

            if(variableModel.MeasurementUnit != null)
            {
                variableModel.SelectedMeasurementUnitId = variableModel.MeasurementUnit.Id;
            }

            return View(variableModel);
        }

        // POST: VariableModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Name,CreationDatetime,ModificationDatetime,RegisterStatus,MeasurementUnitId,SelectedMeasurementUnitId")] VariableModel variableModel)
        {
            if (id != variableModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    MeasurementUnitModel unit = null;

                    if (variableModel.SelectedMeasurementUnitId > 0)
                    {
                        unit = await _context.MeasurementUnitModel.FindAsync(variableModel.SelectedMeasurementUnitId);
                    }

                    variableModel.MeasurementUnit = unit;

                    _context.Update(variableModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VariableModelExists(variableModel.Id))
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
            return View(variableModel);
        }

        // GET: VariableModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var variableModel = await _context.VariableModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (variableModel == null)
            {
                return NotFound();
            }

            return View(variableModel);
        }

        // POST: VariableModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var variableModel = await _context.VariableModel.FindAsync(id);
            _context.VariableModel.Remove(variableModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VariableModelExists(int id)
        {
            return _context.VariableModel.Any(e => e.Id == id);
        }
    }
}
