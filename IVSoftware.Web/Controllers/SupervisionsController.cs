using IVSoftware.Data.Models;
using IVSoftware.Web.Service;
using Microsoft.AspNetCore.Authorization;
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
    public class SupervisionsController : Controller
    {
        private readonly IEntityService<Supervision, Guid> _supervisionService;
        private readonly IEntityService<SupervisionDetail, Guid> _supervisionDetailService;
        private readonly IEntityService<Person, Guid> _personService;

        public SupervisionsController(IEntityService<Supervision, Guid> supervisionService,
            IEntityService<SupervisionDetail, Guid> supervisionDetailService,
            IEntityService<Person, Guid> personService)
        {
            _supervisionService = supervisionService;
            _supervisionDetailService = supervisionDetailService;
            _personService = personService;
        }

        // GET: SupervisionsController/Create
        public async Task<ActionResult> Create(Guid personId)
        {
            var person = await _personService.GetByIdAsync(personId);
            if (person == null)
            {
                return NotFound();
            }

            Supervision supervision = new Supervision()
            {
                SupervisedById = personId,
                SupervisedBy = person,
                Date = DateTime.Today,
                Details = new List<SupervisionDetail>()
            };
            ViewBag.People = await GetPeopleList();

            return View(supervision);
        }

        // POST: SupervisionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Supervision model)
        {
            ViewBag.People = await GetPeopleList();
            try
            {
                if (ModelState.IsValid)
                {
                    await _supervisionService.CreateAsync(model);

                    return RedirectToAction(nameof(Edit), new { id = model.Id });
                }

                return View(model);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("SupervisedById", ex.Message);
                return View(model);
            }
        }

        public ActionResult CreateDetail(Guid id)
        {
            SupervisionDetail supervisionDetail = new SupervisionDetail()
            {
                Id = Guid.Empty,
                SupervisionId = id
            };

            return PartialView("_ModalDetail", supervisionDetail);
        }

        [HttpPost]
        public async Task<ActionResult> CreateDetail(SupervisionDetail model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _supervisionDetailService.CreateAsync(model);

                    return RedirectToAction(nameof(Edit), new { id = model.SupervisionId });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Id", ex.Message);
            }

            return PartialView("_ModalDetail", model);
        }

        //GET: SupervisionsController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var supervision = await _supervisionService.GetByIdAsync(id);
            if (supervision == null) { return NotFound(); }

            ViewBag.People = await GetPeopleList();
            return View(supervision);
        }

        //POST: SupervisionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Supervision model)
        {
            ViewBag.People = await GetPeopleList();
            if (id != model.Id) { return NotFound(); }

            try
            {
                if (ModelState.IsValid)
                {
                    await _supervisionService.UpdateAsync(model);
                    return RedirectToAction("Edit", "People", new { id = model.SupervisedById });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Id", ex.Message);
            }

            return View(model);
        }

        // GET: SupervisionsController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var supervision = await _supervisionService.GetByIdAsync(id);
            if (supervision == null) { return NotFound(); }
            ViewBag.People = await GetPeopleList();

            return View(supervision);
        }

        // POST: SupervisionsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, Supervision model)
        {
            ViewBag.People = await GetPeopleList();
            if (id != model.Id) { return NotFound(); }

            try
            {
                var supervision = await _supervisionService.GetByIdAsync(id);
                if (supervision == null) { return NotFound(); }
                await _supervisionService.DeleteAsync(supervision);

                return RedirectToAction("Edit", "People", new { id = model.SupervisedById });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Id", ex.Message);
            }

            return View(model);
        }

        public async Task<ActionResult> EditDetail(Guid id)
        {
            var supervisionDetail = await _supervisionDetailService.GetByIdAsync(id);
            if (supervisionDetail == null) { return NotFound(); }

            return PartialView("_ModalEditDetail", supervisionDetail);
        }

        [HttpPost]
        public async Task<ActionResult> EditDetail(Guid id, SupervisionDetail model)
        {
            if (id != model.Id) { return NotFound(); }

            try
            {
                if (ModelState.IsValid)
                {
                    await _supervisionDetailService.UpdateAsync(model);
                    return RedirectToAction("Edit", new { id = model.SupervisionId });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Id", ex.Message);
            }

            return PartialView("_ModalEditDetail", model);
        }

        public async Task<ActionResult> DeleteDetail(Guid id)
        {
            var supervisionDetail = await _supervisionDetailService.GetByIdAsync(id);
            if (supervisionDetail == null) { return NotFound(); }

            return PartialView("_ModalDeleteDetail", supervisionDetail);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteDetail(Guid id, SupervisionDetail model)
        {
            if (id != model.Id) { return NotFound(); }

            try
            {
                var supervisionDetail = await _supervisionDetailService.GetByIdAsync(id);
                if (supervisionDetail == null) { return NotFound(); }
                await _supervisionDetailService.DeleteAsync(supervisionDetail);

                return RedirectToAction("Edit", new { id = model.SupervisionId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Id", ex.Message);
            }

            return PartialView("_ModalDeleteDetail", model);
        }

        private async Task<List<SelectListItem>> GetPeopleList()
        {
            var people = (await _personService.GetAllAsync()).Select(t => new SelectListItem()
            {
                Text = t.FullName,
                Value = t.Id.ToString()
            }).ToList();

            return people;
        }
    }
}