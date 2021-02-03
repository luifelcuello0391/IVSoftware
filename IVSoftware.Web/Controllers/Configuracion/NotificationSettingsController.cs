using IVSoftware.Data.Models;
using IVSoftware.Web.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IVSoftware.Web.Controllers.Configuracion
{

    [Authorize(Roles = "Administrador")]
    public class NotificationSettingsController : Controller
    {
        private readonly IEntityService<NotificationSetting, int> _notificationService;

        public NotificationSettingsController(IEntityService<NotificationSetting, int> notificationService)
        {
            _notificationService = notificationService;
        }

        // GET: NotificationSettings
        public async Task<IActionResult> Index()
        {
            return View(await _notificationService.GetAllAsync());
        }

        // GET: NotificationSettings/Details/5
        public async Task<IActionResult> Details(int id)
        {
            NotificationSetting notificationSetting = await _notificationService.GetByIdAsync(id);
            if (notificationSetting == null)
            {
                return NotFound();
            }

            return View(notificationSetting);
        }

        // GET: NotificationSettings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NotificationSettings/Create
        // To protect from over-posting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NotificationSetting model)
        {
            if (ModelState.IsValid)
            {
                string password = ClsCipher.Encrypt(model.Password, ClsCipher.PasswordKey);
                model.Password = password;
                NotificationSetting notificationSetting = await _notificationService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: NotificationSettings/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            NotificationSetting notificationSetting = await _notificationService.GetByIdAsync(id);
            if (notificationSetting == null)
            {
                return NotFound();
            }

            return View(notificationSetting);
        }

        // POST: NotificationSettings/Edit/5
        // To protect from over-posting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NotificationSetting model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string password = ClsCipher.Encrypt(model.Password, ClsCipher.PasswordKey);
                    model.Password = password;
                    NotificationSetting notificationSetting = await _notificationService.UpdateAsync(model);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (!await NotificationSettingExists(model.Id))
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

        // GET: NotificationSettings/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            NotificationSetting notificationSetting = await _notificationService.GetByIdAsync(id);
            if (notificationSetting == null)
            {
                return NotFound();
            }

            return View(notificationSetting);
        }

        // POST: NotificationSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            NotificationSetting notificationSetting = await _notificationService.GetByIdAsync(id);
            if (notificationSetting == null)
            {
                return NotFound();
            }

            await _notificationService.DeleteAsync(notificationSetting);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> NotificationSettingExists(int id)
        {
            return (await _notificationService.GetByIdAsync(id)) != null;
        }
    }
}