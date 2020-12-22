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
        public async Task<IActionResult> Index()
        {
            return View(await _context.QuotationRequest.ToListAsync());
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
            return View();
        }

        public async Task<string> GetClientInfo(string id)
        {
            if (id != null && !string.IsNullOrEmpty(id.Replace(" ", string.Empty)))
            {
                ClientModel client = null;
                try
                {
                    client = await _context.ClientModel.FirstOrDefaultAsync<ClientModel>(x => x.Identification != null && x.Identification.Equals(id));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error GetClientInfo >> " + ex.ToString());
                }

                return client != null ? client.Name : string.Empty;
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return string.Empty;
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
    }
}
