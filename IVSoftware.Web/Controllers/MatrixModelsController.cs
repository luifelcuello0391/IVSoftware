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
    public class MatrixModelsController : Controller
    {
        private readonly IVSoftwareContext _context;

        public MatrixModelsController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: MatrixModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.MatrixModel.ToListAsync());
        }

        // GET: MatrixModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matrixModel = await _context.MatrixModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matrixModel == null)
            {
                return NotFound();
            }

            return View(matrixModel);
        }

        // GET: MatrixModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MatrixModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,OnlyForServices,CreationDatetime,ModificationDatetime,RegisterStatus")] MatrixModel matrixModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(matrixModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(matrixModel);
        }

        // GET: MatrixModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matrixModel = await _context.MatrixModel.FindAsync(id);
            if (matrixModel == null)
            {
                return NotFound();
            }
            return View(matrixModel);
        }

        // POST: MatrixModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,OnlyForServices,CreationDatetime,ModificationDatetime,RegisterStatus")] MatrixModel matrixModel)
        {
            if (id != matrixModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matrixModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatrixModelExists(matrixModel.Id))
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
            return View(matrixModel);
        }

        // GET: MatrixModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matrixModel = await _context.MatrixModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matrixModel == null)
            {
                return NotFound();
            }

            return View(matrixModel);
        }

        // POST: MatrixModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var matrixModel = await _context.MatrixModel.FindAsync(id);
            _context.MatrixModel.Remove(matrixModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatrixModelExists(int id)
        {
            return _context.MatrixModel.Any(e => e.Id == id);
        }
    }
}
