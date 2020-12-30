using IVSoftware.Data.Models;
using IVSoftware.Web.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IVSoftware.Web.Controllers
{
    public class WorkExperiencesController : Controller
    {
        private readonly IEntityService<WorkExperience, Guid> _workExperienceService;
        private readonly IEntityService<Person, Guid> _personService;

        public WorkExperiencesController(IEntityService<WorkExperience, Guid> workExperienceService,
            IEntityService<Person, Guid> personService)
        {
            _workExperienceService = workExperienceService;
            _personService = personService;
        }

        // GET: WorkExperiences/Create
        public async Task<ActionResult> Create(Guid personId)
        {
            var person = await _personService.GetByIdAsync(personId);
            if (person == null)
            {
                return NotFound();
            }

            WorkExperience workExperience = new WorkExperience()
            {
                PersonId = person.Id,
                Person = person,
                StartDate = DateTime.Today
            };

            return View(workExperience);
        }

        // POST: WorkExperiences/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(WorkExperience model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _workExperienceService.CreateAsync(model);
                    return RedirectToAction("Edit", "People", new { id = model.PersonId });
                }

                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: WorkExperiences/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var workExperience = await _workExperienceService.GetByIdAndIncludeAsync(id, be => be.Person);
            if (workExperience == null) { return NotFound(); }

            return View(workExperience);
        }

        // POST: WorkExperiences/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, WorkExperience model)
        {
            if (id != model.Id) { return NotFound(); }

            try
            {
                if (ModelState.IsValid)
                {
                    await _workExperienceService.UpdateAsync(model);
                    return RedirectToAction("Edit", "People", new { id = model.PersonId });
                }

                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: WorkExperiences/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var workExperience = await _workExperienceService.GetByIdAndIncludeAsync(id, be => be.Person);
            if (workExperience == null) { return NotFound(); }

            return View(workExperience);
        }

        // POST: WorkExperiences/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, WorkExperience model)
        {
            if (id != model.Id) { return NotFound(); }

            try
            {
                var workExperience = await _workExperienceService.GetByIdAndIncludeAsync(id, be => be.Person);
                if (workExperience == null) { return NotFound(); }
                await _workExperienceService.DeleteAsync(workExperience);
                return RedirectToAction("Edit", "People", new { id = model.PersonId });
            }
            catch
            {
                return View(model);
            }
        }
    }
}