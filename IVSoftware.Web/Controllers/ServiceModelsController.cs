using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IVSoftware.Models;
using IVSoftware.Web.Models;

namespace IVSoftware.Web.Controllers
{
    public class ServiceModelsController : Controller
    {
        private readonly IVSoftwareContext _context;

        public ServiceModelsController(IVSoftwareContext context)
        {
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
                ViewBag.WorkingRanges = await _context.WorkingRangeModel.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on Create.WorkingRanges >> " + ex.ToString());
            }

            return View();
        }

        // POST: ServiceModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Description,UnitValue,Name,RegisterStatus,CreationDatetime,ModificationDatetime,TypeOfServiceId,SelectedMatrixGroupId,SelectedReferenceMethodId,SelectedWorkingRangeId,AcredditedByIdeam,AuthorizedByINS,ReportDeliveryTime,Valid,BillingCode,BillingName")] ServiceModel serviceModel)
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

                WorkingRangeModel workingRange = null;
                if(serviceModel.SelectedWorkingRangeId > 0)
                {
                    workingRange = await _context.WorkingRangeModel.FindAsync(serviceModel.SelectedWorkingRangeId);
                }

                serviceModel.WorkingRange = workingRange;

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

            if(serviceModel.WorkingRange != null)
            {
                serviceModel.SelectedWorkingRangeId = serviceModel.WorkingRange.Id;
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
                ViewBag.WorkingRanges = await _context.WorkingRangeModel.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on Create.WorkingRanges >> " + ex.ToString());
            }

            return View(serviceModel);
        }

        // POST: ServiceModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Description,UnitValue,Name,RegisterStatus,CreationDatetime,ModificationDatetime,TypeOfServiceId,SelectedMatrixGroupId,SelectedReferenceMethodId,SelectedWorkingRangeId,AcredditedByIdeam,AuthorizedByINS,ReportDeliveryTime,Valid,BillingCode,BillingName")] ServiceModel serviceModel)
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

                    WorkingRangeModel workingRange = null;
                    if (serviceModel.SelectedWorkingRangeId > 0)
                    {
                        workingRange = await _context.WorkingRangeModel.FindAsync(serviceModel.SelectedWorkingRangeId);
                    }

                    serviceModel.WorkingRange = workingRange;

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
    }
}
