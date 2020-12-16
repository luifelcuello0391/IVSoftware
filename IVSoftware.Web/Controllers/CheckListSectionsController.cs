using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IVSoftware.Web.Models;

namespace IVSoftware.Web.Controllers
{
    public class CheckListSectionsController : Controller
    {
        private readonly IVSoftwareContext _context;

        public CheckListSectionsController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: CheckListSections
        public async Task<IActionResult> Index()
        {
            return View(await _context.CheckListSection.Where(x => x.RegisterStatus >= 1).ToListAsync());
        }

        // GET: CheckListSections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkListSection = await _context.CheckListSection
                .FirstOrDefaultAsync(m => m.Id == id);
            if (checkListSection == null)
            {
                return NotFound();
            }

            if(checkListSection.QuestionSections != null && checkListSection.QuestionSections.Count > 0)
            {
                checkListSection.QuestionSections.OrderBy(x => x.Order);
            }

            return View(checkListSection);
        }

        // GET: CheckListSections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CheckListSections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] CheckListSection checkListSection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(checkListSection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(checkListSection);
        }

        public async Task<IActionResult> AssignQuestions (string questionIds, int _id)
        {
            if(questionIds != null && !string.IsNullOrEmpty(questionIds.Replace(" ", string.Empty)))
            {
                var checkListSection = await _context.CheckListSection.FindAsync(_id);

                if(checkListSection == null)
                {
                    return NotFound();
                }

                string[] questions = questionIds.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                if(questions.Length > 0)
                {
                    bool added = false;

                    foreach(string id in questions)
                    {
                        try
                        {
                            int questionId = int.Parse(id);

                            bool found = false;

                            if (checkListSection != null && checkListSection.QuestionSections != null && checkListSection.QuestionSections.Count > 0)
                            {
                                found = checkListSection.QuestionSections.FirstOrDefault<CheckListQuestionCheckListSection>(x => x.QuestionsId == questionId) != null;
                            }

                            if (!found)
                            {
                                CheckListQuestion question = await _context.CheckListQuestion.FirstOrDefaultAsync<CheckListQuestion>(x => x.Id == questionId);

                                if (question != null)
                                {
                                    if (checkListSection.QuestionSections == null)
                                    {
                                        checkListSection.QuestionSections = new List<CheckListQuestionCheckListSection>();
                                    }

                                    CheckListQuestionCheckListSection relation = new CheckListQuestionCheckListSection();
                                    relation.QuestionsId = question.Id;
                                    relation.CheckListQuestion = question;

                                    relation.SectionsId = checkListSection.Id;
                                    relation.CheckListSection = checkListSection;

                                    relation.Order = checkListSection.QuestionSections.Count + 1;

                                    checkListSection.QuestionSections.Add(relation);
                                    added = true;
                                }
                            }
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine("Error on AssignQuestions >> " + ex.ToString());
                        }
                    }

                    if(added)
                    {
                        try
                        {
                            _context.Update(checkListSection);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!CheckListSectionExists(checkListSection.Id))
                            {
                                return NotFound();
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                }
            }

            return RedirectToAction(nameof(Edit), "CheckListSections", new { id = _id });
        }

        public async Task<IActionResult> UnassignQuestion (int questionId, int _id)
        {
            if(_id > 0)
            {
                var checkListSection = await _context.CheckListSection.FindAsync(_id);

                if(checkListSection == null)
                {
                    return NotFound();
                }

                if (checkListSection.QuestionSections != null && checkListSection.QuestionSections.Count > 0)
                {
                    CheckListQuestionCheckListSection question = checkListSection.QuestionSections.FirstOrDefault<CheckListQuestionCheckListSection>(x => x.CheckListQuestionCheckListSectionId == questionId);
                    if (question != null)
                    {
                        checkListSection.QuestionSections.Remove(question);

                        try
                        {
                            _context.Update(checkListSection);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!CheckListSectionExists(checkListSection.Id))
                            {
                                return NotFound();
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                }
        }
            else
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Edit), "CheckListSections", new { id = _id });
        }

        // GET: CheckListSections/Edit/5
        //[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkListSection = await _context.CheckListSection.FindAsync(id);
            if (checkListSection == null)
            {
                return NotFound();
            }

            try
            {
                ViewBag.Questions = await _context.CheckListQuestion.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on Edit.Questions >> " + ex.ToString());
            }

            return View(checkListSection);
        }

        // POST: CheckListSections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] CheckListSection checkListSection)
        {
            if (id != checkListSection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checkListSection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CheckListSectionExists(checkListSection.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(checkListSection);
        }

        // GET: CheckListSections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkListSection = await _context.CheckListSection.FirstOrDefaultAsync(m => m.Id == id);
            if (checkListSection == null)
            {
                return NotFound();
            }

            return View(checkListSection);
        }

        // POST: CheckListSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var checkListSection = await _context.CheckListSection.FindAsync(id);
            checkListSection.RegisterStatus = 0;

            //_context.CheckListSection.Remove(checkListSection);

            try
            {
                _context.Update(checkListSection);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckListSectionExists(checkListSection.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CheckListSectionExists(int id)
        {
            return _context.CheckListSection.Any(e => e.Id == id);
        }
    }
}
