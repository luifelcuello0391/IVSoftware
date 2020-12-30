using IVSoftware.Data.Models;
using IVSoftware.Web.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IVSoftware.Web.Controllers
{
    public class TechnicalKnowledgesController : Controller
    {
        private readonly IEntityService<TechnicalKnowledge, Guid> _technicalKnowledgeService;
        private readonly IEntityService<Person, Guid> _personService;

        public TechnicalKnowledgesController(IEntityService<TechnicalKnowledge, Guid> technicalKnowledgeService,
            IEntityService<Person, Guid> personService)
        {
            _technicalKnowledgeService = technicalKnowledgeService;
            _personService = personService;
        }

        // GET: TechnicalKnowledges/Create
        public async Task<ActionResult> Create(Guid personId)
        {
            var person = await _personService.GetByIdAsync(personId);
            if (person == null)
            {
                return NotFound();
            }

            TechnicalKnowledge technicalKnowledge = new TechnicalKnowledge()
            {
                PersonId = person.Id,
                Person = person
            };

            return View(technicalKnowledge);
        }

        // POST: TechnicalKnowledges/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TechnicalKnowledge model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _technicalKnowledgeService.CreateAsync(model);
                    return RedirectToAction("Edit", "People", new { id = model.PersonId });
                }

                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: TechnicalKnowledges/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var technicalKnowledge = await _technicalKnowledgeService.GetByIdAndIncludeAsync(id, be => be.Person);
            if (technicalKnowledge == null) { return NotFound(); }

            return View(technicalKnowledge);
        }

        // POST: TechnicalKnowledges/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, TechnicalKnowledge model)
        {
            if (id != model.Id) { return NotFound(); }

            try
            {
                if (ModelState.IsValid)
                {
                    await _technicalKnowledgeService.UpdateAsync(model);
                    return RedirectToAction("Edit", "People", new { id = model.PersonId });
                }

                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: TechnicalKnowledges/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var technicalKnowledge = await _technicalKnowledgeService.GetByIdAndIncludeAsync(id, be => be.Person);
            if (technicalKnowledge == null) { return NotFound(); }

            return View(technicalKnowledge);
        }

        // POST: TechnicalKnowledges/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, TechnicalKnowledge model)
        {
            if (id != model.Id) { return NotFound(); }

            try
            {
                var technicalKnowledge = await _technicalKnowledgeService.GetByIdAndIncludeAsync(id, be => be.Person);
                if (technicalKnowledge == null) { return NotFound(); }
                await _technicalKnowledgeService.DeleteAsync(technicalKnowledge);
                return RedirectToAction("Edit", "People", new { id = model.PersonId });
            }
            catch
            {
                return View(model);
            }
        }
    }
}