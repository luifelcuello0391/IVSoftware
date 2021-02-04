﻿using IVSoftware.Data.Models;
using IVSoftware.Web.Data;
using IVSoftware.Web.Models;
using IVSoftware.Web.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.Controllers
{
    [Authorize]
    public class EvaluationsController : Controller
    {
        private readonly IEntityService<Evaluation, int> _evaluationService;
        private readonly IEntityService<Periodicity, int> _periodicityService;
        private readonly IEntityService<Person, Guid> _personService;
        private readonly IEntityService<EvaluationQuestionBank, Guid> _questionService;
        private readonly IEntityService<PersonEvaluation, Guid> _personEvaluationService;
        private readonly IVSoftwareContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMailService _mailService;

        public EvaluationsController(IEntityService<Evaluation, int> evaluationService,
            IEntityService<Periodicity, int> periodicityService,
            IEntityService<Person, Guid> personService,
            IEntityService<EvaluationQuestionBank, Guid> questionService,
            IEntityService<PersonEvaluation, Guid> personEvaluationService,
            IVSoftwareContext context,
            UserManager<User> userManager,
            IMailService mailService)
        {
            _evaluationService = evaluationService;
            _periodicityService = periodicityService;
            _personService = personService;
            _questionService = questionService;
            _personEvaluationService = personEvaluationService;
            _context = context;
            _userManager = userManager;
            _mailService = mailService;
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

            var resultPersonEvaluation = evaluation.PersonEvaluations.Select(pe => new PersonEvaluation()
            {
                Evaluation = pe.Evaluation,
                EvaluationId = pe.EvaluationId,
                IsApproved = pe.IsApproved,
                Person = pe.Person,
                PersonId = pe.PersonId,
                ResultJson = pe.ResultJson,
                Date = pe.Date,
                Score = pe.Score,
                Id = pe.Id
            });

            evaluation.PersonEvaluations = resultPersonEvaluation.ToList();
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
            List<PersonEvaluation> currentPeople =
                (await _personEvaluationService.FindByConditionAsync(pe => pe.EvaluationId == id)).ToList();
            IEnumerable<Person> people = await _personService.GetAllAsync();
            List<PersonEvaluation> personEvaluations = people.Select(p => new PersonEvaluation()
            {
                EvaluationId = id,
                PersonId = p.Id,
                Person = p,
                IsSelected = currentPeople.Any(cp => cp.PersonId == p.Id)
            }).ToList();

            PeopleEvaluationVM peopleEvaluationVM = new PeopleEvaluationVM()
            {
                Id = id,
                PersonEvaluations = personEvaluations
            };

            return PartialView("_ModalAssignPeople", peopleEvaluationVM);
        }

        [HttpPost]
        public async Task<IActionResult> AssignPeople(PeopleEvaluationVM model)
        {
            try
            {
                if (model.Id > 0)
                {
                    bool commitChanges = false;
                    List<PersonEvaluation> currentPeople =
                        (await _personEvaluationService.FindByConditionAsync(pe => pe.EvaluationId == model.Id)).ToList();

                    List<PersonEvaluation> newPeople = model.PersonEvaluations != null ?
                        model.PersonEvaluations.ToList() : new List<PersonEvaluation>();

                    List<PersonEvaluation> personEvaluationToDelete =
                        currentPeople.Where(p => !newPeople.Any(np => np.PersonId == p.PersonId)).ToList();

                    List<PersonEvaluation> personEvaluationToAdd =
                        newPeople.Where(p => !currentPeople.Any(cp => cp.PersonId == p.PersonId)).ToList();

                    if (personEvaluationToDelete != null && personEvaluationToDelete.Count > 0)
                    {
                        _context.PersonEvaluations.RemoveRange(personEvaluationToDelete);
                        commitChanges = true;
                    }

                    if (personEvaluationToAdd != null && personEvaluationToAdd.Count > 0)
                    {
                        _context.PersonEvaluations.AddRange(personEvaluationToAdd);
                        commitChanges = true;
                    }

                    if (commitChanges)
                    {
                        await _context.SaveChangesAsync();
                        foreach(PersonEvaluation personEvaluation in personEvaluationToAdd)
                        {
                            Person person = await _personService.GetByIdAsync(personEvaluation.PersonId);
                            Evaluation evaluation = await _evaluationService.GetByIdAsync(personEvaluation.EvaluationId);
                            if (person != null && !string.IsNullOrEmpty(person.Email))
                            {
                                MailRequest request = new MailRequest()
                                {
                                    ToEmail = personEvaluation.Person.Email,
                                    Subject = "Evaluación Asignada",
                                    Body = $"Se le ha asignado la evaluación: {evaluation?.Name}"
                                };

                                await _mailService.SendEmailAsync(request);
                            }
                        }
                    }

                    return RedirectToAction(nameof(Edit), new { id = model.Id });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("PersonEvaluations", ex.Message);
            }

            return PartialView("_ModalAssignPeople", model);
        }

        public async Task<IActionResult> AssignQuestions(int id)
        {
            List<QuestionEvaluation> currentQuestions =
                        _context.QuestionEvaluations.Where(pe => pe.EvaluationId == id).ToList();
            IEnumerable<EvaluationQuestionBank> questions =
                await _questionService.GetAllAsync();
            List<QuestionEvaluation> questionsEvaluation = questions.Select(p => new QuestionEvaluation()
            {
                EvaluationId = id,
                QuestionId = p.Id,
                Question = p,
                IsSelected = currentQuestions.Any(cp => cp.QuestionId == p.Id)
            }).ToList();

            QuestionsEvaluationVM questionsEvaluationVM = new QuestionsEvaluationVM()
            {
                Id = id,
                QuestionsEvaluation = questionsEvaluation
            };

            return PartialView("_ModalAssignQuestions", questionsEvaluationVM);
        }

        [HttpPost]
        public async Task<IActionResult> AssignQuestions(QuestionsEvaluationVM model)
        {
            try
            {
                if (model.Id > 0)
                {
                    bool commitChanges = false;
                    List<QuestionEvaluation> currentQuestions =
                        _context.QuestionEvaluations.Where(pe => pe.EvaluationId == model.Id).ToList();

                    List<QuestionEvaluation> newQuestions = model.QuestionsEvaluation != null ?
                        model.QuestionsEvaluation.ToList() : new List<QuestionEvaluation>();

                    List<QuestionEvaluation> questionsEvaluationToDelete =
                        currentQuestions.Where(p => !newQuestions.Any(np => np.QuestionId == p.QuestionId)).ToList();

                    List<QuestionEvaluation> questionsEvaluationToAdd =
                        newQuestions.Where(p => !currentQuestions.Any(cp => cp.QuestionId == p.QuestionId)).ToList();

                    if (questionsEvaluationToDelete != null && questionsEvaluationToDelete.Count > 0)
                    {
                        _context.QuestionEvaluations.RemoveRange(questionsEvaluationToDelete);
                        commitChanges = true;
                    }

                    if (questionsEvaluationToAdd != null && questionsEvaluationToAdd.Count > 0)
                    {
                        _context.QuestionEvaluations.AddRange(questionsEvaluationToAdd);
                        commitChanges = true;
                    }

                    if (commitChanges) { await _context.SaveChangesAsync(); }

                    return RedirectToAction(nameof(Edit), new { id = model.Id });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("QuestionsEvaluation", ex.Message);
            }

            return PartialView("_ModalAssignQuestions", model);
        }

        public async Task<IActionResult> MyEvaluations()
        {
            User user = await _userManager.GetUserAsync(User);
            if(user == null) { return NotFound(); }

            Person person = (await _personService.FindByConditionAsync(p => p.UserId == user.Id)).FirstOrDefault();
            if (person == null) { return NotFound(); }

            if(person.PersonEvaluations != null)
            {
                var result = person.PersonEvaluations.Select(pe => new PersonEvaluation()
                {
                    Evaluation = pe.Evaluation,
                    EvaluationId = pe.EvaluationId,
                    IsApproved = pe.IsApproved,
                    Person = pe.Person,
                    PersonId = pe.PersonId,
                    ResultJson = pe.ResultJson,
                    Date = pe.Date,
                    Score = pe.Score,
                    Id = pe.Id
                });
                return View(result);
            }

            return View(new List<PersonEvaluation>());
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                User user = await _userManager.GetUserAsync(User);
                if (user == null) { return NotFound(); }

                Person person = (await _personService.FindByConditionAsync(p => p.UserId == user.Id)).FirstOrDefault();
                if (person == null) { return NotFound(); }

                PersonEvaluation evaluation =
                    (
                        await _personEvaluationService
                            .FindByConditionAsync(pe => pe.EvaluationId == id && pe.PersonId == person.Id)
                    ).FirstOrDefault();

                if (evaluation == null) { return NotFound(); }

                if (string.IsNullOrEmpty(evaluation.ResultJson)) { return NotFound(); }

                EvaluationVM evaluationVM = JsonConvert.DeserializeObject<EvaluationVM>(evaluation.ResultJson);

                return View(evaluationVM);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Name", ex.Message);
                return View(new EvaluationVM());
            }
        }

        public async Task<IActionResult> Start(int id)
        {
            Evaluation evaluation = await _evaluationService.GetByIdAsync(id);
            if(evaluation == null) { return NotFound(); }

            return View(evaluation);
        }

        [HttpPost]
        public async Task<string> Start(EvaluationVM evaluation)
        {
            try
            {
                User user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return JsonConvert.SerializeObject(
                        new
                        {
                            result = 1,
                            message = "No fue encontrado el usuario autenticado"
                        });
                }

                Person person = (await _personService.FindByConditionAsync(p => p.UserId == user.Id)).FirstOrDefault();
                if (person == null)
                {
                    return JsonConvert.SerializeObject(
                        new
                        {
                            result = 1,
                            message = "No fue encontrada la persona"
                        });
                }

                PersonEvaluation personEvaluation =
                (
                    await _personEvaluationService.FindByConditionAsync(
                        pe => pe.EvaluationId == evaluation.Id &&
                        pe.PersonId == person.Id &&
                        string.IsNullOrEmpty(pe.ResultJson)
                    )
                ).FirstOrDefault();
                if (personEvaluation == null)
                {
                    return JsonConvert.SerializeObject(
                        new {
                            result = 1,
                            message = "No fue encontrada la asignación de la evaluación a la persona " + person.FullName
                        });
                }

                personEvaluation.ResultJson = JsonConvert.SerializeObject(evaluation);
                personEvaluation.Date = DateTime.Now;
                personEvaluation.Score = EvaluationResult(personEvaluation.ResultJson);
                personEvaluation.IsApproved = personEvaluation.Score >= evaluation.PercentageToPass;
                _context.PersonEvaluations.Update(personEvaluation);
                await _context.SaveChangesAsync();

                return JsonConvert.SerializeObject(new { result = 0, message = "OK" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { result = 1, message = ex.Message });
            }
        }

        public async Task<IActionResult> Reschedule(Guid id)
        {
            PersonEvaluation personEvaluation =
                await _personEvaluationService.GetByIdAsync(id);
            if(personEvaluation == null) { return NotFound(); }

            return PartialView("_ModalReschedule", personEvaluation);
        }

        [HttpPost]
        public async Task<IActionResult> Reschedule(RescheduleVM model)
        {
            PersonEvaluation personEvaluation = await _personEvaluationService.GetByIdAsync(model.Id);
            if (personEvaluation != null)
            {
                if(model.Date > personEvaluation.Date)
                {
                    try
                    {
                        PersonEvaluation newPersonEvaluation = new PersonEvaluation()
                        {
                            Date = model.Date,
                            EvaluationId = personEvaluation.EvaluationId,
                            PersonId = personEvaluation.PersonId
                        };

                        await _personEvaluationService.CreateAsync(newPersonEvaluation);
                        if (personEvaluation.Person != null && !string.IsNullOrEmpty(personEvaluation.Person.Email))
                        {
                            MailRequest request = new MailRequest()
                            {
                                ToEmail = personEvaluation.Person.Email,
                                Subject = "Evaluación Reprogramada",
                                Body = $"Se le ha reprogramado la evaluación: {personEvaluation.Evaluation?.Name}"
                            };

                            await _mailService.SendEmailAsync(request);
                        }

                        return RedirectToAction(nameof(Edit), new { id = personEvaluation.EvaluationId });
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("Id", ex.Message);
                    }
                }
                else
                {
                    ModelState.AddModelError("Id", "La fecha debe ser mayor a la inicial");
                }
            }
            else
            {
                ModelState.AddModelError("Id", "Asignación no encontrada");
            }

            return PartialView("_ModalReschedule", personEvaluation);
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

        private int EvaluationResult(string resultJson)
        {
            try
            {
                if (string.IsNullOrEmpty(resultJson)) { return 0; }

                EvaluationVM evaluationVM = JsonConvert.DeserializeObject<EvaluationVM>(resultJson);
                var rightAnswers = evaluationVM.Questions.Select(q => q.Answers.Where(a => a.IsSelected && a.IsRight)).ToList();
                var selectedAnswers = evaluationVM.Questions.Select(q => q.Answers.Where(a => a.IsSelected)).ToList();

                int rights = 0;
                for (int i = 0; i < rightAnswers.Count; i++)
                {
                    if (rightAnswers.ElementAtOrDefault(i).Count() == selectedAnswers.ElementAtOrDefault(i).Count())
                    {
                        rights += 1;
                    }
                }

                int PercentageToPass = (rights * 100) / evaluationVM.Questions.Count;

                return PercentageToPass;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}