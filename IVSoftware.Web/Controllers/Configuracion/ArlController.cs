using IVSoftware.Data.Models;
using IVSoftware.Web.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IVSoftware.Web.Controllers.Configuracion
{
    public class ArlController : Controller
    {
        private readonly IEntityService<Arl, int> _arlService;

        public ArlController(IEntityService<Arl, int> arlService)
        {
            _arlService = arlService;
        }

        // GET: Arl
        public async Task<IActionResult> Index()
        {
            return View(await _arlService.GetAllAsync());
        }

        // GET: Arl/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Arl arl = await _arlService.GetByIdAsync(id);
            if (arl == null)
            {
                return NotFound();
            }

            return View(arl);
        }

        // GET: Arl/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Arl/Create
        // To protect from over-posting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Arl model)
        {
            if (ModelState.IsValid)
            {
                Arl arl = await _arlService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Arl/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Arl arl = await _arlService.GetByIdAsync(id);
            if (arl == null)
            {
                return NotFound();
            }

            return View(arl);
        }

        // POST: Arl/Edit/5
        // To protect from over-posting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Arl model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Arl arl = await _arlService.UpdateAsync(model);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (!await ArlExists(model.Id))
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

        // GET: Arl/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Arl arl = await _arlService.GetByIdAsync(id);
            if (arl == null)
            {
                return NotFound();
            }

            return View(arl);
        }

        // POST: Arl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Arl arl = await _arlService.GetByIdAsync(id);
            if (arl == null)
            {
                return NotFound();
            }

            await _arlService.DeleteAsync(arl);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ArlExists(int id)
        {
            return (await _arlService.GetByIdAsync(id)) != null;
        }
    }
}