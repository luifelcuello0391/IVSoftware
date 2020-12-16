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
    public class CheckListQuestionsController : Controller
    {
        private readonly IVSoftwareContext _context;

        public CheckListQuestionsController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: CheckListQuestions
        public async Task<IActionResult> Index()
        {
            return View(await _context.CheckListQuestion.ToListAsync());
        }

        // GET: CheckListQuestions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkListQuestion = await _context.CheckListQuestion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (checkListQuestion == null)
            {
                return NotFound();
            }

            return View(checkListQuestion);
        }

        // GET: CheckListQuestions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CheckListQuestions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeOfQuestion,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] CheckListQuestion checkListQuestion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(checkListQuestion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(checkListQuestion);
        }

        // GET: CheckListQuestions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkListQuestion = await _context.CheckListQuestion.FindAsync(id);
            if (checkListQuestion == null)
            {
                return NotFound();
            }
            return View(checkListQuestion);
        }

        // POST: CheckListQuestions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeOfQuestion,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] CheckListQuestion checkListQuestion)
        {
            if (id != checkListQuestion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checkListQuestion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CheckListQuestionExists(checkListQuestion.Id))
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
            return View(checkListQuestion);
        }

        // GET: CheckListQuestions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkListQuestion = await _context.CheckListQuestion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (checkListQuestion == null)
            {
                return NotFound();
            }

            return View(checkListQuestion);
        }

        // POST: CheckListQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var checkListQuestion = await _context.CheckListQuestion.FindAsync(id);
            _context.CheckListQuestion.Remove(checkListQuestion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CheckListQuestionExists(int id)
        {
            return _context.CheckListQuestion.Any(e => e.Id == id);
        }
    }
}
