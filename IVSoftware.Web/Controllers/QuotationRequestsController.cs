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

        public async Task<string> GetClientInfo (string id)
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
                quotationRequest.Client = await _context.ClientModel.FirstOrDefaultAsync<ClientModel>(x=> x.Identification != null && x.Identification.Equals(quotationRequest.RequestedClientId));
                quotationRequest.Status = await _context.QuotationStatus.FirstOrDefaultAsync<QuotationStatus>(x => x.Id == 1);

                _context.Add(quotationRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quotationRequest);
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
        public async Task<IActionResult> Edit(int? id)
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
