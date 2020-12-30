using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IVSoftware.Web.Models;
using IVSoftware.Data.Models;

namespace IVSoftware.Web.Controllers
{
    public class EquipmentCheckListsController : Controller
    {
        private readonly IVSoftwareContext _context;

        public EquipmentCheckListsController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: EquipmentCheckLists
        public async Task<IActionResult> Index()
        {
            return View(await _context.EquipmentCheckList.ToListAsync());
        }

        // GET: EquipmentCheckLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentCheckList = await _context.EquipmentCheckList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipmentCheckList == null)
            {
                return NotFound();
            }

            return View(equipmentCheckList);
        }

        public async Task<IActionResult> AssignQuestions(string questionIds, int _id)
        {
            if (questionIds != null && !string.IsNullOrEmpty(questionIds.Replace(" ", string.Empty)))
            {
                var checkList = await _context.EquipmentCheckList.FindAsync(_id);

                if (checkList == null)
                {
                    return NotFound();
                }

                string[] questions = questionIds.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                if (questions.Length > 0)
                {
                    bool added = false;

                    foreach (string id in questions)
                    {
                        try
                        {
                            int questionId = int.Parse(id);

                            bool found = false;

                            if (checkList != null && checkList.Questions != null && checkList.Questions.Count > 0)
                            {
                                found = checkList.Questions.FirstOrDefault<EquipmentCheckListQuestions>(x => x.QuestionId == questionId) != null;
                            }

                            if (!found)
                            {
                                CheckListQuestion question = await _context.CheckListQuestion.FirstOrDefaultAsync<CheckListQuestion>(x => x.Id == questionId);

                                if (question != null)
                                {
                                    if (checkList.Questions == null)
                                    {
                                        checkList.Questions = new List<EquipmentCheckListQuestions>();
                                    }

                                    EquipmentCheckListQuestions relation = new EquipmentCheckListQuestions();
                                    relation.QuestionId = question.Id;
                                    relation.Question = question;

                                    relation.CheckListId = checkList.Id;
                                    relation.Checklist = checkList;
                                    relation.Order = checkList.Questions.Count + 1;

                                    checkList.Questions.Add(relation);
                                    added = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error on AssignQuestions >> " + ex.ToString());
                        }
                    }

                    if (added)
                    {
                        try
                        {
                            _context.Update(checkList);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!EquipmentCheckListExists(checkList.Id))
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

            return RedirectToAction(nameof(Edit), "EquipmentCheckLists", new { id = _id });
        }

        public async Task<IActionResult> UnassignQuestion(int questionId, int _id)
        {
            if (_id > 0)
            {
                var checkList = await _context.EquipmentCheckList.FindAsync(_id);

                if (checkList == null)
                {
                    return NotFound();
                }

                if (checkList.Questions != null && checkList.Questions.Count > 0)
                {
                    EquipmentCheckListQuestions question = checkList.Questions.FirstOrDefault<EquipmentCheckListQuestions>(x => x.QuestionId == questionId);
                    if (question != null)
                    {
                        checkList.Questions.Remove(question);

                        try
                        {
                            _context.Update(checkList);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!EquipmentCheckListExists(checkList.Id))
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

            return RedirectToAction(nameof(Edit), "EquipmentCheckLists", new { id = _id });
        }

        public async Task<IActionResult> AssignSections(string sectionIds, int _id)
        {
            if (sectionIds != null && !string.IsNullOrEmpty(sectionIds.Replace(" ", string.Empty)))
            {
                var checkList = await _context.EquipmentCheckList.FindAsync(_id);

                if (checkList == null)
                {
                    return NotFound();
                }

                string[] sections = sectionIds.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                if (sections.Length > 0)
                {
                    bool added = false;

                    foreach (string id in sections)
                    {
                        try
                        {
                            int sectionId = int.Parse(id);

                            bool found = false;

                            if (checkList != null && checkList.Sections != null && checkList.Sections.Count > 0)
                            {
                                found = checkList.Sections.FirstOrDefault<EquipmentCheckListSections>(x => x.SectionId == sectionId) != null;
                            }

                            if (!found)
                            {
                                CheckListSection section = await _context.CheckListSection.FirstOrDefaultAsync<CheckListSection>(x => x.Id == sectionId);

                                if (section != null)
                                {
                                    if (checkList.Sections == null)
                                    {
                                        checkList.Sections = new List<EquipmentCheckListSections>();
                                    }

                                    EquipmentCheckListSections relation = new EquipmentCheckListSections();
                                    relation.SectionId = section.Id;
                                    relation.Section = section;

                                    relation.ChecklistId = checkList.Id;
                                    relation.CheckList = checkList;
                                    relation.Order = checkList.Sections.Count + 1;

                                    checkList.Sections.Add(relation);
                                    added = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error on AssignSections >> " + ex.ToString());
                        }
                    }

                    if (added)
                    {
                        try
                        {
                            _context.Update(checkList);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!EquipmentCheckListExists(checkList.Id))
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

            return RedirectToAction(nameof(Edit), "EquipmentCheckLists", new { id = _id });
        }

        public async Task<IActionResult> UnassignSection(int sectionId, int _id)
        {
            if (_id > 0)
            {
                var checkList = await _context.EquipmentCheckList.FindAsync(_id);

                if (checkList == null)
                {
                    return NotFound();
                }

                if (checkList.Sections != null && checkList.Sections.Count > 0)
                {
                    EquipmentCheckListSections section = checkList.Sections.FirstOrDefault<EquipmentCheckListSections>(x => x.SectionId == sectionId);
                    if (section != null)
                    {
                        checkList.Sections.Remove(section);

                        try
                        {
                            _context.Update(checkList);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!EquipmentCheckListExists(checkList.Id))
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

            return RedirectToAction(nameof(Edit), "EquipmentCheckLists", new { id = _id });
        }

        // GET: EquipmentCheckLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EquipmentCheckLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] EquipmentCheckList equipmentCheckList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipmentCheckList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(equipmentCheckList);
        }

        // GET: EquipmentCheckLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentCheckList = await _context.EquipmentCheckList.FindAsync(id);
            if (equipmentCheckList == null)
            {
                return NotFound();
            }

            try
            {
                ViewBag.Questions = await _context.CheckListQuestion.Where(x => x.RegisterStatus >= 1).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on Edit.Questions >> " + ex.ToString());
            }

            try
            {
                ViewBag.Sections = await _context.CheckListSection.Where(x => x.RegisterStatus >= 1).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on Edit.Sections >> " + ex.ToString());
            }

            equipmentCheckList.Questions.OrderBy(x => x.Order);
            equipmentCheckList.Sections.OrderBy(x => x.Order);

            return View(equipmentCheckList);
        }

        // POST: EquipmentCheckLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] EquipmentCheckList equipmentCheckList)
        {
            if (id != equipmentCheckList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipmentCheckList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentCheckListExists(equipmentCheckList.Id))
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
            return View(equipmentCheckList);
        }

        // GET: EquipmentCheckLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentCheckList = await _context.EquipmentCheckList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipmentCheckList == null)
            {
                return NotFound();
            }

            return View(equipmentCheckList);
        }

        // POST: EquipmentCheckLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipmentCheckList = await _context.EquipmentCheckList.FindAsync(id);
            _context.EquipmentCheckList.Remove(equipmentCheckList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentCheckListExists(int id)
        {
            return _context.EquipmentCheckList.Any(e => e.Id == id);
        }
    }
}
