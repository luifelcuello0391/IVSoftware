using IVSoftware.Data.Models;
using IVSoftware.Web.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IVSoftware.Web.Controllers
{
    public class EvaluationQuestionBanksController : Controller
    {
        private readonly IEntityService<EvaluationQuestionBank, Guid> _evaluationQuestionBankService;

        public EvaluationQuestionBanksController(
            IEntityService<EvaluationQuestionBank, Guid> evaluationQuestionBankService)
        {
            _evaluationQuestionBankService = evaluationQuestionBankService;
        }

        // GET: EvaluationQuestionBanks
        public async Task<IActionResult> Index()
        {
            return View(await _evaluationQuestionBankService.GetAllAsync());
        }

        // GET: EvaluationQuestionBanks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EvaluationQuestionBanks/Create
        // To protect from over-posting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EvaluationQuestionBank model)
        {
            if (ModelState.IsValid)
            {
                EvaluationQuestionBank evaluationQuestionBank =
                    await _evaluationQuestionBankService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: EvaluationQuestionBanks/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            EvaluationQuestionBank evaluationQuestionBank =
                await _evaluationQuestionBankService.GetByIdAsync(id);
            if (evaluationQuestionBank == null)
            {
                return NotFound();
            }

            return View(evaluationQuestionBank);
        }

        // POST: EvaluationQuestionBanks/Edit/5
        // To protect from over-posting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EvaluationQuestionBank model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    EvaluationQuestionBank evaluationQuestionBank =
                        await _evaluationQuestionBankService.UpdateAsync(model);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (!await EvaluationQuestionBankExists(model.Id))
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

        // GET: EvaluationQuestionBanks/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            EvaluationQuestionBank evaluationQuestionBank =
                await _evaluationQuestionBankService.GetByIdAsync(id);
            if (evaluationQuestionBank == null)
            {
                return NotFound();
            }

            return View(evaluationQuestionBank);
        }

        // POST: EvaluationQuestionBanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            EvaluationQuestionBank evaluationQuestionBank =
                await _evaluationQuestionBankService.GetByIdAsync(id);
            if (evaluationQuestionBank == null)
            {
                return NotFound();
            }

            await _evaluationQuestionBankService.DeleteAsync(evaluationQuestionBank);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EvaluationQuestionBankExists(Guid id)
        {
            return (await _evaluationQuestionBankService.GetByIdAsync(id)) != null;
        }
    }
}