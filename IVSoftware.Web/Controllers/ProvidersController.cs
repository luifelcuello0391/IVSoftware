using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IVSoftware.Web.Models;
using IVSoftware.Data.Models;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;
using IVSoftware.Web.Service;

namespace IVSoftware.Web.Controllers
{
    public class ProvidersController : Controller
    {
        private readonly IVSoftwareContext _context;
        private readonly IHostingEnvironment _env;
        private readonly IEntityService<Document, Guid> _documentService;

        public ProvidersController(IVSoftwareContext context, IHostingEnvironment env, IEntityService<Document, Guid> documentService)
        {
            _documentService = documentService;
            _env = env;
            _context = context;
        }

        // GET: Providers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Provider.ToListAsync());
        }

        // GET: Providers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provider = await _context.Provider
                .FirstOrDefaultAsync(m => m.Id == id);
            if (provider == null)
            {
                return NotFound();
            }

            if(provider.RutDocument != null)
            {
                provider.DocumentFile = provider.RutDocument.Url;
            }

            return View(provider);
        }

        // GET: Providers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Providers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Address,PhoneNumber,Email,Contact,Name,RegisterStatus,CreationDatetime,ModificationDatetime,Identification,Rut, WebPage")] Provider provider, List<IFormFile> postedFiles)
        {
            if (ModelState.IsValid)
            {
                Document rut = null;

                if(postedFiles != null && postedFiles.Count > 0)
                {
                    rut = new Document();
                    rut.Id = Guid.NewGuid();
                    rut.Name = postedFiles[0].FileName;
                    rut.CreationDatetime = DateTime.Now;
                    rut.ModiicationDatetime = DateTime.Now;
                    rut.Url = "/AppDocuments/Providers/" + postedFiles[0].FileName;
                }

                provider.RutDocument = rut;

                if(provider.RutDocument != null)
                {
                    if(postedFiles[0].FileName.ToLower().EndsWith(".pdf"))
                    {
                        if(postedFiles[0].Length <= 2000000)
                        {
                            string path = Path.Combine(_env.WebRootPath, "AppDocuments", "Providers");

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
                                    //ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
                                }
                            }

                            // Saves the RUT document information
                            _context.Add(rut);
                            await _context.SaveChangesAsync();

                            // Saves the provider information
                            _context.Add(provider);
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
                else // No document attached
                {
                    // Saves only the provider information
                    _context.Add(provider);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(provider);
        }

        // GET: Providers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provider = await _context.Provider.FindAsync(id);
            if (provider == null)
            {
                return NotFound();
            }

            if(provider.RutDocument != null)
            {
                provider.RutDocuentId = provider.RutDocument.Id;
            }

            return View(provider);
        }

        // POST: Providers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Address,PhoneNumber,Email,Contact,Name,RegisterStatus,CreationDatetime,ModificationDatetime,Identification,Rut,WebPage,RutDocuentId")] Provider provider, List<IFormFile> postedFiles)
        {
            if (id != provider.Id)
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
                                if(provider.RutDocuentId != null)
                                {
                                    IEnumerable<Document> documents = await _documentService.FindByConditionAsync(x => x.Id == provider.RutDocuentId);
                                    if(documents != null && documents.Count() > 0)
                                    {
                                        provider.RutDocument = documents.FirstOrDefault();
                                    }
                                }

                                if (provider.RutDocument != null)
                                {
                                    string previousFileName = provider.RutDocument.Name;

                                    provider.RutDocument.Name = postedFiles[0].FileName;
                                    provider.RutDocument.ModiicationDatetime = DateTime.Now;
                                    provider.RutDocument.Url = "/AppDocuments/Providers/" + postedFiles[0].FileName;

                                    _context.Update(provider.RutDocument);
                                    await _context.SaveChangesAsync();

                                    string deletePath = Path.Combine(_env.WebRootPath, "AppDocuments", "Providers", previousFileName);

                                    // Delete file on physical path
                                    if(System.IO.File.Exists(deletePath))
                                    {
                                        System.IO.File.Delete(deletePath);
                                    }
                                }
                                else
                                {
                                    Document rut = new Document();
                                    rut.Id = Guid.NewGuid();
                                    rut.Name = postedFiles[0].FileName;
                                    rut.CreationDatetime = DateTime.Now;
                                    rut.ModiicationDatetime = DateTime.Now;
                                    rut.Url = "/AppDocuments/Providers/" + postedFiles[0].FileName;
                                    provider.RutDocument = rut;

                                    _context.Add(rut);
                                    await _context.SaveChangesAsync();
                                }

                                // Add file to physical path
                                string path = Path.Combine(_env.WebRootPath, "AppDocuments", "Providers");

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
                                return View(provider);
                            }
                        }
                        else
                        {
                            ViewBag.Message = "El archivo debe ser un PDF";
                            return View(provider);
                        }
                    }

                    _context.Update(provider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProviderExists(provider.Id))
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
            return View(provider);
        }

        // GET: Providers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provider = await _context.Provider
                .FirstOrDefaultAsync(m => m.Id == id);
            if (provider == null)
            {
                return NotFound();
            }

            if (provider.RutDocument != null)
            {
                provider.DocumentFile = provider.RutDocument.Url;
            }

            return View(provider);
        }

        // POST: Providers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var provider = await _context.Provider.FindAsync(id);
            _context.Provider.Remove(provider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProviderExists(int id)
        {
            return _context.Provider.Any(e => e.Id == id);
        }


        public ActionResult DownloadFile(string fileName)
        {
            try
            {
                string[] paths = fileName.Split("/");

                string path = Path.Combine(_env.WebRootPath, "AppDocuments", "Providers", paths[paths.Length - 1]);

                if(System.IO.File.Exists(path))
                {
                    var fs = System.IO.File.OpenRead(path);
                    return File(fs, "application/pdf", Path.GetFileName(paths[paths.Length - 1]));
                }
                else
                {
                    return NotFound();
                }
            }
            catch(FileNotFoundException)
            {
                return NotFound();
            }
        }
    }

}

