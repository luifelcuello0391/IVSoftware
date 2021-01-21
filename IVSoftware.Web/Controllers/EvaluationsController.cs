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
    public class EvaluationsController : Controller
    {
        private readonly IEntityService<Evaluation, int> _evaluationService;
        private readonly IEntityService<Periodicity, int> _periodicityService;
        private readonly IEntityService<Person, Guid> _personService;

        public EvaluationsController(IEntityService<Evaluation, int> evaluationService,
            IEntityService<Periodicity, int> periodicityService,
            IEntityService<Person, Guid> personService)
        {
            _evaluationService = evaluationService;
            _periodicityService = periodicityService;
            _personService = personService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _evaluationService.GetAllAsync());
        }

        // GET: Evaluations/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Periodicity = await GetPeriodicityList();
            return View();
        }

        // POST: Evaluations/Create
        // To protect from over-posting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Evaluation model)
        {
            if (ModelState.IsValid)
            {
                Evaluation evaluation = await _evaluationService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Periodicity = await GetPeriodicityList();
            return View(model);
        }

        // GET: Evaluations/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Evaluation evaluation = await _evaluationService.GetByIdAsync(id);
            if (evaluation == null)
            {
                return NotFound();
            }

            ViewBag.Periodicity = await GetPeriodicityList();
            return View(evaluation);
        }

        // POST: Evaluations/Edit/5
        // To protect from over-posting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Evaluation model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            ViewBag.Periodicity = await GetPeriodicityList();
            if (ModelState.IsValid)
            {
                try
                {
                    Evaluation evaluation = await _evaluationService.UpdateAsync(model);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (!await EvaluationExists(model.Id))
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

        // GET: Evaluations/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Evaluation evaluation = await _evaluationService.GetByIdAsync(id);
            if (evaluation == null)
            {
                return NotFound();
            }

            ViewBag.Periodicity = await GetPeriodicityList();
            return View(evaluation);
        }

        // POST: Evaluations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Evaluation evaluation = await _evaluationService.GetByIdAsync(id);
            if (evaluation == null)
            {
                return NotFound();
            }

            await _evaluationService.DeleteAsync(evaluation);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AssignPeople(int id)
        {
            return PartialView("_ModalAssignPeople", await _personService.GetAllAsync());
        }

        private async Task<bool> EvaluationExists(int id)
        {
            return (await _evaluationService.GetByIdAsync(id)) != null;
        }

        private async Task<List<SelectListItem>> GetPeriodicityList()
        {
            var periodicities = (await _periodicityService.GetAllAsync()).Select(t => new SelectListItem()
            {
                Text = t.Name,
                Value = t.Id.ToString()
            }).ToList();

            return periodicities;
        }
    }
}