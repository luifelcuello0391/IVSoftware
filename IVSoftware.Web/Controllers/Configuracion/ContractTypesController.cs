using IVSoftware.Data.Models;
using IVSoftware.Web.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IVSoftware.Web.Controllers.Configuracion
{
    public class ContractTypesController : Controller
    {
        private readonly IEntityService<ContractType, int> _contractTypeService;

        public ContractTypesController(IEntityService<ContractType, int> contractTypeService)
        {
            _contractTypeService = contractTypeService;
        }

        // GET: ContractTypes
        public async Task<IActionResult> Index()
        {
            return View(await _contractTypeService.GetAllAsync());
        }

        // GET: ContractTypes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ContractType contractType = await _contractTypeService.GetByIdAsync(id);
            if (contractType == null)
            {
                return NotFound();
            }

            return View(contractType);
        }

        // GET: ContractTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContractTypes/Create
        // To protect from over-posting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContractType model)
        {
            if (ModelState.IsValid)
            {
                ContractType contractType = await _contractTypeService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: ContractTypes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ContractType contractType = await _contractTypeService.GetByIdAsync(id);
            if (contractType == null)
            {
                return NotFound();
            }

            return View(contractType);
        }

        // POST: ContractTypes/Edit/5
        // To protect from over-posting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ContractType model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ContractType contractType = await _contractTypeService.UpdateAsync(model);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (!await ContractTypeExists(model.Id))
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

        // GET: ContractTypes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            ContractType contractType = await _contractTypeService.GetByIdAsync(id);
            if (contractType == null)
            {
                return NotFound();
            }

            return View(contractType);
        }

        // POST: ContractTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ContractType contractType = await _contractTypeService.GetByIdAsync(id);
            if (contractType == null)
            {
                return NotFound();
            }

            await _contractTypeService.DeleteAsync(contractType);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ContractTypeExists(int id)
        {
            return (await _contractTypeService.GetByIdAsync(id)) != null;
        }
    }
}