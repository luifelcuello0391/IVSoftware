using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IVSoftware.Web.Models;
using IVSoftware.Data.Models;
using Microsoft.AspNetCore.Identity;
using IVSoftware.Web.Service;

namespace IVSoftware.Web.Controllers
{
    public class ChecklistResponseHeadersController : Controller
    {
        private readonly IVSoftwareContext _context;
        private readonly UserManager<User> _userManager; 
        private readonly IEntityService<Person, Guid> _personService;

        public ChecklistResponseHeadersController(IVSoftwareContext context, UserManager<User> userManager, IEntityService<Person, Guid> personService)
        {
            _personService = personService;
            _userManager = userManager;
            _context = context;
        }

        // GET: ChecklistResponseHeaders
        public async Task<IActionResult> Index(int? id)
        {
            try
            {
                ViewBag.CheckLists = await _context.EquipmentCheckList.ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error on ChecklistResponseHeader.Index >> " + ex.ToString());
            }

            if(id != null)
            {
                try
                {
                    ViewBag.Equipment = await _context.Equipment.FirstOrDefaultAsync<Equipment>(x => x.Id == id.Value);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error on ChecklistResponseHeadersController.Index >> " + ex.ToString());
                }

                return View(await _context.ChecklistResponseHeader.Where(x => x.Equipment != null && x.Equipment.Id == id.Value).ToListAsync());
            }
            else
            {
                return View(await _context.ChecklistResponseHeader.ToListAsync());
            }
        }

        // GET: ChecklistResponseHeaders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklistResponseHeader = await _context.ChecklistResponseHeader
                .FirstOrDefaultAsync(m => m.Id == id);
            if (checklistResponseHeader == null)
            {
                return NotFound();
            }

            return View(checklistResponseHeader);
        }

        public async Task<string> CaptureCheckListResponse(int equipId, int checklistId, string answers, string observation, string fillupDate, int validation_result)
        {
            if(equipId > 0 && checklistId > 0 && answers != null && !string.IsNullOrEmpty(answers.Replace(" ", string.Empty)) && fillupDate != null)
            {
                ChecklistResponseHeader header = new ChecklistResponseHeader();
                header.EquipmentId = equipId;
                header.Equipment = await _context.Equipment.FirstOrDefaultAsync<Equipment>(x => x.Id == equipId);
                header.Name = "Response header";
                header.ValidationResult = validation_result == 1;

                try
                {
                    User currentUser = await _userManager.GetUserAsync(User);
                    if (currentUser != null)
                    {
                        header.ValidatedBy = await _personService.GetByIdAsync(Guid.Parse(currentUser.Id));
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error on CaptureCheckListResponse.UserData >> " + ex.ToString());
                }

                header.CheckListId = checklistId;
                header.CheckList = await _context.EquipmentCheckList.FirstOrDefaultAsync<EquipmentCheckList>(x => x.Id == checklistId);

                header.Observation = observation;
                header.RegisterStatus = 1;
                header.CreationDatetime = DateTime.Now;
                header.ModificationDatetime = DateTime.Now;

                try
                {
                    header.FillUpDate = DateTime.Parse(fillupDate);
                }
                catch
                {
                    header.FillUpDate = DateTime.Now;
                }

                string[] sections = answers.Split(new string[] { "||EOM||" }, StringSplitOptions.RemoveEmptyEntries);
                
                if(sections.Length > 0)
                {
                    foreach(string section in sections)
                    {
                        try
                        {
                            if (section != null && !string.IsNullOrEmpty(section.Replace(" ", string.Empty)))
                            {
                                string[] questions = section.Split(new string[] { "||QUEST||" }, StringSplitOptions.RemoveEmptyEntries);
                                if (questions.Length > 0)
                                {
                                    int sectionId = 0;

                                    foreach (string question in questions)
                                    {
                                        if (question != null && !string.IsNullOrEmpty(question.Replace(" ", string.Empty)))
                                        {
                                            if(question.StartsWith("question_"))
                                            {
                                                string[] answer = question.Split(new string[] { "||RESP||" }, StringSplitOptions.RemoveEmptyEntries);

                                                if (answer.Length > 0)
                                                {
                                                    CheckListResponseDetail response_detail = new CheckListResponseDetail();
                                                    response_detail.Header = header;
                                                    response_detail.QuestionId = int.Parse(answer[0].Replace("question_", string.Empty).Replace("add_", string.Empty));
                                                    response_detail.Response = answer[1];
                                                    response_detail.Name = "Response detail";

                                                    if(sectionId > 0)
                                                    {
                                                        response_detail.SectionId = sectionId;
                                                    }
                                                    else
                                                    {
                                                        response_detail.SectionId = null;
                                                    }
                                                    
                                                    response_detail.RegisterStatus = 1;
                                                    response_detail.CreationDatetime = DateTime.Now;
                                                    response_detail.ModificationDatetime = DateTime.Now;

                                                    if (header != null)
                                                    {
                                                        if (header.Details == null) header.Details = new List<CheckListResponseDetail>();

                                                        header.Details.Add(response_detail);
                                                    }
                                                    else
                                                    {
                                                        break;
                                                    }

                                                    sectionId = 0;
                                                }
                                            }
                                            else if (question.StartsWith("section_"))
                                            {
                                                // Obtains the section Id
                                                sectionId = int.Parse(question.Replace("section_", string.Empty).Replace("valid", string.Empty).Replace(" ", string.Empty));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error on answers collection >> " + ex.ToString());
                        }
                        
                    }
                }

                // Save answers
                if(header != null && header.Details != null && header.Details.Count > 0)
                {
                    try
                    {
                        _context.Add(header);
                        await _context.SaveChangesAsync();
                        return equipId.ToString();
                        //return RedirectToAction(nameof(Index), "ChecklistResponseHeaders", new { id = equipId });
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Error on CheckListResponseHeader.Create >> " + ex.ToString());
                    }
                }

                return null;
            }

            return null;
        }

        // GET: ChecklistResponseHeaders/Create
        public async Task<IActionResult> Create(int? _id, int? _equipId)
        {
            if (_id > 0)
            {
                if (_equipId > 0)
                {
                    try
                    {
                        ViewBag.Equipment = await _context.Equipment.FirstOrDefaultAsync<Equipment>(x => x.Id == _equipId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error on ChecklistResponseHeader.Create.Equipment >> " + ex.ToString());
                    }

                    try
                    {
                        ViewBag.CheckList = await _context.EquipmentCheckList.FirstOrDefaultAsync<EquipmentCheckList>(x => x.Id == _id);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error on ChecklistResponseHeader.Create.Checklist >> " + ex.ToString());
                    }
                }
            }

            return View();
        }

        // POST: ChecklistResponseHeaders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FillUpDate,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] ChecklistResponseHeader checklistResponseHeader)
        {
            if (ModelState.IsValid)
            {
                _context.Add(checklistResponseHeader);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(checklistResponseHeader);
        }

        // GET: ChecklistResponseHeaders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklistResponseHeader = await _context.ChecklistResponseHeader.FindAsync(id);
            if (checklistResponseHeader == null)
            {
                return NotFound();
            }
            return View(checklistResponseHeader);
        }

        // POST: ChecklistResponseHeaders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FillUpDate,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] ChecklistResponseHeader checklistResponseHeader)
        {
            if (id != checklistResponseHeader.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checklistResponseHeader);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChecklistResponseHeaderExists(checklistResponseHeader.Id))
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
            return View(checklistResponseHeader);
        }

        // GET: ChecklistResponseHeaders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklistResponseHeader = await _context.ChecklistResponseHeader
                .FirstOrDefaultAsync(m => m.Id == id);
            if (checklistResponseHeader == null)
            {
                return NotFound();
            }

            return View(checklistResponseHeader);
        }

        // POST: ChecklistResponseHeaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var checklistResponseHeader = await _context.ChecklistResponseHeader.FindAsync(id);
            _context.ChecklistResponseHeader.Remove(checklistResponseHeader);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChecklistResponseHeaderExists(int id)
        {
            return _context.ChecklistResponseHeader.Any(e => e.Id == id);
        }
    }
}
