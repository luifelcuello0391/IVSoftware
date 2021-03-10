using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IVSoftware.Data.Models;
using IVSoftware.Web.Models;
using IVSoftware.Web.Service;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace IVSoftware.Web.Controllers
{
    public class MaintenanceModelsController : Controller
    {
        private readonly IVSoftwareContext _context;
        private readonly IEntityService<Person, Guid> _personService;
        private readonly IEntityService<Document, Guid> _documentService;
        private readonly IHostingEnvironment _env;

        public MaintenanceModelsController(IVSoftwareContext context, IEntityService<Person, Guid> PersonService, IEntityService<Document, Guid> documentService, IHostingEnvironment env)
        {
            _documentService = documentService;
            _env = env;
            this._personService = PersonService;
            _context = context;
        }

        // GET: MaintenanceModels
        public async Task<IActionResult> Index(string filter = "All")
        {
            ViewBag.filter = filter;

            switch(filter)
            {
                case "M":
                    var iVSoftwareContextM = _context.Maintenances.Include(m => m.Equip).Include(m => m.ServiceProvider).Include(m => m.Responsable).Where(m => "M".Equals(m.TypeOfMaintenanceId) && m.RegisterStatus > 0).OrderByDescending(x => x.MaintenanceDate);
                    return View(await iVSoftwareContextM.ToListAsync());

                case "C":
                    var iVSoftwareContextC = _context.Maintenances.Include(m => m.Equip).Include(m => m.ServiceProvider).Include(m => m.Responsable).Where(m => "C".Equals(m.TypeOfMaintenanceId) && m.RegisterStatus > 0).OrderByDescending(x => x.MaintenanceDate);
                    return View(await iVSoftwareContextC.ToListAsync());

                default:
                    var iVSoftwareContext = _context.Maintenances.Include(m => m.Equip).Include(m => m.ServiceProvider).Include(m => m.Responsable).Where(x => x.RegisterStatus > 0).OrderByDescending(x => x.MaintenanceDate);
                    return View(await iVSoftwareContext.ToListAsync());
            }
        }

        // GET: MaintenanceModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceModel = await _context.Maintenances
                .Include(m => m.Equip)
                .Include(m => m.ServiceProvider)
                .Include(m => m.Responsable)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (maintenanceModel == null)
            {
                return NotFound();
            }

            return View(maintenanceModel);
        }

        // GET: MaintenanceModels/Create
        public IActionResult Create()
        {
            ViewData["EquipId"] = new SelectList(_context.Equipment, "Id", "Name");
            ViewData["ProviderId"] = new SelectList(_context.Provider, "Id", "Name");
            return View();
        }

        // POST: MaintenanceModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeOfMaintenanceId,EquipId,MaintenanceDate,Diagnostic,WorkExecuted,NextCalibrationDate,Observations,ProviderId,Name,RegisterStatus,CreationDatetime,ModificationDatetime,PersonId")] MaintenanceModel maintenanceModel, List<IFormFile> postedFiles)
        {
            if (ModelState.IsValid)
            {
                Document certificate = null;

                if (postedFiles != null && postedFiles.Count > 0)
                {
                    certificate = new Document();
                    certificate.Id = Guid.NewGuid();
                    certificate.Name = postedFiles[0].FileName;
                    certificate.CreationDatetime = DateTime.Now;
                    certificate.ModiicationDatetime = DateTime.Now;
                    certificate.Url = "/AppDocuments/Maintenances/" + postedFiles[0].FileName;
                }

                maintenanceModel.MaintenanceCertificateDocument = certificate;
                maintenanceModel.MaintenanceCertificateDocumentId = certificate.Id;

                Equipment equipment = null;
                if(maintenanceModel.EquipId > 0)
                {
                    equipment = await _context.Equipment.FirstOrDefaultAsync<Equipment>(x => x.Id == maintenanceModel.EquipId && x.RegisterStatus > 0);
                }
                maintenanceModel.Equip = equipment;

                Provider provider = null;
                if(maintenanceModel.ProviderId > 0)
                {
                    provider = await _context.Provider.FirstOrDefaultAsync<Provider>(x => x.Id == maintenanceModel.ProviderId && x.RegisterStatus > 0);
                }
                maintenanceModel.ServiceProvider = provider;

                Person responsable = null;
                if(maintenanceModel.PersonId != null)
                {
                    IEnumerable<Person> people = await _personService.FindByConditionAsync(x => x.Id.ToString().Equals(maintenanceModel.PersonId.Value.ToString()));
                    if(people != null && people.Count() > 0)
                    {
                        responsable = people.FirstOrDefault();
                    }
                }
                maintenanceModel.Responsable = responsable;

                if(maintenanceModel.MaintenanceCertificateDocument != null)
                {
                    if (postedFiles[0].FileName.ToLower().EndsWith(".pdf"))
                    {
                        if (postedFiles[0].Length <= 2000000)
                        {
                            string path = Path.Combine(_env.WebRootPath, "AppDocuments", "Maintenances");

                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }

                            List<string> uploadedFiles = new List<string>();
                            foreach (IFormFile postedFile in postedFiles)
                            {
                                string fileName = Path.GetFileName(postedFile.FileName);
                                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                                {
                                    postedFile.CopyTo(stream);
                                    uploadedFiles.Add(fileName);
                                }
                            }

                            // Saves the maintenance certificate document information
                            _context.Add(certificate);
                            await _context.SaveChangesAsync();

                            _context.Add(maintenanceModel);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            ViewBag.Message = "El tamaño máximo del archivo debe ser 2 MB";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "El archivo debe ser un PDF";
                    }
                }
                else
                {
                    // Normal save
                    _context.Add(maintenanceModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewData["EquipId"] = new SelectList(_context.Equipment, "Id", "Name", maintenanceModel.EquipId);
            ViewData["ProviderId"] = new SelectList(_context.Provider, "Id", "Name", maintenanceModel.ProviderId);

            return View(maintenanceModel);
        }

        // GET: MaintenanceModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceModel = await _context.Maintenances.FindAsync(id);
            if (maintenanceModel == null)
            {
                return NotFound();
            }

            if(maintenanceModel.MaintenanceCertificateDocument != null)
            {
                maintenanceModel.MaintenanceCertificateDocumentId = maintenanceModel.MaintenanceCertificateDocument.Id;
            }

            ViewData["EquipId"] = new SelectList(_context.Equipment, "Id", "Name", maintenanceModel.EquipId);
            ViewData["ProviderId"] = new SelectList(_context.Provider, "Id", "Name", maintenanceModel.ProviderId);
            return View(maintenanceModel);
        }

        // POST: MaintenanceModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeOfMaintenanceId,EquipId,MaintenanceDate,Diagnostic,WorkExecuted,NextCalibrationDate,Observations,ProviderId,Name,RegisterStatus,CreationDatetime,ModificationDatetime,PersonId,MaintenanceCertificateDocumentId")] MaintenanceModel maintenanceModel, List<IFormFile> postedFiles)
        {
            if (id != maintenanceModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (postedFiles != null && postedFiles.Count > 0)
                    {
                        if (postedFiles[0].FileName.ToLower().EndsWith(".pdf"))
                        {
                            if (postedFiles[0].Length <= 2000000)
                            {
                                if (maintenanceModel.MaintenanceCertificateDocumentId != null)
                                {
                                    IEnumerable<Document> documents = await _documentService.FindByConditionAsync(x => x.Id == maintenanceModel.MaintenanceCertificateDocumentId);
                                    if (documents != null && documents.Count() > 0)
                                    {
                                        maintenanceModel.MaintenanceCertificateDocument = documents.FirstOrDefault();
                                    }
                                }

                                if (maintenanceModel.MaintenanceCertificateDocument != null)
                                {
                                    string previousFileName = maintenanceModel.MaintenanceCertificateDocument.Name;

                                    maintenanceModel.MaintenanceCertificateDocument.Name = postedFiles[0].FileName;
                                    maintenanceModel.MaintenanceCertificateDocument.ModiicationDatetime = DateTime.Now;
                                    maintenanceModel.MaintenanceCertificateDocument.Url = "/AppDocuments/Maintenances/" + postedFiles[0].FileName;

                                    _context.Update(maintenanceModel.MaintenanceCertificateDocument);
                                    await _context.SaveChangesAsync();

                                    string deletePath = Path.Combine(_env.WebRootPath, "AppDocuments", "Maintenances", previousFileName);

                                    // Delete file on physical path
                                    if (System.IO.File.Exists(deletePath))
                                    {
                                        System.IO.File.Delete(deletePath);
                                    }
                                }
                                else
                                {
                                    Document certificate = new Document();
                                    certificate.Id = Guid.NewGuid();
                                    certificate.Name = postedFiles[0].FileName;
                                    certificate.CreationDatetime = DateTime.Now;
                                    certificate.ModiicationDatetime = DateTime.Now;
                                    certificate.Url = "/AppDocuments/Maintenances/" + postedFiles[0].FileName;
                                    maintenanceModel.MaintenanceCertificateDocument = certificate;

                                    _context.Add(certificate);
                                    await _context.SaveChangesAsync();
                                }

                                // Add file to physical path
                                string path = Path.Combine(_env.WebRootPath, "AppDocuments", "Maintenances");

                                if (!Directory.Exists(path))
                                {
                                    Directory.CreateDirectory(path);
                                }

                                List<string> uploadedFiles = new List<string>();
                                foreach (IFormFile postedFile in postedFiles)
                                {
                                    string fileName = Path.GetFileName(postedFile.FileName);
                                    using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                                    {
                                        postedFile.CopyTo(stream);
                                        uploadedFiles.Add(fileName);
                                    }
                                }
                            }
                            else
                            {
                                ViewBag.Message = "El tamaño máximo del archivo debe ser 2 MB";

                                ViewData["EquipId"] = new SelectList(_context.Equipment, "Id", "Name", maintenanceModel.EquipId);
                                ViewData["ProviderId"] = new SelectList(_context.Provider, "Id", "Name", maintenanceModel.ProviderId);
                                return View(maintenanceModel);
                            }
                        }
                        else
                        {
                            ViewBag.Message = "El archivo debe ser un PDF";

                            ViewData["EquipId"] = new SelectList(_context.Equipment, "Id", "Name", maintenanceModel.EquipId);
                            ViewData["ProviderId"] = new SelectList(_context.Provider, "Id", "Name", maintenanceModel.ProviderId);
                            return View(maintenanceModel);
                        }
                    }


                    Equipment equipment = null;
                    if (maintenanceModel.EquipId > 0)
                    {
                        equipment = await _context.Equipment.FirstOrDefaultAsync<Equipment>(x => x.Id == maintenanceModel.EquipId && x.RegisterStatus > 0);
                    }
                    maintenanceModel.Equip = equipment;

                    Provider provider = null;
                    if (maintenanceModel.ProviderId > 0)
                    {
                        provider = await _context.Provider.FirstOrDefaultAsync<Provider>(x => x.Id == maintenanceModel.ProviderId && x.RegisterStatus > 0);
                    }
                    maintenanceModel.ServiceProvider = provider;

                    Person responsable = null;
                    if (maintenanceModel.PersonId != null)
                    {
                        IEnumerable<Person> people = await _personService.FindByConditionAsync(x => x.Id.ToString().Equals(maintenanceModel.PersonId.Value.ToString()));
                        if (people != null && people.Count() > 0)
                        {
                            responsable = people.FirstOrDefault();
                        }
                    }
                    maintenanceModel.Responsable = responsable;

                    _context.Update(maintenanceModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaintenanceModelExists(maintenanceModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipId"] = new SelectList(_context.Equipment, "Id", "Name", maintenanceModel.EquipId);
            ViewData["ProviderId"] = new SelectList(_context.Provider, "Id", "Name", maintenanceModel.ProviderId);
            return View(maintenanceModel);
        }

        // GET: MaintenanceModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceModel = await _context.Maintenances
                .Include(m => m.Equip)
                .Include(m => m.ServiceProvider)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maintenanceModel == null)
            {
                return NotFound();
            }

            return View(maintenanceModel);
        }

        // POST: MaintenanceModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maintenanceModel = await _context.Maintenances.FindAsync(id);
            _context.Maintenances.Remove(maintenanceModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaintenanceModelExists(int id)
        {
            return _context.Maintenances.Any(e => e.Id == id);
        }

        public async Task<IActionResult> GetPersons()
        {
            try
            {
                IEnumerable<Person> persons = await _personService.GetAllAsync();
                return PartialView("personSelectionList", persons);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on ServiceModelsController.GetPersons >> " + ex.ToString());
            }

            return null;
        }

        public async Task<IActionResult> GetProviders()
        {
            try
            {
                List<Provider> providers = await _context.Provider.ToListAsync();
                return PartialView("providerSelectionList", providers);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on ServiceModelsController.GetProviders >> " + ex.ToString());
            }

            return null;
        }

        public async Task<IActionResult> GetEquipments()
        {
            try
            {
                List<Equipment> equipments = await _context.Equipment.ToListAsync();
                return PartialView("equipmentSelectionList", equipments);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on ServiceModelsController.GetEquipments >> " + ex.ToString());
            }

            return null;
        }

        public async Task<IActionResult> GetPerson(string identification)
        {
            try
            {
                IEnumerable<Person> persons = await _personService.FindByConditionAsync(x => x.IdentificationNumber.Equals(identification));
                if (persons != null && persons.Count() > 0)
                {
                    return PartialView("ResponsableData", persons.First());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on MaintenanceModelsController.GetPerson >> " + ex.ToString());
            }

            return PartialView("ResponsableData", null);
        }

        public async Task<IActionResult> GetPersonByGuid(string identification)
        {
            try
            {
                IEnumerable<Person> persons = await _personService.FindByConditionAsync(x => x.Id.ToString().Equals(identification));
                if (persons != null && persons.Count() > 0)
                {
                    return PartialView("ResponsableData", persons.First());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on MaintenanceModelsController.GetPerson >> " + ex.ToString());
            }

            return PartialView("ResponsableData", null);
        }

        public async Task<IActionResult> GetProvider(int identification)
        {
            try
            {
                Provider provider = await _context.Provider.FirstOrDefaultAsync<Provider>(x => x.Id == identification && x.RegisterStatus > 0);
                if (provider != null)
                {
                    return PartialView("ProviderData", provider);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on MaintenanceModelsController.GetProvider >> " + ex.ToString());
            }

            return PartialView("ProviderData", null);
        }

        public async Task<IActionResult> GetEquipment(int identification)
        {
            try
            {
                Equipment equipment = await _context.Equipment.FirstOrDefaultAsync<Equipment>(x => x.Id == identification && x.RegisterStatus > 0);
                if (equipment != null)
                {
                    return PartialView("EquipmentData", equipment);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on MaintenanceModelsController.GetEquipment >> " + ex.ToString());
            }

            return PartialView("EquipmentData", null);
        }

        public ActionResult DownloadFile(string fileName)
        {
            try
            {
                string[] paths = fileName.Split("/");

                string path = Path.Combine(_env.WebRootPath, "AppDocuments", "Maintenances", paths[paths.Length - 1]);

                if (System.IO.File.Exists(path))
                {
                    var fs = System.IO.File.OpenRead(path);
                    return File(fs, "application/pdf", Path.GetFileName(paths[paths.Length - 1]));
                }
                else
                {
                    return NotFound();
                }
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
