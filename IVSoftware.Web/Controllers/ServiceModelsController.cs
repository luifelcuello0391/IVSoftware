using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IVSoftware.Models;
using IVSoftware.Web.Models;
using IVSoftware.Data.Models;
using IVSoftware.Web.Service;

namespace IVSoftware.Web.Controllers
{
    public class ServiceModelsController : Controller
    {
        private readonly IVSoftwareContext _context;
        private readonly IEntityService<Person, Guid> _personService;

        public ServiceModelsController(IVSoftwareContext context, IEntityService<Person, Guid> PersonService)
        {
            _personService = PersonService;
            _context = context;
        }

        // GET: ServiceModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.ServiceModel.ToListAsync());
        }

        // GET: ServiceModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceModel = await _context.ServiceModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceModel == null)
            {
                return NotFound();
            }

            return View(serviceModel);
        }

        // GET: ServiceModels/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                ViewBag.Units = await _context.MeasurementUnitModel.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on Create.Units >> " + ex.ToString());
            }

            try
            {
                ViewBag.ServiceTypes = await _context.TypeOfService.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on Create.ServiceTypes >> " + ex.ToString());
            }

            try
            {
                ViewBag.Matrixes = await _context.MatrixModel.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on Create.Matrixes >> " + ex.ToString());
            }

            try
            {
                ViewBag.Variables = await _context.VariableModel.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on Create.Variables >> " + ex.ToString());
            }

            try
            {
                ViewBag.ReferenceMethods = await _context.ReferenceMethodModel.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on Create.ReferenceMethods >> " + ex.ToString());
            }

            return View();
        }

        // POST: ServiceModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Description,UnitValue,Name,RegisterStatus,CreationDatetime,ModificationDatetime,TypeOfServiceId,SelectedMatrixGroupId,SelectedReferenceMethodId,AcredditedByIdeam,AuthorizedByINS,ReportDeliveryTime,Valid,BillingCode,BillingName,Precondition,MinimumValue,MaximumValue,PersonId,AvailableForClients,WeeklyAssignmentQuantity,BackupAnalystId,MaximumWeeklyAssignment,SelectedAnalisysTypeId,SelectedTypeOfSamplingId,VolumeEstablishedByLaboratory,SelectedStorageContainerId,WashingCleaningConditionsForContainer,SamplingCaptureDescription,PreservativesTheirConcentrationQuantity,StorageTemperature,AnalizeBefore,RegulatoryStorageTime,ReceptionDays,TestItemRetentionTimeInDays,RelatedProtocol,SelectedAnalisysTechniqueId,ReceptionOnMonday,ReceptionOnTuesday,ReceptionOnWednesday,ReceptionOnThursday,ReceptionOnFriday,ReceptionOnSaturday,ReceptionOnSunday,SelectedUnitId,AnalisysObservations,QuantificationLimit")] ServiceModel serviceModel)
        {
            if (ModelState.IsValid)
            {
                TypeOfService service_type = null;

                if (serviceModel.TypeOfServiceId > 0)
                {
                    service_type = await _context.TypeOfService.FindAsync(serviceModel.TypeOfServiceId);
                }

                serviceModel.ServiceType = service_type;

                MatrixModel matrix = null;
                if(serviceModel.SelectedMatrixGroupId > 0)
                {
                    matrix = await _context.MatrixModel.FindAsync(serviceModel.SelectedMatrixGroupId);
                }

                serviceModel.MatrixGroup = matrix;

                ReferenceMethodModel referenceMethod = null;
                if(serviceModel.SelectedReferenceMethodId > 0)
                {
                    referenceMethod = await _context.ReferenceMethodModel.FindAsync(serviceModel.SelectedReferenceMethodId);
                }

                serviceModel.ReferenceMethod = referenceMethod;

                Person responsable = null;
                if(serviceModel.PersonId != null)
                {
                    IEnumerable<Person> persons = await _personService.FindByConditionAsync(x => x.Id.Equals(serviceModel.PersonId));
                    if(persons != null && persons.Count() > 0)
                    {
                        responsable = persons.First();
                    }
                }
                serviceModel.Responsable = responsable;

                Person backupAnalyst = null;
                if (serviceModel.BackupAnalystId != null)
                {
                    IEnumerable<Person> persons = await _personService.FindByConditionAsync(x => x.Id.Equals(serviceModel.BackupAnalystId));
                    if (persons != null && persons.Count() > 0)
                    {
                        backupAnalyst = persons.First();
                    }
                }
                serviceModel.BackupAnalyst = backupAnalyst;

                AnalisysType analisys = null;
                if(serviceModel.SelectedAnalisysTypeId > 0)
                {
                    analisys = await _context.AnalisysType.FirstOrDefaultAsync(x => x.Id == serviceModel.SelectedAnalisysTypeId);
                }
                serviceModel.AnalisysType = analisys;

                AnalisysTechnique technique = null;
                if(serviceModel.SelectedAnalisysTechniqueId > 0)
                {
                    technique = await _context.AnalisysTechnique.FirstOrDefaultAsync(x => x.Id == serviceModel.SelectedAnalisysTechniqueId);
                }
                serviceModel.AnalisysTechnique = technique;

                SamplingType samplingType = null;
                if(serviceModel.SelectedTypeOfSamplingId > 0)
                {
                    samplingType = await _context.SamplingType.FirstOrDefaultAsync(x => x.Id == serviceModel.SelectedTypeOfSamplingId);
                }
                serviceModel.TypeOfSampling = samplingType;

                StorageContainer storage = null;
                if(serviceModel.SelectedStorageContainerId > 0)
                {
                    storage = await _context.StorageContainer.FirstOrDefaultAsync(x => x.Id == serviceModel.SelectedStorageContainerId);
                }
                serviceModel.StorageContainer = storage;

                MeasurementUnitModel unit = null;
                if(serviceModel.SelectedUnitId > 0)
                {
                    unit = await _context.MeasurementUnitModel.FirstOrDefaultAsync(x => x.Id == serviceModel.SelectedUnitId);
                }
                serviceModel.MeasurementUnit = unit;

                serviceModel.ReceptionDays = serviceModel.ObtainsReceptionDays;

                _context.Add(serviceModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceModel);
        }

        // GET: ServiceModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceModel = await _context.ServiceModel.FindAsync(id);

            if (serviceModel == null)
            {
                return NotFound();
            }

            if (serviceModel.MatrixGroup != null)
            {
                serviceModel.SelectedMatrixGroupId = serviceModel.MatrixGroup.Id;
            }

            if (serviceModel.ServiceType != null)
            {
                serviceModel.TypeOfServiceId = serviceModel.ServiceType.Id;
            }

            if(serviceModel.ReferenceMethod != null)
            {
                serviceModel.SelectedReferenceMethodId = serviceModel.ReferenceMethod.Id;
            }

            if(serviceModel.AnalisysType != null)
            {
                serviceModel.SelectedAnalisysTypeId = serviceModel.AnalisysType.Id;
            }

            if (serviceModel.AnalisysTechnique != null)
            {
                serviceModel.SelectedAnalisysTechniqueId = serviceModel.AnalisysTechnique.Id;
            }

            if (serviceModel.TypeOfSampling != null)
            {
                serviceModel.SelectedTypeOfSamplingId = serviceModel.TypeOfSampling.Id;
            }

            if (serviceModel.StorageContainer != null)
            {
                serviceModel.SelectedStorageContainerId = serviceModel.StorageContainer.Id;
            }

            if(serviceModel.BackupAnalyst != null)
            {
                serviceModel.BackupAnalystId = serviceModel.BackupAnalyst.Id;
            }

            if(serviceModel.Responsable != null)
            {
                serviceModel.PersonId = serviceModel.Responsable.Id;
            }

            try
            {
                ViewBag.ServiceTypes = await _context.TypeOfService.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on Create.ServiceTypes >> " + ex.ToString());
            }

            try
            {
                ViewBag.Matrixes = await _context.MatrixModel.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on Create.Matrixes >> " + ex.ToString());
            }

            try
            {
                ViewBag.Variables = await _context.VariableModel.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on Create.Variables >> " + ex.ToString());
            }

            try
            {
                ViewBag.ReferenceMethods = await _context.ReferenceMethodModel.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on Create.ReferenceMethods >> " + ex.ToString());
            }

            try
            {
                ViewBag.Units = await _context.MeasurementUnitModel.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on Create.Units >> " + ex.ToString());
            }

            return View(serviceModel);
        }

        // POST: ServiceModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Description,UnitValue,Name,RegisterStatus,CreationDatetime,ModificationDatetime,TypeOfServiceId,SelectedMatrixGroupId,SelectedReferenceMethodId,AcredditedByIdeam,AuthorizedByINS,ReportDeliveryTime,Valid,BillingCode,BillingName,Precondition,MinimumValue,MaximumValue,PersonId,AvailableForClients,WeeklyAssignmentQuantity,BackupAnalystId,MaximumWeeklyAssignment,SelectedAnalisysTypeId,SelectedTypeOfSamplingId,VolumeEstablishedByLaboratory,SelectedStorageContainerId,WashingCleaningConditionsForContainer,SamplingCaptureDescription,PreservativesTheirConcentrationQuantity,StorageTemperature,AnalizeBefore,RegulatoryStorageTime,ReceptionDays,TestItemRetentionTimeInDays,RelatedProtocol,SelectedAnalisysTechniqueId,ReceptionOnMonday,ReceptionOnTuesday,ReceptionOnWednesday,ReceptionOnThursday,ReceptionOnFriday,ReceptionOnSaturday,ReceptionOnSunday,SelectedUnitId,AnalisysObservations,QuantificationLimit")] ServiceModel serviceModel)
        {
            if (id != serviceModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    TypeOfService service_type = null;

                    if (serviceModel.TypeOfServiceId > 0)
                    {
                        service_type = await _context.TypeOfService.FindAsync(serviceModel.TypeOfServiceId);
                    }

                    serviceModel.ServiceType = service_type;

                    MatrixModel matrix = null;
                    if (serviceModel.SelectedMatrixGroupId > 0)
                    {
                        matrix = await _context.MatrixModel.FindAsync(serviceModel.SelectedMatrixGroupId);
                    }

                    serviceModel.MatrixGroup = matrix;

                    ReferenceMethodModel referenceMethod = null;
                    if (serviceModel.SelectedReferenceMethodId > 0)
                    {
                        referenceMethod = await _context.ReferenceMethodModel.FindAsync(serviceModel.SelectedReferenceMethodId);
                    }

                    serviceModel.ReferenceMethod = referenceMethod;

                    if(serviceModel.PersonId != null)
                    {
                        if(serviceModel.Responsable == null || 
                            (serviceModel.Responsable != null && !serviceModel.Responsable.Id.Equals(serviceModel.PersonId)))
                        {
                            IEnumerable<Person> persons = await _personService.FindByConditionAsync(x => x.Id.Equals(serviceModel.PersonId));
                            if (persons != null && persons.Count() > 0)
                            {
                                serviceModel.Responsable = persons.First();
                            }
                        }
                    }

                    Person backupAnalyst = null;
                    if (serviceModel.BackupAnalystId != null)
                    {
                        IEnumerable<Person> persons = await _personService.FindByConditionAsync(x => x.Id.Equals(serviceModel.BackupAnalystId));
                        if (persons != null && persons.Count() > 0)
                        {
                            backupAnalyst = persons.First();
                        }
                    }
                    serviceModel.BackupAnalyst = backupAnalyst;

                    AnalisysType analisys = null;
                    if (serviceModel.SelectedAnalisysTypeId > 0)
                    {
                        analisys = await _context.AnalisysType.FirstOrDefaultAsync(x => x.Id == serviceModel.SelectedAnalisysTypeId);
                    }
                    serviceModel.AnalisysType = analisys;

                    AnalisysTechnique technique = null;
                    if (serviceModel.SelectedAnalisysTechniqueId > 0)
                    {
                        technique = await _context.AnalisysTechnique.FirstOrDefaultAsync(x => x.Id == serviceModel.SelectedAnalisysTechniqueId);
                    }
                    serviceModel.AnalisysTechnique = technique;

                    SamplingType samplingType = null;
                    if (serviceModel.SelectedTypeOfSamplingId > 0)
                    {
                        samplingType = await _context.SamplingType.FirstOrDefaultAsync(x => x.Id == serviceModel.SelectedTypeOfSamplingId);
                    }
                    serviceModel.TypeOfSampling = samplingType;

                    StorageContainer storage = null;
                    if (serviceModel.SelectedStorageContainerId > 0)
                    {
                        storage = await _context.StorageContainer.FirstOrDefaultAsync(x => x.Id == serviceModel.SelectedStorageContainerId);
                    }
                    serviceModel.StorageContainer = storage;

                    MeasurementUnitModel unit = null;
                    if (serviceModel.SelectedUnitId > 0)
                    {
                        unit = await _context.MeasurementUnitModel.FirstOrDefaultAsync(x => x.Id == serviceModel.SelectedUnitId);
                    }
                    serviceModel.MeasurementUnit = unit;

                    serviceModel.ReceptionDays = serviceModel.ObtainsReceptionDays;

                    _context.Update(serviceModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceModelExists(serviceModel.Id))
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
            return View(serviceModel);
        }

        // GET: ServiceModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceModel = await _context.ServiceModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceModel == null)
            {
                return NotFound();
            }

            return View(serviceModel);
        }

        // POST: ServiceModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceModel = await _context.ServiceModel.FindAsync(id);

            try
            {
                if(serviceModel != null)
                {
                    foreach(var m in _context.ServiceGroupServicesRelation.Where(r => r.ServiceId == serviceModel.Id))
                    {
                        _context.ServiceGroupServicesRelation.Remove(m);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error on ServiceModelController.DeleteConfirmed >> " + ex.ToString());
            }

            _context.ServiceModel.Remove(serviceModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceModelExists(int id)
        {
            return _context.ServiceModel.Any(e => e.Id == id);
        }
        public async Task<IActionResult> GetPersons()
        {
            try
            {
                IEnumerable<Person> persons = await _personService.GetAllAsync();
                return PartialView("PersonSelectionList", persons);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error on ServiceModelsController.GetPersons >> " + ex.ToString());
            }

            return null;
        }

        public async Task<IActionResult> GetBackupPersons()
        {
            try
            {
                IEnumerable<Person> persons = await _personService.GetAllAsync();
                return PartialView("BackupPersonSelectionList", persons);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on ServiceModelsController.GetBackupPersons >> " + ex.ToString());
            }

            return null;
        }

        public async Task<IActionResult> GetPerson(string identification)
        {
            try
            {
                IEnumerable < Person > persons = await _personService.FindByConditionAsync(x => x.IdentificationNumber != null && x.IdentificationNumber.Equals(identification));
                if(persons != null && persons.Count() > 0)
                {
                    return PartialView("ResponsableData", persons.First());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on ServiceModelsController.GetPerson >> " + ex.ToString());
            }

            return PartialView("ResponsableData", null);
        }

        public async Task<IActionResult> GetBackupPerson(string identification)
        {
            try
            {
                IEnumerable<Person> persons = await _personService.FindByConditionAsync(x => x.IdentificationNumber != null && x.IdentificationNumber.Equals(identification));
                if (persons != null && persons.Count() > 0)
                {
                    return PartialView("BackupResponsableData", persons.First());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on ServiceModelsController.GetPerson >> " + ex.ToString());
            }

            return PartialView("BackupResponsableData", null);
        }

        public async Task<IActionResult> GetStorageContainers()
        {
            List<StorageContainer> containers = await _context.StorageContainer.ToListAsync();
            return PartialView("StorageContainerSelectionList", containers);
        }

        public async Task<IActionResult> GetAnalisysTypes()
        {
            List<AnalisysType> analisysTypes = await _context.AnalisysType.ToListAsync();
            return PartialView("AnalisysTypeSelectionList", analisysTypes);
        }

        public async Task<IActionResult> GetSamplingTypes()
        {
            List<SamplingType> samplingTypes = await _context.SamplingType.ToListAsync();
            return PartialView("SamplingTypeSelectionList", samplingTypes);
        }

        public async Task<string> GetStorageContainer(int identification)
        {
            StorageContainer container = await _context.StorageContainer.FirstOrDefaultAsync(x => x.Id == identification);

            if(container != null)
            {
                return string.Format("{0};{1}", container.Id, container.Name);
            }

            return null;
        }

        public async Task<string> GetSamplingType(int identification)
        {
            SamplingType sampling = await _context.SamplingType.FirstOrDefaultAsync(x => x.Id == identification);

            if(sampling != null)
            {
                return string.Format("{0};{1}", sampling.Id, sampling.Name);
            }

            return null;
        }

        public async Task<string> GetAnalisysType(int identification)
        {
            AnalisysType analisys = await _context.AnalisysType.FirstOrDefaultAsync(x => x.Id == identification);

            if(analisys != null)
            {
                return string.Format("{0};{1}", analisys.Id, analisys.Name);
            }

            return null;
        }

        public async Task<IActionResult> GetAnalisysTechniques()
        {
            List<AnalisysTechnique> techniques = await _context.AnalisysTechnique.ToListAsync();
            return PartialView("AnalisysTechniqueSelectionList", techniques);
        }

        public async Task<string> GetAnalisysTechnique(int identification)
        {
            AnalisysTechnique analisys = await _context.AnalisysTechnique.FirstOrDefaultAsync(x => x.Id == identification);

            if (analisys != null)
            {
                return string.Format("{0};{1}", analisys.Id, analisys.Name);
            }

            return null;
        }
    }
}
