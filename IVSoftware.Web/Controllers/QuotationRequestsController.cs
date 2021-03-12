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
using IVSoftware.Web.Data;
using IVSoftware.Web.Service;
using Microsoft.AspNetCore.Identity;

namespace IVSoftware.Web.Controllers
{
    public class QuotationRequestsController : Controller
    {
        private readonly IVSoftwareContext _context;
        private readonly IMailService _mailService;
        private readonly UserManager<User> _userManager;
        private readonly IEntityService<Person, Guid> _personService;

        public QuotationRequestsController(IVSoftwareContext context, IMailService mailService, UserManager<User> userManager, IEntityService<Person, Guid> personService)
        {
            _personService = personService;
            _userManager = userManager;
            _mailService = mailService;
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
                    return View(await _context.QuotationRequest.Where(x => x.Status != null && x.Status.Id == 1).OrderByDescending(x => x.RequestDateTime).ToListAsync());

                case "2": // Generated
                    return View(await _context.QuotationRequest.Where(x => x.Status != null && x.Status.Id == 2).OrderByDescending(x => x.RequestDateTime).ToListAsync());

                case "3": // Sent
                    return View(await _context.QuotationRequest.Where(x => x.Status != null && x.Status.Id == 3).OrderByDescending(x => x.RequestDateTime).ToListAsync());

                case "4": // Cancelled
                    return View(await _context.QuotationRequest.Where(x => x.Status != null && x.Status.Id == 4).OrderByDescending(x => x.RequestDateTime).ToListAsync());

                default:
                    return View(await _context.QuotationRequest.OrderByDescending(x => x.RequestDateTime).ToListAsync());
            }
        }

        // GET: QuotationRequests/QuotationClientConfirmation
        public async Task<IActionResult> QuotationClientConfirmation(int? id)
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

            if (quotationRequest.Incentives != null && quotationRequest.Incentives.Count > 0)
            {
                foreach (IncentivesIntoServiceQuotationRequest incentive in quotationRequest.Incentives)
                {
                    if (incentive != null)
                    {
                        incentive.ServiceTotalValue = quotationRequest.ServicesTotal;
                    }
                }
            }

            if (quotationRequest.Taxes != null && quotationRequest.Taxes.Count > 0)
            {
                foreach (TaxesIntoServiceQuotationRequest tax in quotationRequest.Taxes)
                {
                    if (tax != null)
                    {
                        tax.QuotationTotal = quotationRequest.QuotationTotalValue;
                    }
                }
            }

            return View(quotationRequest);
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

            if (quotationRequest.Incentives != null && quotationRequest.Incentives.Count > 0)
            {
                foreach (IncentivesIntoServiceQuotationRequest incentive in quotationRequest.Incentives)
                {
                    if (incentive != null)
                    {
                        incentive.ServiceTotalValue = quotationRequest.ServicesTotal;
                    }
                }
            }

            if (quotationRequest.Taxes != null && quotationRequest.Taxes.Count > 0)
            {
                foreach(TaxesIntoServiceQuotationRequest tax in quotationRequest.Taxes)
                {
                    if(tax != null)
                    {
                        tax.QuotationTotal = quotationRequest.QuotationTotalValue;
                    }
                }
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

        public async Task<ActionResult> GetClientInformation(string id, int? selectedContactId = null)
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

                if(client != null)
                {
                    client.SelectedContactId = selectedContactId;
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

            var quotationRequest = await _context.QuotationRequest.FirstOrDefaultAsync(x => x.Id == id.Value);
            if (quotationRequest == null)
            {
                return NotFound();
            }

            if (quotationRequest.Incentives != null && quotationRequest.Incentives.Count > 0)
            {
                foreach (IncentivesIntoServiceQuotationRequest incentive in quotationRequest.Incentives)
                {
                    if (incentive != null)
                    {
                        incentive.ServiceTotalValue = quotationRequest.ServicesTotal;
                    }
                }
            }

            if (quotationRequest.Taxes != null && quotationRequest.Taxes.Count > 0)
            {
                foreach (TaxesIntoServiceQuotationRequest tax in quotationRequest.Taxes)
                {
                    if (tax != null)
                    {
                        tax.QuotationTotal = quotationRequest.QuotationTotalValue;
                    }
                }
            }

            return View("Manage", quotationRequest);
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

            if(quotationRequest.Services != null && quotationRequest.Services.Count > 0)
            {
                try
                {
                    ViewBag.AssignedServices = string.Empty;
                    foreach(ServicesIntoQuotation service in quotationRequest.Services)
                    {
                        if(service != null && service.ServiceId != null)
                        {
                            ViewBag.AssignedServices += string.Format("{0};", service.ServiceId.Value);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write("Error on Edit.Quotation.AssignedServices >> " + ex.ToString());
                }
            }

            if(quotationRequest.Incentives != null && quotationRequest.Incentives.Count > 0)
            {
                try
                {
                    ViewBag.AssignedIncentives = string.Empty;
                    foreach(IncentivesIntoServiceQuotationRequest incentives in quotationRequest.Incentives)
                    {
                        if(incentives != null && incentives.IncentiveId != null)
                        {
                            ViewBag.AssignedIncentives += string.Format("{0};", incentives.IncentiveId);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write("Error on Edit.Quotation.AssignedIncentives >> " + ex.ToString());
                }
            }

            quotationRequest.RequestedClientId = quotationRequest.ClientIdentification;
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

            if (quotationRequest.Incentives != null && quotationRequest.Incentives.Count > 0)
            {
                foreach (IncentivesIntoServiceQuotationRequest incentive in quotationRequest.Incentives)
                {
                    if (incentive != null)
                    {
                        incentive.ServiceTotalValue = quotationRequest.ServicesTotal;
                    }
                }
            }

            if (quotationRequest.Taxes != null && quotationRequest.Taxes.Count > 0)
            {
                foreach (TaxesIntoServiceQuotationRequest tax in quotationRequest.Taxes)
                {
                    if (tax != null)
                    {
                        tax.QuotationTotal = quotationRequest.QuotationTotalValue;
                    }
                }
            }

            return View(quotationRequest);
        }

        // POST: QuotationRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string response = null)
        {
            var quotationRequest = await _context.QuotationRequest.FindAsync(id);
            if(quotationRequest != null)
            {
                var status = await _context.QuotationStatus.FindAsync(4);

                if(status != null)
                {
                    if(response == null || string.IsNullOrEmpty(response.Replace(" ", string.Empty)))
                    {
                        response = "La cotización ha sido cancelada por el personal interno.";
                    }

                    quotationRequest.CancelationResponse = response;
                    quotationRequest.Status = status;
                    quotationRequest.ModificationDatetime = DateTime.Now;

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

        public async Task<QuotationRequest> ObtainsQuotationForEdition (QuotationRequest quotation, int? quotation_id)
        {
            // QuotationRequest quotation = await PrepareQuotation(date, client_request, client_id, contact_id, _services, _incentives);

            QuotationRequest currentQuotation = null;

            if(quotation_id != null)
            {
                currentQuotation = await _context.QuotationRequest.FirstOrDefaultAsync(x => x.Id == quotation_id.Value);

                if (quotation != null && currentQuotation != null)
                {
                    currentQuotation.ContactId = quotation.ContactId;
                    currentQuotation.Contact = quotation.Contact;

                    currentQuotation.RequestDateTime = quotation.RequestDateTime;
                    currentQuotation.ClientRequestDescription = quotation.ClientRequestDescription;
                    currentQuotation.QuotationTotalValue = quotation.QuotationTotalValue;
                    currentQuotation.QuotationTotalValueAfterTaxes = quotation.QuotationTotalValueAfterTaxes;

                    if (currentQuotation.Incentives != null) currentQuotation.Incentives.Clear();

                    currentQuotation.Incentives = quotation.Incentives;
                    if (currentQuotation.Incentives != null && currentQuotation.Incentives.Count > 0)
                    {
                        foreach (IncentivesIntoServiceQuotationRequest incentive in currentQuotation.Incentives)
                        {
                            if (incentive != null)
                            {
                                incentive.QuotationRequest = currentQuotation;
                                incentive.QuotationRequestId = currentQuotation.Id;
                            }
                        }
                    }

                    if (currentQuotation.Services != null) currentQuotation.Services.Clear();

                    currentQuotation.Services = quotation.Services;
                    if (currentQuotation.Services != null)
                    {
                        foreach (ServicesIntoQuotation service in currentQuotation.Services)
                        {
                            if (service != null)
                            {
                                service.QuotationRequest = currentQuotation;
                                service.QuotationRequestId = currentQuotation.Id;
                            }
                        }
                    }

                    currentQuotation.ServicesReportTime = quotation.ServicesReportTime;
                    currentQuotation.ServicesTotal = quotation.ServicesTotal;

                    if (currentQuotation.Taxes != null) currentQuotation.Taxes.Clear();
                    currentQuotation.Taxes = quotation.Taxes;
                    if (currentQuotation.Taxes != null && currentQuotation.Taxes.Count > 0)
                    {
                        foreach (TaxesIntoServiceQuotationRequest tax in currentQuotation.Taxes)
                        {
                            if (tax != null)
                            {
                                tax.QuotationRequest = currentQuotation;
                                tax.QuotationRequestId = currentQuotation.Id;
                            }
                        }
                    }
                }
            }

            return currentQuotation;
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
                        request.ServicesTotal += service.TotalValue;

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

        public async Task<IActionResult> ConfirmQuotation (DateTime date, string client_request, int client_id, int contact_id, string _services, string _incentives, int? quotation_id)
        {
            if(quotation_id != null)
            {
                return PartialView("QuotationRequestResume", await ObtainsQuotationForEdition(await PrepareQuotation(date, client_request, client_id, contact_id, _services, _incentives), quotation_id));
            }
            else
            {
                return PartialView("QuotationRequestResume", await PrepareQuotation(date, client_request, client_id, contact_id, _services, _incentives));
            }
        }

        public async Task<string> SaveQuotation (DateTime date, string client_request, int client_id, int contact_id, string _services, string _incentives, int? quotation_id)
        {
            QuotationRequest request = null;

            if(quotation_id != null)
            {
                // Edition
                request = await ObtainsQuotationForEdition(await PrepareQuotation(date, client_request, client_id, contact_id, _services, _incentives), quotation_id);
                if(request != null)
                {
                    request.ModificationDatetime = DateTime.Now;
                }
            }
            else 
            {
                // Creation
                request = await PrepareQuotation(date, client_request, client_id, contact_id, _services, _incentives);
            }

            try
            {
                User currentUser = await _userManager.GetUserAsync(User);
                if (currentUser != null && request != null)
                {
                    request.GenerationUsed = (await _personService.FindByConditionAsync(x => x.UserId != null && x.UserId.Equals(currentUser.Id))).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on CaptureCheckListResponse.UserData >> " + ex.ToString());
            }


            if (request != null)
            {
                try
                {
                    if(quotation_id != null)
                    {
                        // Edition
                        _context.Update(request);
                    }
                    else
                    {
                        // Creation
                        _context.Add(request);
                    }
                    
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

        public async Task<IActionResult> GetServicesOnLoad (string id, int quotationId)
        {
            if (id != null && !string.IsNullOrEmpty(id.Replace(" ", string.Empty)))
            {
                string[] ids = id.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                if(ids != null && ids.Length > 0)
                {
                    List<ServiceModel> services = new List<ServiceModel>();
                    foreach(string _id in ids)
                    {
                        if(_id != null && !string.IsNullOrEmpty(_id.Replace(" ", string.Empty)))
                        {
                            int Serviceid = int.Parse(_id);
                            ServiceModel service = await _context.ServiceModel.FirstOrDefaultAsync(x => x.Id == Serviceid);
                            if(service != null)
                            {
                                ServicesIntoQuotation serviceInto = await _context.ServicesIntoQuotation.FirstOrDefaultAsync(x => x.ServiceId == service.Id && x.QuotationRequestId == quotationId);
                                if (serviceInto != null)
                                {
                                    service.ServiceQuantity = serviceInto.Quantity;
                                }

                                services.Add(service);
                            }
                        }
                    }

                    return PartialView("ServiceRegister", services);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<IActionResult> GetIncentivesOnLoad(string id)
        {
            if (id != null && !string.IsNullOrEmpty(id.Replace(" ", string.Empty)))
            {
                string[] ids = id.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                if (ids != null && ids.Length > 0)
                {
                    List<IncentiveModel> incentives = new List<IncentiveModel>();
                    foreach (string _id in ids)
                    {
                        if (_id != null && !string.IsNullOrEmpty(_id.Replace(" ", string.Empty)))
                        {
                            int IncentiveId = int.Parse(_id);
                            IncentiveModel incentive = await _context.IncentiveModel.FirstOrDefaultAsync(x => x.Id == IncentiveId);
                            if(incentive != null) incentives.Add(incentive);
                        }
                    }

                    return PartialView("IncentiveRegister", incentives);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<string> ConfirmQuotationManagement (int id)
        {
            if(id > 0)
            {
                QuotationRequest request = await _context.QuotationRequest.FirstOrDefaultAsync(x => x.Id == id);

                if(request != null)
                {
                    try
                    {
                        // Assigns the status: 'Generado'
                        QuotationStatus status = await _context.QuotationStatus.FirstOrDefaultAsync(x => x.Id == 2);
                        if(status != null)
                        {
                            request.Status = status;
                            request.ModificationDatetime = DateTime.Now;
                            request.LastGenerationDate = DateTime.Now;

                            _context.Update(request);
                            await _context.SaveChangesAsync();

                            return "OK";
                        }
                        else
                        {
                            return "Error: No fué posible asignar el estado 'Generado' porque no existe en la base de datos.";
                        }
                    }
                    catch(Exception ex)
                    {
                        return string.Format("Error: {0}", ex.ToString());
                    }
                }
                else
                {
                    return "Error: No hay información de la solicitud de cotización.";
                }
            }
            else
            {
                return "Error: No hay información de la solicitud de cotización.";
            }
        }

        public async Task<string> SendQuotationConfirmationEmail(int id)
        {
            if(id > 0)
            {
                QuotationRequest request = await _context.QuotationRequest.FirstOrDefaultAsync(x => x.Id == id);

                if(request != null)
                {
                    try
                    {
                        string contactEmail = null;
                        string emailBody = null;

                        if (request.Contact != null && !string.IsNullOrEmpty(request.Contact.ReportDeliveryEmail.Replace(" ", string.Empty)))
                        {
                            contactEmail = request.Contact.ReportDeliveryEmail;
                        }
                        else if (request.Client != null && !string.IsNullOrEmpty(request.Client.EmailAddress.Replace(" ", string.Empty)))
                        {
                            contactEmail = request.Client.EmailAddress;
                        }

                        if(contactEmail != null && !string.IsNullOrEmpty(contactEmail.Replace(" ", string.Empty)))
                        {
                            emailBody = OrganizeEmailBody(request);

                            if (emailBody != null && !string.IsNullOrEmpty(emailBody.Replace(" ", string.Empty)))
                            {
                                MailRequest email = new MailRequest()
                                {
                                    ToEmail = contactEmail,
                                    Subject = string.Format("Confirmación de cotización {0}", request.Name.Replace("<consecutive>", request.Id.ToString())),
                                    Body = emailBody
                                    // CC = personEvaluation.Person.OthersEmail
                                };

                                await _mailService.SendEmailAsync(email);

                                return string.Format("OK: Correo enviado a {0}.", contactEmail);
                            }
                            else
                            {
                                return "Error: No fué posible crear el contenido del correo de confirmación.";
                            }
                        }
                        else
                        {
                            return "Error: No hay un correo electrónico registrado para el cliente.";
                        }
                    }
                    catch(Exception ex)
                    {
                        return string.Format("Error: {0}", ex.ToString());
                    }
                }
                else
                {
                    return "Error: No hay información de la cotización.";
                }
            }
            else
            {
                return "Error: El id de la solicitud no es válida.";
            }
        }

        private string OrganizeEmailBody (QuotationRequest request)
        {
            if(request != null)
            {
                string builder = string.Empty;

                var fullUrl = this.Url.Action("QuotationClientConfirmation", "QuotationRequests", new { id = request.Id }, this.Request.Scheme);

                #region Greetings
                // Company contact
                if (request.Contact != null && request.Contact.Name != null && !string.IsNullOrEmpty(request.Contact.Name.Replace(" ", string.Empty)))
                {
                    builder += string.Format("{0}<br><br>Cordial saludo<br><br>", request.Contact.Name);
                }
                else if (request.Client != null && request.Client.ContactName != null && !string.IsNullOrEmpty(request.Client.ContactName.Replace(" ", string.Empty)))
                {
                    builder += string.Format("{0}<br><br>Cordial saludo<br><br>", request.Client.ContactName);
                }
                #endregion

                #region Phone numbers information
                string phoneNumber = null;

                if(request.Contact != null && request.Contact.PhoneNumber != null && !string.IsNullOrEmpty(request.Contact.PhoneNumber.Replace(" ", string.Empty)))
                {
                    // Adds the contact phone number
                    phoneNumber = request.Contact.PhoneNumber;

                    if(request.Contact.Extension != null && !string.IsNullOrEmpty(request.Contact.Extension.Replace(" ", string.Empty)))
                    {
                        if(!string.IsNullOrEmpty(phoneNumber))
                        {
                            phoneNumber += string.Format(" EXT. {0}", request.Contact.Extension);
                        }
                    }
                }
                else if (request.Client != null)
                {
                    
                    if(request.Client.PhoneNumber != null && !string.IsNullOrEmpty(request.Client.PhoneNumber.Replace(" ", string.Empty)))
                    {
                        // Adds the client main phone number
                        phoneNumber = request.Client.PhoneNumber;

                        if(request.Client.Extension != null && !string.IsNullOrEmpty(request.Client.Extension))
                        {
                            if(!string.IsNullOrEmpty(phoneNumber))
                            {
                                phoneNumber += string.Format(" EXT. {0}", request.Client.Extension);
                            }
                        }
                    }
                    
                    if (request.Client.CellPhone != null && !string.IsNullOrEmpty(request.Client.CellPhone.Replace(" ", string.Empty)))
                    {
                        // Adds the client alternative phone number
                        if(!string.IsNullOrEmpty(phoneNumber))
                        {
                            phoneNumber += string.Format(" - {0}", request.Client.CellPhone); 
                        }
                        else
                        {
                            // It is empty
                            phoneNumber = request.Client.CellPhone;
                        }
                    }
                }
                #endregion

                builder += string.Format("Su solicitud de cotización ha sido confirmada con el siguiente código <b>{0}</b>, cualquier modificación, nos comunicaremos con usted {1} para aclarar o informar particularidades de su solicitud.<br><br>", 
                    request.Name.Replace("<consecutive>", request.Id.ToString()),
                    !string.IsNullOrEmpty(phoneNumber) ? string.Format("a los siguientes números de teléfono {0}", phoneNumber) : "a través de este medio");

                if(!string.IsNullOrEmpty(fullUrl))
                {
                    builder += string.Format("Haga click <a href=\"{0}\">Aquí</a> para aceptar o cancelar la cotización.<br><br>", fullUrl);
                }
                else
                {
                    builder += string.Format("Para aceptar y continuar con el proceso de reserva de cupo para la toma de muestras, deberá enviarnos un mensaje al correo electrónico <b>XXXXXX@cornare.com</b> con el código de la cotización: <b>{0}</b><br><br>", request.Name.Replace("<consecutive>", request.Id.ToString()));
                }

                builder += "Los datos suministrados serán tratados de acuerdo a la política de protección de datos Resolución 112-4540 del 25 de octubre de 2018 \"Por medio de la cual se adopta la Política de Protección de Datos Personales en la Corporación Autónoma Regional de las Cuencas de los Ríos Negro y Nare -Cornare\": https://www.cornare.gov.co/politica-de-datos-personales/, en caso de no estar de acuerdo favor responder este correo solicitando el retiro de nuestra base de datos.<br><br>";

                builder += "Cualquier inquietud no dude en comunicarse con nosotros.<br><br>";

                if(request.GenerationUsed != null && request.GenerationUsed.FullName != null && !string.IsNullOrEmpty(request.GenerationUsed.FullName.Replace(" ", string.Empty)))
                {
                    builder += string.Format("Su solicitud fué recibida por {0}.<br><br>", request.GenerationUsed.FullName);
                }

                return builder;

            }

            return null;
        }

        public async Task<IActionResult> QuotationResult(int? id)
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

        public async Task<string> AcceptQuotation (int id)
        {
            try
            {
                QuotationRequest request = await _context.QuotationRequest.FirstOrDefaultAsync(x => x.Id == id);

                if (request != null)
                {
                    try
                    {
                        if (request.Status != null)
                        {
                            if (request.Status.Id == 4)
                            {
                                return "Error: La cotización ya ha sido CANCELADA, cualquier inquietud no dude en comunicarse con nosotros.";
                            }
                            else if (request.Status.Id == 5)
                            {
                                return "Error: La cotización ya ha sido ACEPTADA, cualquier inquietud no dude en comunicarse con nosotros.";
                            }
                        }

                        QuotationStatus status = await _context.QuotationStatus.FirstOrDefaultAsync(x => x.Id == 5);

                        if (status != null)
                        {
                            request.Status = status;
                            request.ModificationDatetime = DateTime.Now;
                            request.HasBeenGenerated = true;

                            _context.Update(request);
                            await _context.SaveChangesAsync();

                            return "Ok";
                        }
                        else
                        {
                            return "Error: El estado 'Confirmado' no ha sido encontrado.";
                        }
                    }
                    catch (Exception ex)
                    {
                        return string.Format("Error: {0}", ex.ToString());
                    }
                }

                return "Error: No hay información de la cotización.";
            }
            catch(Exception ex)
            {
                return string.Format("Error: {0}", ex.ToString());
            }
            
        }

        public async Task<IActionResult> QuotationCancelConfirmation(int? id)
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

            return PartialView(quotationRequest);
        }

        public async Task<string> RejectQuotation(int id, string response)
        {
            try
            {
                QuotationRequest request = await _context.QuotationRequest.FirstOrDefaultAsync(x => x.Id == id);

                if (request != null)
                {
                    try
                    {
                        if (request.Status != null)
                        {
                            if (request.Status.Id == 4)
                            {
                                return "Error: La cotización ya ha sido CANCELADA, cualquier inquietud no dude en comunicarse con nosotros.";
                            }
                            else if (request.Status.Id == 5)
                            {
                                return "Error: La cotización ya ha sido ACEPTADA, cualquier inquietud no dude en comunicarse con nosotros.";
                            }
                        }

                        QuotationStatus status = await _context.QuotationStatus.FirstOrDefaultAsync(x => x.Id == 4);

                        if (status != null)
                        {
                            request.Status = status;
                            request.ModificationDatetime = DateTime.Now;
                            request.CancelationResponse = response;

                            _context.Update(request);
                            await _context.SaveChangesAsync();

                            return "Ok";
                        }
                        else
                        {
                            return "Error: El estado 'Cancelado' no ha sido encontrado.";
                        }
                    }
                    catch (Exception ex)
                    {
                        return string.Format("Error: {0}", ex.ToString());
                    }
                }

                return "Error: No hay información de la cotización.";
            }
            catch (Exception ex)
            {
                return string.Format("Error: {0}", ex.ToString());
            }

        }
    }
}
