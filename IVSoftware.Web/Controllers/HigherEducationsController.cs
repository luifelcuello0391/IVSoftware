using IVSoftware.Data.Models;
using IVSoftware.Web.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.Controllers
{
    public class HigherEducationsController : Controller
    {
        private readonly IEntityService<HigherEducation, Guid> _higherEducationService;
        private readonly IEntityService<Person, Guid> _personService;
        private readonly IEntityService<AcademicLevel, int> _academicLevelService;

        public HigherEducationsController(IEntityService<HigherEducation, Guid> higherEducationService,
            IEntityService<Person, Guid> personService,
            IEntityService<AcademicLevel, int> academicLevelService)
        {
            _higherEducationService = higherEducationService;
            _personService = personService;
            _academicLevelService = academicLevelService;
        }

        // GET: HigherEducationsController/Create
        public async Task<ActionResult> Create(Guid personId)
        {
            var person = await _personService.GetByIdAsync(personId);
            if (person == null)
            {
                return NotFound();
            }

            HigherEducation higherEducation = new HigherEducation()
            {
                PersonId = person.Id,
                Person = person,
                DegreeDate = DateTime.Today
            };

            ViewBag.AcademicLevels = await GetAcademicLevelsSelectList();

            return View(higherEducation);
        }

        // POST: HigherEducationsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HigherEducation model)
        {
            ViewBag.AcademicLevels = await GetAcademicLevelsSelectList();

            try
            {
                if (ModelState.IsValid)
                {
                    await _higherEducationService.CreateAsync(model);
                    return RedirectToAction("Edit", "People", new { id = model.PersonId });
                }

                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: HigherEducationsController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            ViewBag.AcademicLevels = await GetAcademicLevelsSelectList();
            var higherEducation = await _higherEducationService.GetByIdAndIncludeAsync(id, be => be.Person);
            if (higherEducation == null) { return NotFound(); }

            return View(higherEducation);
        }

        // POST: HigherEducationsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, HigherEducation model)
        {
            if (id != model.Id) { return NotFound(); }
            ViewBag.AcademicLevels = await GetAcademicLevelsSelectList();

            try
            {
                if (ModelState.IsValid)
                {
                    await _higherEducationService.UpdateAsync(model);
                    return RedirectToAction("Edit", "People", new { id = model.PersonId });
                }

                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: HigherEducationsController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            ViewBag.AcademicLevels = await GetAcademicLevelsSelectList();
            var higherEducation = await _higherEducationService.GetByIdAndIncludeAsync(id, be => be.Person);
            if (higherEducation == null) { return NotFound(); }

            return View(higherEducation);
        }

        // POST: HigherEducationsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, HigherEducation model)
        {
            if (id != model.Id) { return NotFound(); }
            ViewBag.AcademicLevels = await GetAcademicLevelsSelectList();

            try
            {
                var higherEducation = await _higherEducationService.GetByIdAndIncludeAsync(id, be => be.Person);
                if (higherEducation == null) { return NotFound(); }
                await _higherEducationService.DeleteAsync(higherEducation);
                return RedirectToAction("Edit", "People", new { id = model.PersonId });
            }
            catch
            {
                return View(model);
            }
        }

        private async Task<List<SelectListItem>> GetAcademicLevelsSelectList()
        {
            var academicLevels = new List<SelectListItem>();
            academicLevels = (await _academicLevelService.GetAllAsync()).Select(t => new SelectListItem()
            {
                Text = t.Name,
                Value = t.Id.ToString()
            }).ToList();

            return academicLevels;
        }
    }
}