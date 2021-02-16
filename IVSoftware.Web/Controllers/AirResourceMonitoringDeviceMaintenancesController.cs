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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace IVSoftware.Web.Controllers
{
    public class AirResourceMonitoringDeviceMaintenancesController : Controller
    {
        private readonly IVSoftwareContext _context;
        private readonly IEntityService<Person, Guid> _personService;
        private readonly IEntityService<Document, Guid> _documentService;
        private readonly IHostingEnvironment _env;

        public AirResourceMonitoringDeviceMaintenancesController(IVSoftwareContext context, IEntityService<Person, Guid> PersonService, IEntityService<Document, Guid> documentService, IHostingEnvironment env)
        {
            _documentService = documentService;
            _env = env;
            this._personService = PersonService;
            _context = context;
        }

        // GET: AirResourceMonitoringDeviceMaintenances
        public async Task<IActionResult> Index(string filter = "All")
        {
            ViewBag.filter = filter;

            switch(filter)
            {
                case "R":
                    var iVSoftwareContextR = _context.AirResourceMonitoringDevideMaintenances.Include(a => a.Equipment).Where(m => "R".Equals(m.TypeOfMaintenanceId));
                    return View(await iVSoftwareContextR.ToListAsync());

                case "P":
                    var iVSoftwareContextP = _context.AirResourceMonitoringDevideMaintenances.Include(a => a.Equipment).Where(m => "P".Equals(m.TypeOfMaintenanceId));
                    return View(await iVSoftwareContextP.ToListAsync());

                case "C":
                    var iVSoftwareContextC = _context.AirResourceMonitoringDevideMaintenances.Include(a => a.Equipment).Where(m => "C".Equals(m.TypeOfMaintenanceId));
                    return View(await iVSoftwareContextC.ToListAsync());

                default:
                    var iVSoftwareContext = _context.AirResourceMonitoringDevideMaintenances.Include(a => a.Equipment);
                    return View(await iVSoftwareContext.ToListAsync());
            }
        }

        // GET: AirResourceMonitoringDeviceMaintenances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airResourceMonitoringDeviceMaintenance = await _context.AirResourceMonitoringDevideMaintenances
                .Include(a => a.Equipment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (airResourceMonitoringDeviceMaintenance == null)
            {
                return NotFound();
            }

            return View(airResourceMonitoringDeviceMaintenance);
        }

        // GET: AirResourceMonitoringDeviceMaintenances/Create
        public IActionResult Create()
        {
            ViewData["EquipId"] = new SelectList(_context.Equipment, "Id", "Name");
            return View();
        }

        // POST: AirResourceMonitoringDeviceMaintenances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeOfMaintenanceId,EquipId,MaintenanceDate,ProviderName,ProviderIdentificaton,ProviderAddress,ProviderPhoneNumber,ProviderContactName,SparePartsChanged,NextMaintenanceDate,Observations,Location,Description,StockNumber,Name,RegisterStatus,CreationDatetime,ModificationDatetime,PersonId")] AirResourceMonitoringDeviceMaintenance airResourceMonitoringDeviceMaintenance, List<IFormFile> postedFiles)
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

                airResourceMonitoringDeviceMaintenance.MaintenanceCertificateDocument = certificate;
                airResourceMonitoringDeviceMaintenance.MaintenanceCertificateDocumentId = certificate.Id;

                Equipment equipment = null;
                if(airResourceMonitoringDeviceMaintenance.EquipId > 0)
                {
                    equipment = await _context.Equipment.FirstOrDefaultAsync(x => x.Id == airResourceMonitoringDeviceMaintenance.EquipId);
                }
                airResourceMonitoringDeviceMaintenance.Equipment = equipment;

                Person responsable = null;
                if(airResourceMonitoringDeviceMaintenance.PersonId != null)
                {
                  IEnumerable<Person> persons = await _personService.FindByConditionAsync(x => x.Id.ToString().Equals(airResourceMonitoringDeviceMaintenance.PersonId.Value.ToString()));
                    if(persons != null && persons.Count() > 0)
                    {
                        responsable = persons.FirstOrDefault();
                    }
                }
                airResourceMonitoringDeviceMaintenance.Responsable = responsable;

                if (airResourceMonitoringDeviceMaintenance.MaintenanceCertificateDocument != null)
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

                            _context.Add(airResourceMonitoringDeviceMaintenance);
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
                    _context.Add(airResourceMonitoringDeviceMaintenance);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["EquipId"] = new SelectList(_context.Equipment, "Id", "Name", airResourceMonitoringDeviceMaintenance.EquipId);
            return View(airResourceMonitoringDeviceMaintenance);
        }

        // GET: AirResourceMonitoringDeviceMaintenances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airResourceMonitoringDeviceMaintenance = await _context.AirResourceMonitoringDevideMaintenances.FindAsync(id);
            if (airResourceMonitoringDeviceMaintenance == null)
            {
                return NotFound();
            }

            if(airResourceMonitoringDeviceMaintenance.MaintenanceCertificateDocument != null)
            {
                airResourceMonitoringDeviceMaintenance.MaintenanceCertificateDocumentId = airResourceMonitoringDeviceMaintenance.MaintenanceCertificateDocument.Id;
            }

            ViewData["EquipId"] = new SelectList(_context.Equipment, "Id", "Name", airResourceMonitoringDeviceMaintenance.EquipId);
            return View(airResourceMonitoringDeviceMaintenance);
        }

        // POST: AirResourceMonitoringDeviceMaintenances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeOfMaintenanceId,EquipId,MaintenanceDate,ProviderName,ProviderIdentificaton,ProviderAddress,ProviderPhoneNumber,ProviderContactName,SparePartsChanged,NextMaintenanceDate,Observations,Location,Description,StockNumber,Name,RegisterStatus,CreationDatetime,ModificationDatetime,PersonId,MaintenanceCertificateDocumentId")] AirResourceMonitoringDeviceMaintenance airResourceMonitoringDeviceMaintenance, List<IFormFile> postedFiles)
        {
            if (id != airResourceMonitoringDeviceMaintenance.Id)
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
                                if (airResourceMonitoringDeviceMaintenance.MaintenanceCertificateDocumentId != null)
                                {
                                    IEnumerable<Document> documents = await _documentService.FindByConditionAsync(x => x.Id == airResourceMonitoringDeviceMaintenance.MaintenanceCertificateDocumentId);
                                    if (documents != null && documents.Count() > 0)
                                    {
                                        airResourceMonitoringDeviceMaintenance.MaintenanceCertificateDocument = documents.FirstOrDefault();
                                    }
                                }

                                if (airResourceMonitoringDeviceMaintenance.MaintenanceCertificateDocument != null)
                                {
                                    string previousFileName = airResourceMonitoringDeviceMaintenance.MaintenanceCertificateDocument.Name;

                                    airResourceMonitoringDeviceMaintenance.MaintenanceCertificateDocument.Name = postedFiles[0].FileName;
                                    airResourceMonitoringDeviceMaintenance.MaintenanceCertificateDocument.ModiicationDatetime = DateTime.Now;
                                    airResourceMonitoringDeviceMaintenance.MaintenanceCertificateDocument.Url = "/AppDocuments/Maintenances/" + postedFiles[0].FileName;

                                    _context.Update(airResourceMonitoringDeviceMaintenance.MaintenanceCertificateDocument);
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
                                    airResourceMonitoringDeviceMaintenance.MaintenanceCertificateDocument = certificate;

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

                                ViewData["EquipId"] = new SelectList(_context.Equipment, "Id", "Name", airResourceMonitoringDeviceMaintenance.EquipId);
                                return View(airResourceMonitoringDeviceMaintenance);
                            }
                        }
                        else
                        {
                            ViewBag.Message = "El archivo debe ser un PDF";

                            ViewData["EquipId"] = new SelectList(_context.Equipment, "Id", "Name", airResourceMonitoringDeviceMaintenance.EquipId);
                            return View(airResourceMonitoringDeviceMaintenance);
                        }
                    }


                    Equipment equipment = null;
                    if (airResourceMonitoringDeviceMaintenance.EquipId > 0)
                    {
                        equipment = await _context.Equipment.FirstOrDefaultAsync(x => x.Id == airResourceMonitoringDeviceMaintenance.EquipId);
                    }
                    airResourceMonitoringDeviceMaintenance.Equipment = equipment;

                    Person responsable = null;
                    if (airResourceMonitoringDeviceMaintenance.PersonId != null)
                    {
                        IEnumerable<Person> persons = await _personService.FindByConditionAsync(x => x.Id.ToString().Equals(airResourceMonitoringDeviceMaintenance.PersonId.Value.ToString()));
                        if (persons != null && persons.Count() > 0)
                        {
                            responsable = persons.FirstOrDefault();
                        }
                    }
                    airResourceMonitoringDeviceMaintenance.Responsable = responsable;

                    _context.Update(airResourceMonitoringDeviceMaintenance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AirResourceMonitoringDeviceMaintenanceExists(airResourceMonitoringDeviceMaintenance.Id))
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
            ViewData["EquipId"] = new SelectList(_context.Equipment, "Id", "Name", airResourceMonitoringDeviceMaintenance.EquipId);
            return View(airResourceMonitoringDeviceMaintenance);
        }

        // GET: AirResourceMonitoringDeviceMaintenances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airResourceMonitoringDeviceMaintenance = await _context.AirResourceMonitoringDevideMaintenances
                .Include(a => a.Equipment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (airResourceMonitoringDeviceMaintenance == null)
            {
                return NotFound();
            }

            return View(airResourceMonitoringDeviceMaintenance);
        }

        // POST: AirResourceMonitoringDeviceMaintenances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var airResourceMonitoringDeviceMaintenance = await _context.AirResourceMonitoringDevideMaintenances.FindAsync(id);
            _context.AirResourceMonitoringDevideMaintenances.Remove(airResourceMonitoringDeviceMaintenance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AirResourceMonitoringDeviceMaintenanceExists(int id)
        {
            return _context.AirResourceMonitoringDevideMaintenances.Any(e => e.Id == id);
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

        public async Task<string> GetProvider(int identification)
        {
            try
            {
                Provider provider = await _context.Provider.FirstOrDefaultAsync<Provider>(x => x.Id == identification && x.RegisterStatus > 0);
                if (provider != null)
                {
                    return string.Format("{0};{1};{2};{3};{4};", provider.Identification, provider.Name, provider.Address, provider.PhoneNumber, provider.Contact);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on MaintenanceModelsController.GetProvider >> " + ex.ToString());
            }

            return null;
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
