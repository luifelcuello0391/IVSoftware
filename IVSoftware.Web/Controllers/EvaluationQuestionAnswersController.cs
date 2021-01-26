using IVSoftware.Data.Models;
using IVSoftware.Web.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IVSoftware.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class EvaluationQuestionAnswersController : Controller
    {
        private readonly IEntityService<EvaluationQuestionAnswer, Guid> _questionAnswerService;

        public EvaluationQuestionAnswersController(IEntityService<EvaluationQuestionAnswer, Guid> questionAnswerService)
        {
            _questionAnswerService = questionAnswerService;
        }

        // GET: EvaluationQuestionAnswers
        public async Task<IActionResult> Index()
        {
            return View(await _questionAnswerService.GetAllAsync());
        }

        // GET: EvaluationQuestionAnswers/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            EvaluationQuestionAnswer evaluationQuestionAnswer = await _questionAnswerService.GetByIdAsync(id);
            if (evaluationQuestionAnswer == null)
            {
                return NotFound();
            }

            return View(evaluationQuestionAnswer);
        }

        // GET: EvaluationQuestionAnswers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EvaluationQuestionAnswers/Create
        // To protect from over-posting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EvaluationQuestionAnswer model)
        {
            if (ModelState.IsValid)
            {
                EvaluationQuestionAnswer evaluationQuestionAnswer = await _questionAnswerService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: EvaluationQuestionAnswers/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            EvaluationQuestionAnswer evaluationQuestionAnswer = await _questionAnswerService.GetByIdAsync(id);
            if (evaluationQuestionAnswer == null)
            {
                return NotFound();
            }

            return View(evaluationQuestionAnswer);
        }

        // POST: EvaluationQuestionAnswers/Edit/5
        // To protect from over-posting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EvaluationQuestionAnswer model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    EvaluationQuestionAnswer evaluationQuestionAnswer =
                        await _questionAnswerService.UpdateAsync(model);

                    return RedirectToAction(nameof(Edit),
                        "EvaluationQuestionBanks",
                        new { id = model.EvaluationQuestionBankId}
                    );
                }
                catch (Exception ex)
                {
                    if (!await QuestionAnswerExists(model.Id))
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

        // GET: EvaluationQuestionAnswers/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            EvaluationQuestionAnswer evaluationQuestionAnswer = await _questionAnswerService.GetByIdAsync(id);
            if (evaluationQuestionAnswer == null)
            {
                return NotFound();
            }

            return View(evaluationQuestionAnswer);
        }

        // POST: EvaluationQuestionAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            EvaluationQuestionAnswer evaluationQuestionAnswer =
                await _questionAnswerService.GetByIdAsync(id);

            if (evaluationQuestionAnswer == null)
            {
                return NotFound();
            }

            await _questionAnswerService.DeleteAsync(evaluationQuestionAnswer);
            return RedirectToAction(nameof(Edit),
                "EvaluationQuestionBanks",
                new { id = evaluationQuestionAnswer.EvaluationQuestionBankId }
            );
        }

        private async Task<bool> QuestionAnswerExists(Guid id)
        {
            return (await _questionAnswerService.GetByIdAsync(id)) != null;
        }
    }
}