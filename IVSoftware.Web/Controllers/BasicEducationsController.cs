using IVSoftware.Data.Models;
using IVSoftware.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IVSoftware.Web.Controllers
{
    public class BasicEducationsController : Controller
    {
        private readonly IEntityService<BasicEducation, Guid> _basicEducationService;
        private readonly IEntityService<Person, Guid> _personService;

        public BasicEducationsController(IEntityService<BasicEducation, Guid> basicEducationService,
            IEntityService<Person, Guid> personService)
        {
            _basicEducationService = basicEducationService;
            _personService = personService;
        }

        // GET: BasicEducationsController/Create
        public async Task<ActionResult> Create(Guid personId)
        {
            var person = await _personService.GetByIdAsync(personId);
            if(person == null)
            {
                return NotFound();
            }

            BasicEducation basicEducation = new BasicEducation()
            {
                PersonId = person.Id,
                Person = person,
                DegreeDate = DateTime.Today
            };

            return View(basicEducation);
        }

        // POST: BasicEducationsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BasicEducation model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _basicEducationService.CreateAsync(model);
                    return RedirectToAction("Edit", "People", new { id = model.PersonId });
                }

                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: BasicEducationsController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var basicEducation = await _basicEducationService.GetByIdAndIncludeAsync(id, be => be.Person);
            if(basicEducation == null) { return NotFound(); }

            return View(basicEducation);
        }

        // POST: BasicEducationsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, BasicEducation model)
        {
            if (id != model.Id){ return NotFound(); }

            try
            {
                if (ModelState.IsValid)
                {
                    await _basicEducationService.UpdateAsync(model);
                    return RedirectToAction("Edit", "People", new { id = model.PersonId });
                }

                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: BasicEducationsController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var basicEducation = await _basicEducationService.GetByIdAndIncludeAsync(id, be => be.Person);
            if (basicEducation == null) { return NotFound(); }

            return View(basicEducation);
        }

        // POST: BasicEducationsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, BasicEducation model)
        {
            if (id != model.Id) { return NotFound(); }

            try
            {
                var basicEducation = await _basicEducationService.GetByIdAndIncludeAsync(id, be => be.Person);
                if (basicEducation == null) { return NotFound(); }
                await _basicEducationService.DeleteAsync(basicEducation);
                return RedirectToAction("Edit", "People", new { id = model.PersonId });
            }
            catch
            {
                return View(model);
            }
        }
    }
}