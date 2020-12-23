using IVSoftware.Data.Models;
using IVSoftware.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IVSoftware.Web.Controllers
{
    public class BasicEducationsController : Controller
    {
        private readonly IEntityService<BasicEducation, Guid> _basicEducationService;
        private readonly IEntityService<Person, Guid> _personService;

        public BasicEducationsController(IEntityService<BasicEducation, Guid> basicEducationService,
            IEntityService<Person, Guid> personService)
        {
            _basicEducationService = basicEducationService;
            _personService = personService;
        }

        // GET: BasicEducationsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: BasicEducationsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BasicEducationsController/Create
        public async Task<ActionResult> Create(Guid personId)
        {
            var person = await _personService.GetByIdAsync(personId);
            if(person == null)
            {
                return NotFound();
            }

            BasicEducation basicEducation = new BasicEducation()
            {
                PersonId = person.Id,
                Person = person,
                DegreeDate = DateTime.Today
            };

            return View(basicEducation);
        }

        // POST: BasicEducationsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BasicEducation model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _basicEducationService.CreateAsync(model);
                    return RedirectToAction("Edit", "People", new { id = model.PersonId });
                }

                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: BasicEducationsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BasicEducationsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BasicEducationsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BasicEducationsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}