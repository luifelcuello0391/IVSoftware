using IVSoftware.Data.Models;
using IVSoftware.Web.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.Controllers
{
    [Authorize]
    public class PeopleController : Controller
    {
        private readonly IEntityService<Person, Guid> _personService;
        private readonly IEntityService<IdentificationType, int> _identificationTypeService;
        private readonly IEntityService<Arl, int> _arlService;
        private readonly IEntityService<Eps, int> _epsService;
        private readonly IEntityService<BloodType, int> _bloodTypeService;
        private readonly IEntityService<ContractType, int> _contractTypeService;

        public PeopleController(IEntityService<Person, Guid> personService,
            IEntityService<IdentificationType, int> identificationTypeService,
            IEntityService<Arl, int> arlService,
            IEntityService<Eps, int> epsService,
            IEntityService<BloodType, int> bloodTypeService,
            IEntityService<ContractType, int> contractTypeService)
        {
            _personService = personService;
            _identificationTypeService = identificationTypeService;
            _arlService = arlService;
            _epsService = epsService;
            _bloodTypeService = bloodTypeService;
            _contractTypeService = contractTypeService;
        }

        // GET: PeopleController
        public async Task<ActionResult> Index()
        {
            return View(await _personService.GetAllAsync());
        }

        // GET: PeopleController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var person = await _personService.GetByIdAndIncludeAsync(id,
                i => i.IdentificationType, a => a.Arl,
                e => e.Eps, bt => bt.BloodType,
                ct => ct.ContractType);

            if (person == null)
            {
                return NotFound();
            }

            ViewBag.IdentificationTypes = await GetIdentificationTypeSelectList();
            ViewBag.ARLs = await GetArlSelectList();
            ViewBag.EPSs = await GetEpsSelectList();
            ViewBag.BloodTypes = await GetBloodTypeSelectList();
            ViewBag.ContractTypes = await GetContractTypeSelectList();

            return View(person);
        }

        // GET: PeopleController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.IdentificationTypes = await GetIdentificationTypeSelectList();
            ViewBag.ARLs = await GetArlSelectList();
            ViewBag.EPSs = await GetEpsSelectList();
            ViewBag.BloodTypes = await GetBloodTypeSelectList();
            ViewBag.ContractTypes = await GetContractTypeSelectList();

            return View();
        }

        // POST: PeopleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Person model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _personService.CreateAsync(model);
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.IdentificationTypes = await GetIdentificationTypeSelectList();
                ViewBag.ARLs = await GetArlSelectList();
                ViewBag.EPSs = await GetEpsSelectList();
                ViewBag.BloodTypes = await GetBloodTypeSelectList();
                ViewBag.ContractTypes = await GetContractTypeSelectList();

                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: PeopleController/Edit/5
        public async Task<ActionResult> Edit(Guid? id, string email)
        {
            var person = id.HasValue ? await _personService.GetByIdAsync(id.Value)
                : (await _personService.FindByConditionAsync(p => p.Email == email)).FirstOrDefault();

            if (person == null)
            {
                return NotFound();
            }

            ViewBag.IdentificationTypes = await GetIdentificationTypeSelectList();
            ViewBag.ARLs = await GetArlSelectList();
            ViewBag.EPSs = await GetEpsSelectList();
            ViewBag.BloodTypes = await GetBloodTypeSelectList();
            ViewBag.ContractTypes = await GetContractTypeSelectList();

            return View(person);
        }

        // POST: PeopleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Person model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _personService.UpdateAsync(model);
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.IdentificationTypes = await GetIdentificationTypeSelectList();
                ViewBag.ARLs = await GetArlSelectList();
                ViewBag.EPSs = await GetEpsSelectList();
                ViewBag.BloodTypes = await GetBloodTypeSelectList();
                ViewBag.ContractTypes = await GetContractTypeSelectList();

                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: PeopleController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var person = await _personService.GetByIdAndIncludeAsync(id,
                i => i.IdentificationType, a => a.Arl,
                e => e.Eps, bt => bt.BloodType,
                ct => ct.ContractType);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: PeopleController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                var person = await _personService.GetByIdAsync(id);
                if (person == null)
                {
                    return NotFound();
                }

                await _personService.DeleteAsync(person);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private async Task<List<SelectListItem>> GetIdentificationTypeSelectList()
        {
            var identificationTypes = new List<SelectListItem>();
            identificationTypes = (await _identificationTypeService.GetAllAsync()).Select(t => new SelectListItem()
            {
                Text = t.Name,
                Value = t.Id.ToString()
            }).ToList();

            return identificationTypes;
        }

        private async Task<List<SelectListItem>> GetArlSelectList()
        {
            var ARLs = new List<SelectListItem>();
            ARLs = (await _arlService.GetAllAsync()).Select(t => new SelectListItem()
            {
                Text = t.Name,
                Value = t.Id.ToString()
            }).ToList();

            return ARLs;
        }

        private async Task<List<SelectListItem>> GetEpsSelectList()
        {
            var EPSs = new List<SelectListItem>();
            EPSs = (await _epsService.GetAllAsync()).Select(t => new SelectListItem()
            {
                Text = t.Name,
                Value = t.Id.ToString()
            }).ToList();

            return EPSs;
        }

        private async Task<List<SelectListItem>> GetBloodTypeSelectList()
        {
            var bloodTypes = new List<SelectListItem>();
            bloodTypes = (await _bloodTypeService.GetAllAsync()).Select(t => new SelectListItem()
            {
                Text = t.Name,
                Value = t.Id.ToString()
            }).ToList();

            return bloodTypes;
        }

        private async Task<List<SelectListItem>> GetContractTypeSelectList()
        {
            var contractTypes = new List<SelectListItem>();
            contractTypes = (await _contractTypeService.GetAllAsync()).Select(t => new SelectListItem()
            {
                Text = t.Name,
                Value = t.Id.ToString()
            }).ToList();

            return contractTypes;
        }
    }
}