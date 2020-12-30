using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IVSoftware.Data.Models;
using IVSoftware.Web.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IVSoftware.Web.Controllers
{
    public class TrainingsController : Controller
    {
        private readonly IEntityService<Training, Guid> _trainingService;
        private readonly IEntityService<Person, Guid> _personService;
        private readonly IEntityService<CertificationType, int> _certificationTypeService;

        public TrainingsController(IEntityService<Training, Guid> trainingService,
            IEntityService<Person, Guid> personService,
            IEntityService<CertificationType, int> certificationTypeService)
        {
            _trainingService = trainingService;
            _personService = personService;
            _certificationTypeService = certificationTypeService;
        }

        // GET: TrainingsController/Create
        public async Task<ActionResult> Create(Guid personId)
        {
            var person = await _personService.GetByIdAsync(personId);
            if (person == null)
            {
                return NotFound();
            }

            Training training = new Training()
            {
                PersonId = person.Id,
                Person = person,
                EndDate = DateTime.Today
            };

            ViewBag.CertificationTypes = await GetCertificationTypesSelectList();

            return View(training);
        }

        // POST: TrainingsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Training model)
        {
            ViewBag.CertificationTypes = await GetCertificationTypesSelectList();

            try
            {
                if (ModelState.IsValid)
                {
                    await _trainingService.CreateAsync(model);
                    return RedirectToAction("Edit", "People", new { id = model.PersonId });
                }

                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: TrainingsController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            ViewBag.CertificationTypes = await GetCertificationTypesSelectList();
            var training = await _trainingService.GetByIdAndIncludeAsync(id, be => be.Person);
            if (training == null) { return NotFound(); }

            return View(training);
        }

        // POST: TrainingsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Training model)
        {
            if (id != model.Id) { return NotFound(); }
            ViewBag.CertificationTypes = await GetCertificationTypesSelectList();

            try
            {
                if (ModelState.IsValid)
                {
                    await _trainingService.UpdateAsync(model);
                    return RedirectToAction("Edit", "People", new { id = model.PersonId });
                }

                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: TrainingsController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            ViewBag.CertificationTypes = await GetCertificationTypesSelectList();
            var training = await _trainingService.GetByIdAndIncludeAsync(id, be => be.Person);
            if (training == null) { return NotFound(); }

            return View(training);
        }

        // POST: TrainingsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, Training model)
        {
            if (id != model.Id) { return NotFound(); }
            ViewBag.CertificationTypes = await GetCertificationTypesSelectList();

            try
            {
                var training = await _trainingService.GetByIdAndIncludeAsync(id, be => be.Person);
                if (training == null) { return NotFound(); }
                await _trainingService.DeleteAsync(training);
                return RedirectToAction("Edit", "People", new { id = model.PersonId });
            }
            catch
            {
                return View(model);
            }
        }

        private async Task<List<SelectListItem>> GetCertificationTypesSelectList()
        {
            var certificationTypes = new List<SelectListItem>();
            certificationTypes = (await _certificationTypeService.GetAllAsync()).Select(t => new SelectListItem()
            {
                Text = t.Name,
                Value = t.Id.ToString()
            }).ToList();

            return certificationTypes;
        }
    }
}