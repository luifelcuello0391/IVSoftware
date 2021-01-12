using IVSoftware.Data.Models;
using IVSoftware.Web.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IVSoftware.Web.Controllers.Configuracion
{
    public class IdentificationTypesController : Controller
    {
        private readonly IEntityService<IdentificationType, int> _identificationTypeService;

        public IdentificationTypesController(IEntityService<IdentificationType, int> identificationTypeService)
        {
            _identificationTypeService = identificationTypeService;
        }

        // GET: IdentificationTypes
        public async Task<IActionResult> Index()
        {
            return View(await _identificationTypeService.GetAllAsync());
        }

        // GET: IdentificationTypes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            IdentificationType identificationType = await _identificationTypeService.GetByIdAsync(id);
            if (identificationType == null)
            {
                return NotFound();
            }

            return View(identificationType);
        }

        // GET: IdentificationTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IdentificationTypes/Create
        // To protect from over-posting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IdentificationType model)
        {
            if (ModelState.IsValid)
            {
                IdentificationType identificationType = await _identificationTypeService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: IdentificationTypes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            IdentificationType identificationType = await _identificationTypeService.GetByIdAsync(id);
            if (identificationType == null)
            {
                return NotFound();
            }

            return View(identificationType);
        }

        // POST: IdentificationTypes/Edit/5
        // To protect from over-posting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IdentificationType model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    IdentificationType identificationType = await _identificationTypeService.UpdateAsync(model);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (!await IdentificationTypeExists(model.Id))
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

        // GET: IdentificationTypes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            IdentificationType identificationType = await _identificationTypeService.GetByIdAsync(id);
            if (identificationType == null)
            {
                return NotFound();
            }

            return View(identificationType);
        }

        // POST: IdentificationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            IdentificationType identificationType = await _identificationTypeService.GetByIdAsync(id);
            if (identificationType == null)
            {
                return NotFound();
            }

            await _identificationTypeService.DeleteAsync(identificationType);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> IdentificationTypeExists(int id)
        {
            return (await _identificationTypeService.GetByIdAsync(id)) != null;
        }
    }
}