using IVSoftware.Data.Models;
using IVSoftware.Web.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IVSoftware.Web.Controllers.Configuracion
{
    public class EpsController : Controller
    {
        private readonly IEntityService<Eps, int> _epsService;

        public EpsController(IEntityService<Eps, int> epsService)
        {
            _epsService = epsService;
        }

        // GET: Eps
        public async Task<IActionResult> Index()
        {
            return View(await _epsService.GetAllAsync());
        }

        // GET: Eps/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Eps eps = await _epsService.GetByIdAsync(id);
            if (eps == null)
            {
                return NotFound();
            }

            return View(eps);
        }

        // GET: Eps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Eps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Eps model)
        {
            if (ModelState.IsValid)
            {
                Eps eps = await _epsService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Eps/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Eps eps = await _epsService.GetByIdAsync(id);
            if (eps == null)
            {
                return NotFound();
            }

            return View(eps);
        }

        // POST: Eps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Eps model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Eps eps = await _epsService.UpdateAsync(model);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (!await EpsExists(model.Id))
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

        // GET: Eps/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Eps eps = await _epsService.GetByIdAsync(id);
            if (eps == null)
            {
                return NotFound();
            }

            return View(eps);
        }

        // POST: Eps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Eps eps = await _epsService.GetByIdAsync(id);
            if (eps == null)
            {
                return NotFound();
            }

            await _epsService.DeleteAsync(eps);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EpsExists(int id)
        {
            return (await _epsService.GetByIdAsync(id)) != null;
        }
    }
}