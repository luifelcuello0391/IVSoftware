using IVSoftware.Data.Models;
using IVSoftware.Web.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IVSoftware.Web.Controllers
{
    public class OtherTechnicalKnowledgesController : Controller
    {
        private readonly IEntityService<OtherTechnicalKnowledge, Guid> _otherTechnicalKnowledgeService;
        private readonly IEntityService<Person, Guid> _personService;

        public OtherTechnicalKnowledgesController(IEntityService<OtherTechnicalKnowledge, Guid> otherTechnicalKnowledgeService,
            IEntityService<Person, Guid> personService)
        {
            _otherTechnicalKnowledgeService = otherTechnicalKnowledgeService;
            _personService = personService;
        }

        // GET: OtherTechnicalKnowledges/Create
        public async Task<ActionResult> Create(Guid personId)
        {
            var person = await _personService.GetByIdAsync(personId);
            if (person == null)
            {
                return NotFound();
            }

            OtherTechnicalKnowledge otherTechnicalKnowledge = new OtherTechnicalKnowledge()
            {
                PersonId = person.Id,
                Person = person
            };

            return View(otherTechnicalKnowledge);
        }

        // POST: OtherTechnicalKnowledges/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OtherTechnicalKnowledge model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _otherTechnicalKnowledgeService.CreateAsync(model);
                    return RedirectToAction("Edit", "People", new { id = model.PersonId });
                }

                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: OtherTechnicalKnowledges/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var otherTechnicalKnowledge = await _otherTechnicalKnowledgeService.GetByIdAndIncludeAsync(id, be => be.Person);
            if (otherTechnicalKnowledge == null) { return NotFound(); }

            return View(otherTechnicalKnowledge);
        }

        // POST: OtherTechnicalKnowledges/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, OtherTechnicalKnowledge model)
        {
            if (id != model.Id) { return NotFound(); }

            try
            {
                if (ModelState.IsValid)
                {
                    await _otherTechnicalKnowledgeService.UpdateAsync(model);
                    return RedirectToAction("Edit", "People", new { id = model.PersonId });
                }

                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: OtherTechnicalKnowledges/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var otherTechnicalKnowledge = await _otherTechnicalKnowledgeService.GetByIdAndIncludeAsync(id, be => be.Person);
            if (otherTechnicalKnowledge == null) { return NotFound(); }

            return View(otherTechnicalKnowledge);
        }

        // POST: OtherTechnicalKnowledges/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, OtherTechnicalKnowledge model)
        {
            if (id != model.Id) { return NotFound(); }

            try
            {
                var otherTechnicalKnowledge = await _otherTechnicalKnowledgeService.GetByIdAndIncludeAsync(id, be => be.Person);
                if (otherTechnicalKnowledge == null) { return NotFound(); }
                await _otherTechnicalKnowledgeService.DeleteAsync(otherTechnicalKnowledge);
                return RedirectToAction("Edit", "People", new { id = model.PersonId });
            }
            catch
            {
                return View(model);
            }
        }
    }
}