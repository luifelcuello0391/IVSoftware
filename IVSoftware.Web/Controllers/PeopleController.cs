using IVSoftware.Data.Models;
using IVSoftware.Web.BusinessLogic;
using IVSoftware.Web.Data;
using IVSoftware.Web.Models;
using IVSoftware.Web.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.Controllers
{
    [Authorize(Roles = "Administrador, Funcionario")]
    public class PeopleController : Controller
    {
        private readonly IEntityService<Person, Guid> _personService;
        private readonly IEntityService<IdentificationType, int> _identificationTypeService;
        private readonly IEntityService<Arl, int> _arlService;
        private readonly IEntityService<Eps, int> _epsService;
        private readonly IEntityService<BloodType, int> _bloodTypeService;
        private readonly IEntityService<ContractType, int> _contractTypeService;
        private readonly IEntityService<Gender, int> _genderService;
        private readonly UserManager<User> _userManager;
        private readonly IEntityService<AcademicLevel, int> _academicLevelService;
        private readonly IEntityService<CertificationType, int> _certificationTypeService;
        private readonly IEntityService<JobRole, int> _jobRoleService;
        private readonly IVSoftwareContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public PeopleController(IEntityService<Person, Guid> personService,
            IEntityService<IdentificationType, int> identificationTypeService,
            IEntityService<Arl, int> arlService,
            IEntityService<Eps, int> epsService,
            IEntityService<BloodType, int> bloodTypeService,
            IEntityService<ContractType, int> contractTypeService,
            IEntityService<Gender, int> genderService,
            UserManager<User> userManager,
            IEntityService<AcademicLevel, int> academicLevelService,
            IEntityService<CertificationType, int> certificationTypeService,
            IEntityService<JobRole, int> jobRoleService,
            IVSoftwareContext context,
            RoleManager<IdentityRole> roleManager)
        {
            _personService = personService;
            _identificationTypeService = identificationTypeService;
            _arlService = arlService;
            _epsService = epsService;
            _bloodTypeService = bloodTypeService;
            _contractTypeService = contractTypeService;
            _userManager = userManager;
            _genderService = genderService;
            _academicLevelService = academicLevelService;
            _certificationTypeService = certificationTypeService;
            _jobRoleService = jobRoleService;
            _context = context;
            _roleManager = roleManager;
        }

        // GET: PeopleController
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Index()
        {
            return View(await _personService.GetAllAsync());
        }

        // GET: PeopleController/Details/5
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Details(Guid id)
        {
            var person = await _personService.GetByIdAndIncludeAsync(id,
                i => i.IdentificationType, a => a.Arl,
                e => e.Eps, bt => bt.BloodType,
                ct => ct.ContractType,
                g => g.Gender);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: PeopleController/Create
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Create()
        {
            ViewBag.IdentificationTypes = await GetIdentificationTypeSelectList();
            ViewBag.ARLs = await GetArlSelectList();
            ViewBag.EPSs = await GetEpsSelectList();
            ViewBag.BloodTypes = await GetBloodTypeSelectList();
            ViewBag.ContractTypes = await GetContractTypeSelectList();
            ViewBag.Genders = await GetGenderSelectList();

            return View();
        }

        // POST: PeopleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
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
                ViewBag.Genders = await GetGenderSelectList();

                return View(model);
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> CreateUser(Guid id)
        {
            ViewBag.Roles = await GetRolesList();
            var person = await _personService.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            CreateUserVM createUserVM = new CreateUserVM
            {
                Id = person.Id,
                Email = person.Email
            };

            return PartialView("_ModalCreateUser", createUserVM);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> CreateUser(CreateUserVM model)
        {
            ViewBag.Roles = await GetRolesList();
            try
            {
                if (ModelState.IsValid)
                {
                    User user = new User
                    {
                        Email = model.Email,
                        UserName = model.Email
                    };

                    var result = await _userManager.CreateAsync(user, ClsCipher.Encrypt(model.Password, ClsCipher.PasswordKey));
                    if (result == IdentityResult.Success)
                    {
                        var userCreated = await _userManager.FindByEmailAsync(model.Email);
                        var person = await _personService.GetByIdAsync(model.Id);
                        if (person != null)
                        {
                            person.UserId = userCreated.Id;
                            Person personResult = await _personService.UpdateAsync(person);
                            if(personResult == null)
                            {
                                ModelState.AddModelError("Rol", "No se pudo agregar el usuario a la persona");
                                return PartialView("_ModalCreateUser", model);
                            }

                            IdentityResult rolResult = await _userManager.AddToRoleAsync(user, model.Rol);
                            if (rolResult != IdentityResult.Success)
                            {
                                ModelState.AddModelError("Rol", "No se pudo agregar el rol al usuario");
                                return PartialView("_ModalCreateUser", model);
                            }
                        }

                        return RedirectToAction(nameof(Edit), new { id = model.Id });
                    }
                }
            }
            catch (Exception)
            {
                return PartialView("_ModalCreateUser", model);
            }

            return PartialView("_ModalCreateUser", model);
        }

        // GET: PeopleController/Edit/5

        public async Task<ActionResult> Edit(Guid? id, string email)
        {
            var person = await _personService.GetFullPerson(id, email, _academicLevelService, _certificationTypeService);

            if (person == null)
            {
                return NotFound();
            }

            ViewBag.IdentificationTypes = await GetIdentificationTypeSelectList();
            ViewBag.ARLs = await GetArlSelectList();
            ViewBag.EPSs = await GetEpsSelectList();
            ViewBag.BloodTypes = await GetBloodTypeSelectList();
            ViewBag.ContractTypes = await GetContractTypeSelectList();
            ViewBag.Genders = await GetGenderSelectList();
            ViewBag.HasUser = (person.User != null);
            ViewBag.JobRoles = await GetJobRolesList();
            ViewBag.Roles = await GetRolesList();

            person.Role = (person.User != null) ? (await _userManager.GetRolesAsync(person.User)).FirstOrDefault() : "";

            return View(person);
        }

        // POST: PeopleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Person model)
        {
            try
            {
                User user = _userManager.Users.Where(u => u.Id == model.UserId).FirstOrDefault();
                List<SelectListItem> roles = await GetRolesList();
                ViewBag.IdentificationTypes = await GetIdentificationTypeSelectList();
                ViewBag.ARLs = await GetArlSelectList();
                ViewBag.EPSs = await GetEpsSelectList();
                ViewBag.BloodTypes = await GetBloodTypeSelectList();
                ViewBag.ContractTypes = await GetContractTypeSelectList();
                ViewBag.Genders = await GetGenderSelectList();
                ViewBag.HasUser = (!string.IsNullOrEmpty(model.UserId));
                ViewBag.JobRoles = await GetJobRolesList();
                ViewBag.Roles = roles;

                if (ModelState.IsValid)
                {
                    List<PersonJobRole> personJobRoles = new List<PersonJobRole>();
                    if (model.Roles != null && model.Roles.Count > 0)
                    {
                        personJobRoles =
                            model.Roles.Select(r => new PersonJobRole()
                            {
                                PersonId = id,
                                JobRoleId = r
                            }).ToList();

                        _context.PersonJobRoles.RemoveRange(
                            _context.PersonJobRoles.Where(pjr => pjr.PersonId == id));
                        _context.PersonJobRoles.AddRange(personJobRoles);
                        model.PeopleJobRole = personJobRoles;
                    }

                    if(!string.IsNullOrEmpty(model.Role) && !await _userManager.IsInRoleAsync(user, model.Role))
                    {
                        SelectListItem currentRole = roles.Where(r => r.Value != model.Role).FirstOrDefault();
                        if(currentRole != null)
                        {
                            IdentityResult removingRolResult = await _userManager.RemoveFromRoleAsync(user, currentRole.Value);
                            if (removingRolResult != IdentityResult.Success)
                            {
                                ModelState.AddModelError("Rol", $"No se pudo agregar al rol: {string.Join(',', removingRolResult.Errors.Select(e => e.Description))}");
                                return View(model);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("Rol", "No se encuentra ningún rol");
                            return View(model);
                        }

                        IdentityResult assingRolResult = await _userManager.AddToRoleAsync(user, model.Role);
                        if (assingRolResult != IdentityResult.Success)
                        {
                            ModelState.AddModelError("Rol", $"No se pudo agregar al rol: {string.Join(',', assingRolResult.Errors.Select(e => e.Description))}");
                            return View(model);
                        }
                    }

                    await _personService.UpdateAsync(model);
                    if (User.IsInRole("Administrador"))
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    return RedirectToAction(nameof(Index), "Home");
                }

                return View(model);
            }
            catch(Exception ex)
            {
                return View(model);
            }
        }

        // GET: PeopleController/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var person = await _personService.GetByIdAndIncludeAsync(id,
                i => i.IdentificationType, a => a.Arl,
                e => e.Eps, bt => bt.BloodType,
                ct => ct.ContractType,
                g => g.Gender);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: PeopleController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
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

        // GET: PeopleController/ChangePassword/
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = await _userManager.GetUserAsync(User);
                    if (user == null)
                    {
                        return NotFound();
                    }

                    string currentPassword = ClsCipher.Encrypt(model.Password, ClsCipher.PasswordKey);
                    if (!await _userManager.CheckPasswordAsync(user, currentPassword))
                    {
                        ModelState.AddModelError("Password", "Contraseña incorrecta");
                        return View(model);
                    }

                    IdentityResult result =
                        await _userManager.ChangePasswordAsync(
                            user,
                            currentPassword,
                            ClsCipher.Encrypt(model.NewPassword, ClsCipher.PasswordKey));

                    if (result == IdentityResult.Success)
                    {
                        return RedirectToAction("Logout", "Account");
                    }

                    ModelState.AddModelError("Password", string.Join(';', result.Errors.Select(e => e.Description)));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Password", ex.Message);
            }

            return View(model);
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

        private async Task<List<SelectListItem>> GetGenderSelectList()
        {
            var genders = new List<SelectListItem>();
            genders = (await _genderService.GetAllAsync()).Select(t => new SelectListItem()
            {
                Text = t.Name,
                Value = t.Id.ToString()
            }).ToList();

            return genders;
        }

        private async Task<List<SelectListItem>> GetJobRolesList()
        {
            var Jobroles = (await _jobRoleService.GetAllAsync()).Select(t => new SelectListItem()
            {
                Text = t.Name,
                Value = t.Id.ToString()
            }).ToList();

            return Jobroles;
        }

        private async Task<List<SelectListItem>> GetRolesList()
        {
            var roles = await _roleManager.Roles.Select(t => new SelectListItem()
            {
                Text = t.Name,
                Value = t.Name
            }).ToListAsync();

            return roles;
        }
    }
}