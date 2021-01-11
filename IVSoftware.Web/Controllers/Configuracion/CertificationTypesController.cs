using IVSoftware.Data.Models;
using IVSoftware.Web.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IVSoftware.Web.Controllers.Configuracion
{
    public class CertificationTypesController : Controller
    {
        private readonly IEntityService<CertificationType, int> _certificationTypeService;

        public CertificationTypesController(IEntityService<CertificationType, int> certificationTypeService)
        {
            _certificationTypeService = certificationTypeService;
        }

        // GET: CertificationTypes
        public async Task<IActionResult> Index()
        {
            return View(await _certificationTypeService.GetAllAsync());
        }

        // GET: CertificationTypes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            CertificationType certificationType = await _certificationTypeService.GetByIdAsync(id);
            if (certificationType == null)
            {
                return NotFound();
            }

            return View(certificationType);
        }

        // GET: CertificationTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CertificationTypes/Create
        // To protect from over-posting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CertificationType model)
        {
            if (ModelState.IsValid)
            {
                CertificationType certificationType = await _certificationTypeService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: CertificationTypes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            CertificationType certificationType = await _certificationTypeService.GetByIdAsync(id);
            if (certificationType == null)
            {
                return NotFound();
            }

            return View(certificationType);
        }

        // POST: CertificationTypes/Edit/5
        // To protect from over-posting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CertificationType model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    CertificationType certificationType = await _certificationTypeService.UpdateAsync(model);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (!await CertificationTypeExists(model.Id))
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

        // GET: CertificationTypes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            CertificationType certificationType = await _certificationTypeService.GetByIdAsync(id);
            if (certificationType == null)
            {
                return NotFound();
            }

            return View(certificationType);
        }

        // POST: CertificationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            CertificationType certificationType = await _certificationTypeService.GetByIdAsync(id);
            if (certificationType == null)
            {
                return NotFound();
            }

            await _certificationTypeService.DeleteAsync(certificationType);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CertificationTypeExists(int id)
        {
            return (await _certificationTypeService.GetByIdAsync(id)) != null;
        }
    }
}