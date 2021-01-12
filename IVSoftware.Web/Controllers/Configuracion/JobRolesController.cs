using IVSoftware.Data.Models;
using IVSoftware.Web.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IVSoftware.Web.Controllers.Configuracion
{
    public class JobRolesController : Controller
    {
        private readonly IEntityService<JobRole, int> _jobRolService;

        public JobRolesController(IEntityService<JobRole, int> jobRolService)
        {
            _jobRolService = jobRolService;
        }

        // GET: JobRoles
        public async Task<IActionResult> Index()
        {
            return View(await _jobRolService.GetAllAsync());
        }

        // GET: JobRoles/Details/5
        public async Task<IActionResult> Details(int id)
        {
            JobRole jobRole = await _jobRolService.GetByIdAsync(id);
            if (jobRole == null)
            {
                return NotFound();
            }

            return View(jobRole);
        }

        // GET: JobRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobRoles/Create
        // To protect from over-posting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobRole model)
        {
            if (ModelState.IsValid)
            {
                JobRole jobRole = await _jobRolService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: JobRoles/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            JobRole jobRole = await _jobRolService.GetByIdAsync(id);
            if (jobRole == null)
            {
                return NotFound();
            }

            return View(jobRole);
        }

        // POST: JobRoles/Edit/5
        // To protect from over-posting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JobRole model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    JobRole jobRole = await _jobRolService.UpdateAsync(model);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (!await JobRolExists(model.Id))
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

        // GET: JobRoles/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            JobRole jobRole = await _jobRolService.GetByIdAsync(id);
            if (jobRole == null)
            {
                return NotFound();
            }

            return View(jobRole);
        }

        // POST: JobRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            JobRole jobRole = await _jobRolService.GetByIdAsync(id);
            if (jobRole == null)
            {
                return NotFound();
            }

            await _jobRolService.DeleteAsync(jobRole);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> JobRolExists(int id)
        {
            return (await _jobRolService.GetByIdAsync(id)) != null;
        }
    }
}