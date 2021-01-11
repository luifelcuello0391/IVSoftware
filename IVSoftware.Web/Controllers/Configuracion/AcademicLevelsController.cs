using IVSoftware.Data.Models;
using IVSoftware.Web.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IVSoftware.Web.Controllers.Configuracion
{
    public class AcademicLevelsController : Controller
    {
        private readonly IEntityService<AcademicLevel, int> _academicLevelService;

        public AcademicLevelsController(IEntityService<AcademicLevel, int> academicLevelService)
        {
            _academicLevelService = academicLevelService;
        }

        // GET: AcademicLevels
        public async Task<IActionResult> Index()
        {
            return View(await _academicLevelService.GetAllAsync());
        }

        // GET: AcademicLevels/Details/5
        public async Task<IActionResult> Details(int id)
        {
            AcademicLevel academicLevel = await _academicLevelService.GetByIdAsync(id);
            if (academicLevel == null)
            {
                return NotFound();
            }

            return View(academicLevel);
        }

        // GET: AcademicLevels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AcademicLevels/Create
        // To protect from over-posting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AcademicLevel model)
        {
            if (ModelState.IsValid)
            {
                AcademicLevel academicLevel = await _academicLevelService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: AcademicLevels/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            AcademicLevel academicLevel = await _academicLevelService.GetByIdAsync(id);
            if (academicLevel == null)
            {
                return NotFound();
            }

            return View(academicLevel);
        }

        // POST: AcademicLevels/Edit/5
        // To protect from over-posting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AcademicLevel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    AcademicLevel academicLevel = await _academicLevelService.UpdateAsync(model);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (!await AcademicLevelExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError("Name", ex.Message);
                        return View(model);
                    }
                }
            }

            return View(model);
        }

        // GET: AcademicLevels/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            AcademicLevel academicLevel = await _academicLevelService.GetByIdAsync(id);
            if (academicLevel == null)
            {
                return NotFound();
            }

            return View(academicLevel);
        }

        // POST: AcademicLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            AcademicLevel academicLevel = await _academicLevelService.GetByIdAsync(id);
            if (academicLevel == null)
            {
                return NotFound();
            }

            await _academicLevelService.DeleteAsync(academicLevel);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AcademicLevelExists(int id)
        {
            return (await _academicLevelService.GetByIdAsync(id)) != null;
        }
    }
}