using IVSoftware.Data.Models;
using IVSoftware.Web.Models;
using IVSoftware.Web.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.Controllers
{
    public class TechnicalKnowledgesController : Controller
    {
        private readonly IEntityService<TechnicalKnowledge, Guid> _technicalKnowledgeService;
        private readonly IEntityService<Person, Guid> _personService;
        private readonly IVSoftwareContext _context;

        public TechnicalKnowledgesController(IEntityService<TechnicalKnowledge, Guid> technicalKnowledgeService,
            IEntityService<Person, Guid> personService,
            IVSoftwareContext context)
        {
            _technicalKnowledgeService = technicalKnowledgeService;
            _personService = personService;
            _context = context;
        }

        // GET: TechnicalKnowledges/Create
        public async Task<ActionResult> Create(Guid personId)
        {
            var person = await _personService.GetByIdAsync(personId);
            if (person == null)
            {
                return NotFound();
            }

            ViewBag.Services = await GetServiceSelectList();
            ViewBag.Matrix = await GetMatrixSelectList();
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
            ViewBag.Services = await GetServiceSelectList();
            ViewBag.Matrix = await GetMatrixSelectList();
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
            ViewBag.Services = await GetServiceSelectList();
            ViewBag.Matrix = await GetMatrixSelectList();

            return View(technicalKnowledge);
        }

        // POST: TechnicalKnowledges/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, TechnicalKnowledge model)
        {
            if (id != model.Id) { return NotFound(); }
            ViewBag.Services = await GetServiceSelectList();
            ViewBag.Matrix = await GetMatrixSelectList();
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
            ViewBag.Services = await GetServiceSelectList();
            ViewBag.Matrix = await GetMatrixSelectList();

            return View(technicalKnowledge);
        }

        // POST: TechnicalKnowledges/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, TechnicalKnowledge model)
        {
            if (id != model.Id) { return NotFound(); }
            ViewBag.Services = await GetServiceSelectList();
            ViewBag.Matrix = await GetMatrixSelectList();
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

        private async Task<List<SelectListItem>> GetServiceSelectList()
        {
            var services = new List<SelectListItem>();
            services = await _context.ServiceModel.Select(t => new SelectListItem()
            {
                Text = t.Name,
                Value = t.Id.ToString()
            }).ToListAsync();

            return services;
        }

        private async Task<List<SelectListItem>> GetMatrixSelectList()
        {
            var matrix = new List<SelectListItem>();
            matrix = await _context.MatrixModel.Select(t => new SelectListItem()
            {
                Text = t.Name,
                Value = t.Id.ToString()
            }).ToListAsync();

            return matrix;
        }
    }
}