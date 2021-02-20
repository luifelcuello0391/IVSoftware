using ClosedXML.Excel;
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
using System.IO;
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
                            if (personResult == null)
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
                    IQueryable<PersonJobRole> currentPersonJobRoles =
                        _context.PersonJobRoles.Where(pjr => pjr.PersonId == id);
                    if (model.Roles != null && model.Roles.Count > 0)
                    {
                        personJobRoles =
                            model.Roles.Select(r => new PersonJobRole()
                            {
                                PersonId = id,
                                JobRoleId = r
                            }).ToList();

                        _context.PersonJobRoles.RemoveRange(currentPersonJobRoles);
                        _context.PersonJobRoles.AddRange(personJobRoles);
                        model.PeopleJobRole = personJobRoles;
                    }
                    else if(currentPersonJobRoles.Any())
                    {
                        _context.PersonJobRoles.RemoveRange(currentPersonJobRoles);
                    }

                    if (!string.IsNullOrEmpty(model.Role) && !await _userManager.IsInRoleAsync(user, model.Role))
                    {
                        SelectListItem currentRole = roles.Where(r => r.Value != model.Role).FirstOrDefault();
                        if (currentRole != null)
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
            catch (Exception ex)
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

                if (person.User != null)
                {
                    await _userManager.DeleteAsync(person.User);
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

        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> ResetPassword(Guid id)
        {
            var person = await _personService.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            ResetPasswordVM resetPasswordVM = new ResetPasswordVM
            {
                Id = person.Id
            };

            return PartialView("_ModalResetPassword", resetPasswordVM);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> ResetPassword(ResetPasswordVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Person person = await _personService.GetByIdAsync(model.Id);
                    User user = person.User;
                    if (user == null) { return NotFound(); }

                    string token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, token, ClsCipher.Encrypt(model.Password, ClsCipher.PasswordKey));
                    if (result == IdentityResult.Success)
                    {
                        return RedirectToAction(nameof(Edit), new { id = model.Id });
                    }

                    ModelState.AddModelError("Password", string.Join(';', result.Errors.Select(e => e.Description)));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Password", ex.Message);
            }

            return PartialView("_ModalResetPassword", model);
        }

        public async Task<IActionResult> DownloadExcelDocument(Guid id)
        {
            Person person = await _personService.GetByIdAsync(id);
            if(person == null) { return NotFound(); }

            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = $"Hoja de Vida - { person.FullName }.xlsx";
            try
            {
                string cvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Formats\HV.xlsx");
                using (var workbook = new XLWorkbook(cvPath))
                {
                    IXLWorksheet worksheet = workbook.Worksheets.FirstOrDefault();

                    #region Logo
                    var imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Formats\Logo.png");
                    var image = worksheet.AddPicture(imagePath)
                        .MoveTo(30, 25)
                        .Scale(1); // optional: resize picture
                    #endregion

                    #region Datos Personales
                    worksheet.Cell(9, 4).Value = person.FullName;
                    worksheet.Cell(10, 4).Value = person.IdentificationNumber;
                    worksheet.Cell(11, 4).Value = person.Email;
                    worksheet.Cell(12, 4).Value = person.PhoneNumber;
                    worksheet.Cell(10, 7).Value = person.Arl?.Name;
                    worksheet.Cell(11, 7).Value = person.Eps?.Name;
                    worksheet.Cell(12, 7).Value = person.BloodType?.Name;
                    #endregion

                    #region Roles
                    int jobRoleRow = 14;
                    foreach(PersonJobRole personJobRole in person.PeopleJobRole)
                    {
                        worksheet.Cell(jobRoleRow, 4).Value = personJobRole.JobRole.Name;
                        jobRoleRow++;
                    }
                    #endregion

                    #region Educación
                    BasicEducation firstBasicEducation = person.BasicEducations.FirstOrDefault();
                    worksheet.Cell(22, 2).Value = firstBasicEducation?.InstitutionName;
                    worksheet.Cell(22, 6).Value = firstBasicEducation?.SpecialtyName;
                    worksheet.Cell(22, 8).Value = firstBasicEducation?.DegreeDate;

                    int higherEducationRow = 23;
                    foreach (HigherEducation higherEducation in person.HigherEducations)
                    {
                        worksheet.Cell(higherEducationRow, 2).Value = higherEducation.InstitutionName;
                        worksheet.Cell(higherEducationRow, 4).Value = higherEducation.AcademicLevel?.Name;
                        worksheet.Cell(higherEducationRow, 6).Value = higherEducation.StudiesName;
                        worksheet.Cell(higherEducationRow, 8).Value = higherEducation.DegreeDate;
                        higherEducationRow++;
                    }
                    #endregion

                    #region Formación
                    int trainingRow = 30;
                    foreach (Training training in person.Trainings)
                    {
                        worksheet.Cell(trainingRow, 1).Value = training.Name;
                        worksheet.Cell(trainingRow, 3).Value = training.Subject;
                        worksheet.Cell(trainingRow, 4).Value = training.CertificationType?.Name;
                        worksheet.Cell(trainingRow, 5).Value = training.Entity;
                        worksheet.Cell(trainingRow, 7).Value = training.EndDate;
                        worksheet.Cell(trainingRow, 8).Value = training.NumberOfHours;
                        trainingRow++;
                    }
                    #endregion

                    #region Experiencia
                    int workExperienceRow = 45;
                    foreach (WorkExperience workExperience in person.WorkExperiences)
                    {
                        worksheet.Cell(workExperienceRow, 1).Value = workExperience.CompanyName;
                        worksheet.Cell(workExperienceRow, 2).Value = workExperience.JobTitle;
                        worksheet.Cell(workExperienceRow, 3).Value = workExperience.Responsibilities;
                        worksheet.Cell(workExperienceRow, 6).Value = workExperience.StartDate;
                        worksheet.Cell(workExperienceRow, 7).Value = workExperience.EndDate;

                        DateTime endDate = workExperience.EndDate.HasValue ?
                            workExperience.EndDate.Value : DateTime.Today;
                        int totalDays = (endDate - workExperience.StartDate).Days;
                        if (totalDays > 0)
                        {
                            var mod = totalDays % 365;
                            var years = (totalDays >= 365) ? (totalDays / 365) : 0;
                            var months = (mod > 0) ? (mod / 30) : 0;

                            worksheet.Cell(workExperienceRow, 8).Value = $"{years} Año(s) y {months} Mes(es)"; ;
                        }
                        workExperienceRow++;
                    }
                    #endregion

                    #region Conocimiento Técnico
                    int technicalKnowledgeRow = 57;
                    int initiaTechnicalKnowledgeColumn = 1;
                    foreach (TechnicalKnowledge technicalKnowledge in person.TechnicalKnowledges)
                    {
                        worksheet.Cell(technicalKnowledgeRow, initiaTechnicalKnowledgeColumn).Value = technicalKnowledge.Matrix?.Name;
                        worksheet.Cell(technicalKnowledgeRow, initiaTechnicalKnowledgeColumn + 1).Value = technicalKnowledge.AnalyticTechnique;
                        worksheet.Cell(technicalKnowledgeRow, initiaTechnicalKnowledgeColumn + 2).Value = technicalKnowledge.Matrix?.Name;
                        worksheet.Cell(technicalKnowledgeRow, initiaTechnicalKnowledgeColumn + 3).Value = technicalKnowledge.Time;

                        if(technicalKnowledgeRow == 61)
                        {
                            technicalKnowledgeRow = 57;
                            initiaTechnicalKnowledgeColumn = 5;
                        }
                        else
                        {
                            technicalKnowledgeRow++;
                        }
                    }
                    #endregion

                    #region Otros Conocimietos
                    int otherTechnicalRow = 63;
                    int initiaotherTechnicalColumn = 2;
                    foreach (var otherTechnical in person.OtherTechnicalKnowledges)
                    {
                        worksheet.Cell(otherTechnicalRow, initiaotherTechnicalColumn).Value = otherTechnical.Name;
                        worksheet.Cell(otherTechnicalRow, initiaotherTechnicalColumn + 2).Value = otherTechnical.Time;

                        if (otherTechnicalRow == 65)
                        {
                            otherTechnicalRow = 63;
                            initiaotherTechnicalColumn = 6;
                        }
                        else
                        {
                            otherTechnicalRow++;
                        }
                    }
                    #endregion

                    #region Habilidades
                    worksheet.Cell(68, 1).Value = person.Skills;
                    #endregion

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, contentType, fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
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