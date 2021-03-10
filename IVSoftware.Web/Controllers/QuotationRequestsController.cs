using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IVSoftware.Models;
using IVSoftware.Web.Models;
using System.Net;
using IVSoftware.Data.Models;

namespace IVSoftware.Web.Controllers
{
    public class QuotationRequestsController : Controller
    {
        private readonly IVSoftwareContext _context;

        public QuotationRequestsController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: QuotationRequests
        public async Task<IActionResult> Index(string filter)
        {
            if(string.IsNullOrEmpty(filter))
            {
                ViewBag.filter = "0";
            }
            else
            {
                ViewBag.filter = filter;
            }

            switch(filter)
            {
                case "1": // Registered
                    return View(await _context.QuotationRequest.Where(x => x.Status != null && x.Status.Id == 1).ToListAsync());

                case "2": // Generated
                    return View(await _context.QuotationRequest.Where(x => x.Status != null && x.Status.Id == 2).ToListAsync());

                case "3": // Sent
                    return View(await _context.QuotationRequest.Where(x => x.Status != null && x.Status.Id == 3).ToListAsync());

                case "4": // Cancelled
                    return View(await _context.QuotationRequest.Where(x => x.Status != null && x.Status.Id == 4).ToListAsync());

                default:
                    return View(await _context.QuotationRequest.ToListAsync());
            }
        }

        // GET: QuotationRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotationRequest = await _context.QuotationRequest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quotationRequest == null)
            {
                return NotFound();
            }

            return View(quotationRequest);
        }

        // GET: QuotationRequests/Create
        public IActionResult Create()
        {
            TempData["serviceList"] = new List<int>();

            try
            {
                ViewBag.ActiveClients = _context.ClientModel.ToList();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error on QuotationRequestController.Create >> " + ex.ToString());
            }

            return View();
        }

        public async Task<ActionResult> GetClientInformation(string id)
        {
            if(id != null && !string.IsNullOrEmpty(id.Replace(" ", string.Empty)))
            {
                ClientModel client = null;
                try
                {
                    client = await _context.ClientModel.FirstOrDefaultAsync<ClientModel>(x => x.Identification != null && x.Identification.Equals(id));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error GetClientInformation >> " + ex.ToString());
                }

                return PartialView("clientMainInfo", client);
            }
            else
            {
                return PartialView("clientSelectionList", _context.ClientModel.ToList());
            }
        }

        // POST: QuotationRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HasBeenGenerated,LastGenerationDate,ClientRequestDescription,RequestDateTime,Name,RegisterStatus,CreationDatetime,ModificationDatetime,RequestedClientName,RequestedClientId")] QuotationRequest quotationRequest)
        {
            if (ModelState.IsValid)
            {
                quotationRequest.Client = await _context.ClientModel.FirstOrDefaultAsync<ClientModel>(x => x.Identification != null && x.Identification.Equals(quotationRequest.RequestedClientId));
                quotationRequest.Status = await _context.QuotationStatus.FirstOrDefaultAsync<QuotationStatus>(x => x.Id == 1);

                _context.Add(quotationRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quotationRequest);
        }


        public async Task<IActionResult> AddService (int serviceId, int quotationId, int isForManagement)
        {
            try
            {
                if (serviceId > 0)
                {
                    if (quotationId > 0)
                    {
                        QuotationRequest quotation = await _context.QuotationRequest.FirstOrDefaultAsync(x => x.Id == quotationId);

                        if (quotation != null)
                        {
                            if (quotation.Services == null)
                            {
                                quotation.Services = new List<ServicesIntoQuotation>();
                            }

                            if (quotation.Services.FirstOrDefault(x => x.ServiceId == serviceId) == null)
                            {
                                ServiceModel service = await _context.ServiceModel.FirstOrDefaultAsync(x => x.Id == serviceId);

                                if (service != null)
                                {
                                    ServicesIntoQuotation relation = new ServicesIntoQuotation();
                                    relation.QuotationRequestId = quotationId;
                                    relation.QuotationRequest = quotation;
                                    relation.ServiceId = serviceId;
                                    relation.Service = service;

                                    quotation.Services.Add(relation);

                                    try
                                    {
                                        _context.Update(quotation);
                                        await _context.SaveChangesAsync();
                                    }
                                    catch (DbUpdateConcurrencyException)
                                    {
                                        if (!QuotationRequestExists(quotationId))
                                        {
                                            return NotFound();
                                        }
                                        else
                                        {
                                            throw;
                                        }
                                    }
                                }
                                else
                                {
                                    return NotFound();
                                }
                            }

                            return RedirectToAction(nameof(Edit), new { id = quotationId, toManage = isForManagement });
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> RemoveService (int serviceId, int quotationId, int isForManagement)
        {
            if(serviceId > 0)
            {
                if(quotationId > 0)
                {
                    QuotationRequest quotation = await _context.QuotationRequest.FirstOrDefaultAsync(x => x.Id == quotationId);

                    if(quotation != null)
                    {
                        try
                        {
                            if(quotation.Services != null && quotation.Services.Count> 0)
                            {
                                ServicesIntoQuotation service = quotation.Services.FirstOrDefault(x => x.ServiceId == serviceId);

                                if (service != null)
                                {
                                    quotation.Services.Remove(service);

                                    try
                                    {
                                        _context.Update(quotation);
                                        await _context.SaveChangesAsync();
                                    }
                                    catch (DbUpdateConcurrencyException)
                                    {
                                        if (!QuotationRequestExists(quotationId))
                                        {
                                            return NotFound();
                                        }
                                        else
                                        {
                                            throw;
                                        }
                                    }
                                }
                            }
                        }
                        catch(Exception)
                        {
                            throw;
                        }
                    }
                    else
                    {
                        return NotFound();
                    }

                    return RedirectToAction(nameof(Edit), new { id = quotationId, toManage = isForManagement });
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> AddServiceByGroup (int groupId, int quotationId, int isForManagement)
        {
            try
            {
                if (groupId > 0)
                {
                    if (quotationId > 0)
                    {
                        QuotationRequest quotation = await _context.QuotationRequest.FirstOrDefaultAsync(x => x.Id == quotationId);

                        if (quotation != null)
                        {
                            if (quotation.Services == null)
                            {
                                quotation.Services = new List<ServicesIntoQuotation>();
                            }

                            ServiceGroupModel group = await _context.ServiceGroupModel.FirstOrDefaultAsync(x => x.Id == groupId);

                            if (group != null && group.Services != null && group.Services.Count > 0)
                            {
                                foreach (ServiceGroupServicesRelation service in group.Services)
                                {
                                    if (service != null && service.Service != null)
                                    {
                                        if (quotation.Services.FirstOrDefault(x => x.ServiceId == service.ServiceId) == null)
                                        {
                                            ServicesIntoQuotation relation = new ServicesIntoQuotation();
                                            relation.ServiceId = service.ServiceId;
                                            relation.Service = service.Service;
                                            relation.QuotationRequestId = quotationId;
                                            relation.QuotationRequest = quotation;

                                            quotation.Services.Add(relation);
                                        }
                                    }
                                }

                                try
                                {
                                    _context.Update(quotation);
                                    await _context.SaveChangesAsync();
                                }
                                catch (DbUpdateConcurrencyException)
                                {
                                    if (!QuotationRequestExists(quotationId))
                                    {
                                        return NotFound();
                                    }
                                    else
                                    {
                                        throw;
                                    }
                                }
                            }

                            return RedirectToAction(nameof(Edit), new { id = quotationId, toManage = isForManagement });
                        }
                    }
                }

                return NotFound();
            }
            catch(Exception)
            {
                throw;
            }
        }
        
        public async Task<IActionResult> SendQuotation (int quotationId, int? contactId, string email)
        {
            return NotFound();
        }
        
        public async Task<IActionResult> ManageQuotation (int quotationId)
        {
            if(quotationId > 0)
            {
                QuotationRequest quotation = await _context.QuotationRequest.FirstOrDefaultAsync(x => x.Id == quotationId);

                if(quotation != null)
                {
                    quotation.Status = await _context.QuotationStatus.FirstOrDefaultAsync<QuotationStatus>(x => x.Id == 2);
                    quotation.ModificationDatetime = DateTime.Now;
                    quotation.HasBeenGenerated = true;
                    quotation.LastGenerationDate = DateTime.Now;
                    // Add the user that generates the quotation

                    try
                    {
                        _context.Update(quotation);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!QuotationRequestExists(quotationId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Edit), new { id = quotationId, toManage = 1});
                }
            }

            return NotFound();
        }

        // GET QuotationRequests/Manage
        public async Task<IActionResult> Manage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotationRequest = await _context.QuotationRequest.FindAsync(id);
            if (quotationRequest == null)
            {
                return NotFound();
            }
            return RedirectToAction("Create", "QuotationModels", new { id = quotationRequest.Id });
        }

        // GET: QuotationRequests/Edit/5
        public async Task<IActionResult> Edit(int? id, int toManage = 0)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotationRequest = await _context.QuotationRequest.FindAsync(id);
            if (quotationRequest == null)
            {
                return NotFound();
            }

            try
            {
                ViewBag.Services = await _context.ServiceModel.ToListAsync();
            }catch(Exception ex)
            {
                Console.Write("Error on Edit.Quotation.Services >> " + ex.ToString());
            }

            try
            {
                ViewBag.Groups = await _context.ServiceGroupModel.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.Write("Error on Edit.Quotation.Groups >> " + ex.ToString());
            }

            quotationRequest.ManageQuotation = toManage;

            return View(quotationRequest);
        }

        // POST: QuotationRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HasBeenGenerated,LastGenerationDate,ClientRequestDescription,RequestDateTime,Name,RegisterStatus,CreationDatetime,ModificationDatetime")] QuotationRequest quotationRequest)
        {
            if (id != quotationRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quotationRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuotationRequestExists(quotationRequest.Id))
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
            return View(quotationRequest);
        }

        // GET: QuotationRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotationRequest = await _context.QuotationRequest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quotationRequest == null)
            {
                return NotFound();
            }

            return View(quotationRequest);
        }

        // POST: QuotationRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quotationRequest = await _context.QuotationRequest.FindAsync(id);
            if(quotationRequest != null)
            {
                var status = await _context.QuotationStatus.FindAsync(4);

                if(status != null)
                {
                    quotationRequest.Status = status;

                    _context.Update(quotationRequest);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        private bool QuotationRequestExists(int id)
        {
            return _context.QuotationRequest.Any(e => e.Id == id);
        }

        public async Task<IActionResult> GetAllServices()
        {
            List<ServiceModel> services = await _context.ServiceModel.Where(x => x.Valid).ToListAsync();

            return PartialView("ServiceSelectionList", services);
        }

        public async Task<IActionResult> GetServiceInformation(int id)
        {
            try
            {
                IEnumerable<ServiceModel> services = await _context.ServiceModel.Where(x => x.Id == id).ToListAsync();

                return PartialView("ServiceRegister", services);
            }
            catch(Exception)
            {
                return PartialView("ServiceRegister", null);
            }
            
        }

        public async Task<IActionResult> GetServiceGroups()
        {
            List<ServiceGroupModel> groups = await _context.ServiceGroupModel.ToListAsync();

            return PartialView("ServiceGroupSelectionList", groups);
        }

        public async Task<IActionResult> GetServicesFromGroup (int id, string currentAssignedServices)
        {
            try
            {
                List<ServiceGroupServicesRelation> servicesOnGroup = await _context.ServiceGroupServicesRelation.Where(x => x.ServiceGroupId == id).ToListAsync();

                if(servicesOnGroup != null && servicesOnGroup.Count > 0)
                {
                    List<ServiceModel> services = new List<ServiceModel>();
                    foreach(ServiceGroupServicesRelation service in servicesOnGroup)
                    {
                        if(service != null)
                        {
                            if(currentAssignedServices != null && !string.IsNullOrEmpty(currentAssignedServices.Replace(" ", string.Empty)))
                            {
                                if(!currentAssignedServices.Contains(service.ServiceId + ";"))
                                {
                                    // Only adds the services that has not been added yet
                                    services.Add(service.Service);
                                }
                            }
                            else // There is no assigned services
                            {
                                services.Add(service.Service);
                            }
                            
                        }
                    }

                    return PartialView("ServiceRegister", services);
                }

                return PartialView("ServiceRegister", null);
            }
            catch(Exception ex)
            {
                return PartialView("ServiceRegister", null);
            }
            
        }

        public async Task<IActionResult> GetIncentives()
        {
            return PartialView("IncentivesSelectionList", await _context.IncentiveModel.ToListAsync());
        }

        public async Task<IActionResult> SelectIncentive (int id)
        {
            try
            {
                IEnumerable<IncentiveModel> incentives = await _context.IncentiveModel.Where(x => x.Id == id).ToListAsync();

                return PartialView("IncentiveRegister", incentives);
            }
            catch (Exception)
            {
                return PartialView("IncentiveRegister", null);
            }
        }

        public async Task<QuotationRequest> PrepareQuotation(DateTime date, string client_request, int client_id, int? contact_id, string _services, string _incentives)
        {
            QuotationRequest request = new QuotationRequest();

            // General information
            request.RequestDateTime = date;
            request.CreationDatetime = DateTime.Now;
            request.ModificationDatetime = DateTime.Now;
            request.RegisterStatus = 1;
            request.Name = string.Format("C<consecutive>-{0}", DateTime.Now.Year);
            request.ClientRequestDescription = client_request;
            request.HasBeenGenerated = false;
            request.Status = await _context.QuotationStatus.FirstOrDefaultAsync(x => x.Id == 1);

            // Client information
            request.ClientId = client_id;
            request.Client = await _context.ClientModel.FirstOrDefaultAsync(x => x.Id == client_id);

            if (request.Client != null)
            {
                request.RequestedClientId = request.Client.Identification;
                request.RequestedClientName = request.Client.Name;
            }

            // Contact information
            if(contact_id != null)
            {
                request.ContactId = contact_id.Value;
                request.Contact = await _context.ClientContact.FirstOrDefaultAsync(x => x.Id == contact_id.Value);
            }

            // Services
            string[] servicesId = _services.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
            if (servicesId != null && servicesId.Length > 0)
            {
                request.Services = new List<ServicesIntoQuotation>();

                foreach (string id in servicesId)
                {
                    try
                    {
                        string[] id_quantity = id.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                        if (id_quantity != null && id_quantity.Length >= 2)
                        {
                            ServicesIntoQuotation service = new ServicesIntoQuotation();
                            service.ServiceId = int.Parse(id_quantity[0]);
                            service.Service = await _context.ServiceModel.FirstOrDefaultAsync(x => x.Id == service.ServiceId);

                            if (service.Service != null)
                            {
                                service.CurrentValue = service.Service.UnitValue;
                            }
                            service.QuotationRequest = request;
                            service.Quantity = int.Parse(id_quantity[1].Replace("quantity:", string.Empty));

                            request.Services.Add(service);
                        }
                    }
                    catch { }
                }
            }

            // Service total value and report time
            request.ServicesTotal = 0f;
            request.ServicesReportTime = 0;

            if (request.Services != null && request.Services.Count > 0)
            {
                foreach (ServicesIntoQuotation service in request.Services)
                {
                    if (service != null)
                    {
                        request.ServicesTotal += service.CurrentValue;

                        if (service.Service != null && service.Service.ReportDeliveryTime > request.ServicesReportTime)
                        {
                            request.ServicesReportTime = service.Service.ReportDeliveryTime;
                        }
                    }
                }
            }

            // Incentives
            if (_incentives != null && !string.IsNullOrEmpty(_incentives.Replace(" ", string.Empty)))
            {
                string[] incentivesId = _incentives.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                if (incentivesId != null && incentivesId.Length > 0)
                {
                    request.Incentives = new List<IncentivesIntoServiceQuotationRequest>();
                    int _id = 0;

                    foreach (string id in incentivesId)
                    {
                        try
                        {
                            _id = int.Parse(id);
                            IncentivesIntoServiceQuotationRequest incentives = new IncentivesIntoServiceQuotationRequest();
                            incentives.IncentiveId = _id;
                            incentives.Incentive = await _context.IncentiveModel.FirstOrDefaultAsync(x => x.Id == _id);

                            if (incentives.Incentive != null)
                            {
                                incentives.IncentiveCurrentValue = incentives.Incentive.Value;
                                incentives.IsPercentage = incentives.Incentive.IsPercentage;
                                incentives.ServiceTotalValue = request.ServicesTotal;
                            }

                            incentives.QuotationRequest = request;

                            request.Incentives.Add(incentives);
                        }
                        catch { }
                    }
                }
            }

            // Obtains the total from services
            request.QuotationTotalValue = request.ServicesTotal;

            // Quotation total
            if (request.Incentives != null && request.Incentives.Count > 0)
            {
                foreach (IncentivesIntoServiceQuotationRequest incentive in request.Incentives)
                {
                    if (incentive != null && incentive.Incentive != null)
                    {
                        request.QuotationTotalValue -= incentive.IsPercentage ? incentive.IncentiveValueFromTotal : incentive.IncentiveCurrentValue;
                    }
                }
            }

            // Obtains the taxes
            List<TaxModel> taxes = await _context.Taxes.ToListAsync();
            if (taxes != null && taxes.Count > 0)
            {
                request.Taxes = new List<TaxesIntoServiceQuotationRequest>();

                foreach (TaxModel tax in taxes)
                {
                    if (tax != null)
                    {
                        TaxesIntoServiceQuotationRequest _tax = new TaxesIntoServiceQuotationRequest();
                        _tax.TaxId = tax.Id;
                        _tax.Tax = tax;
                        _tax.CurrentTaxValue = tax.Currentvalue;
                        _tax.QuotationRequest = request;
                        _tax.QuotationTotal = request.QuotationTotalValue;

                        request.Taxes.Add(_tax);
                    }
                }
            }

            request.QuotationTotalValueAfterTaxes = request.QuotationTotalValue;

            // Taxes
            if (request.Taxes != null && request.Taxes.Count > 0)
            {
                foreach (TaxesIntoServiceQuotationRequest tax in request.Taxes)
                {
                    if (tax != null)
                    {
                        request.QuotationTotalValueAfterTaxes += tax.ValueFromQuotationTotal;
                    }
                }
            }

            return request;
        }

        public async Task<IActionResult> ConfirmQuotation (DateTime date, string client_request, int client_id, int contact_id, string _services, string _incentives)
        {
            return PartialView("QuotationRequestResume", await PrepareQuotation(date, client_request, client_id, contact_id, _services, _incentives));
        }

        public async Task<string> SaveQuotation (DateTime date, string client_request, int client_id, int contact_id, string _services, string _incentives)
        {
            QuotationRequest request = await PrepareQuotation(date, client_request, client_id, contact_id, _services, _incentives);

            if(request != null)
            {
                try
                {
                    _context.Add(request);
                    await _context.SaveChangesAsync();

                    return "OK";
                }
                catch(Exception ex)
                {
                    return string.Format("Error: {0}", ex.ToString());
                }
            }
            else
            {
                return "Error: No hay información de la cotización";
            }
        }
    }
}
