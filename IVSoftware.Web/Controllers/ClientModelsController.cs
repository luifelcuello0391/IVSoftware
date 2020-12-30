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
    public class ClientModelsController : Controller
    {
        private readonly IEntityService<IdentificationType, int> _IdentificationTypeService;
        private readonly IEntityService<Department, int> _DepartmentService;
        private readonly IEntityService<Municipality, int> _MunicipalityService;
        private readonly IVSoftwareContext _context;

        public ClientModelsController(IVSoftwareContext context, 
            IEntityService<IdentificationType, int> identificationService,
            IEntityService<Department, int> departmentService,
            IEntityService<Municipality, int> municipalityService)
        {
            _IdentificationTypeService = identificationService;
            _DepartmentService = departmentService;
            _MunicipalityService = municipalityService;
            _context = context;
        }

        // GET: ClientModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.ClientModel.ToListAsync());
        }

        // GET: ClientModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientModel = await _context.ClientModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientModel == null)
            {
                return NotFound();
            }

            return View(clientModel);
        }

        [HttpPost]
        public string GetCityByStateId(int stateid)
        {
            List<Municipio> objcity = new List<Municipio>();
            objcity = _context.Municipio.Where(m => m.Departamento != null && m.Departamento.Id == stateid).ToList();
            string result = string.Empty;

            if(objcity != null && objcity.Count > 0)
            {
                foreach(Municipio city in objcity)
                {
                    if(city != null)
                    {
                        result += string.Format("{0};{1}|", city.Id, city.Nombre);
                    }
                }
            }

            return result;
        }

        [HttpPost]
        public async Task<string> AddContact (int docTypeId, string id, string name, 
            int departmentId, int cityId, string address, string phone, 
            string extension, string cellphone, string jobDepartment, 
            string job, string email, int clientId)
        {
            try
            {
                if(clientId > 0)
                {
                    ClientModel client = await _context.ClientModel.FirstOrDefaultAsync(x => x.Id == clientId);

                    if(client != null)
                    {
                        if(client.Contacts == null)
                        {
                            client.Contacts = new List<ClientContact>();
                        }

                        if (client.Contacts.FirstOrDefault(x => x.Identification != null && x.Identification.Equals(id)) == null)
                        {
                            // The contact has not been assigned
                            ClientContact contact = new ClientContact();
                            contact.DocumentType = await _IdentificationTypeService.GetByIdAsync(docTypeId);
                            contact.Identification = id;
                            contact.Name = name;
                            contact.AddressDepartment = await _DepartmentService.GetByIdAsync(departmentId);
                            contact.City = await _MunicipalityService.GetByIdAsync(cityId);
                            contact.Address = address;
                            contact.PhoneNumber = phone;
                            contact.Extension = extension;
                            contact.CellPhone = cellphone;
                            contact.Department = jobDepartment;
                            contact.Position = job;
                            contact.ReportDeliveryEmail = email;
                            contact.RegisterStatus = 1;
                            contact.CreationDatetime = DateTime.Now;
                            contact.ModificationDatetime = DateTime.Now;

                            client.Contacts.Add(contact);

                            try
                            {
                                _context.Update(client);
                                await _context.SaveChangesAsync();
                            }
                            catch(DbUpdateConcurrencyException ex)
                            {
                                if (!ClientModelExists(client.Id))
                                {
                                    return "Error: Cliente no encontrado >> " + ex.ToString();
                                }
                                else
                                {
                                    throw;
                                }
                            }
                        }
                        else
                        {
                            // The contact is already assigned
                            return "Error: Ya existe un contacto asignado con la identificación '" + id + "'.";
                        }
                    }
                    else
                    {
                        return "Error: Cliente no encontrado.";
                    }
                }
                else
                {
                    return "Error: Cliente inválido.";
                }
            }
            catch(Exception ex)
            {
                return string.Format("Error: " + ex.ToString());
            }

            return "Ok:";
        }

        public async Task<IActionResult> RemoveContact(int clientId, int contactId)
        {
            if(clientId > 0)
            {
                if(contactId > 0)
                {
                    ClientModel client = await _context.ClientModel.FirstOrDefaultAsync(x => x.Id == clientId);
                    if(client != null)
                    {
                        try
                        {
                            if(client.Contacts != null && client.Contacts.Count > 0)
                            {
                                ClientContact contact = client.Contacts.FirstOrDefault(x => x.Id == contactId);

                                if (contact != null)
                                {
                                    client.Contacts.Remove(contact);
                                }
                            }

                            try
                            {
                                _context.Update(client);
                                await _context.SaveChangesAsync();
                            }
                            catch (DbUpdateConcurrencyException ex)
                            {
                                if (!ClientModelExists(client.Id))
                                {
                                    return NotFound();
                                }
                                else
                                {
                                    throw;
                                }
                            }

                            return RedirectToAction(nameof(Edit), new { id = clientId });
                        }
                        catch(DbUpdateConcurrencyException)
                        {
                            if (!ClientModelExists(client.Id))
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
                else
                {
                    return RedirectToAction(nameof(Edit), new { id = clientId });
                }
            }
            else
            {
                return NotFound();
            }
        }

        // GET: ClientModels/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                ViewBag.ClientTypes = await _context.ClientTypeModel.ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error on Create.ClientTypes >> " + ex.ToString());
            }

            try
            {
                ViewBag.DocTypes = await _context.TipoDocumento.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on Create.DocTypes >> " + ex.ToString());
            }

            try
            {
                ViewBag.Departments = await _context.Departamento.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on Create.Departments >> " + ex.ToString());
            }

            //try
            //{
            //    ViewBag.Cities = await _context.Municipio.ToListAsync();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error on Create.Cities >> " + ex.ToString());
            //}

            return View();
        }

        // POST: ClientModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DocumentType,Identification,Address,PhoneNumber,ContactName,EmailAddress,IsRegular,LastServiceExecutionDatetime,Name,RegisterStatus,CreationDatetime,ModificationDatetime,SelectedDocumentTypeId,SelectedClientTypeId,PersonType,Extension,DepartmentId,CityId,CellPhone")] ClientModel clientModel)
        {
            if (ModelState.IsValid)
            {
                ClientTypeModel type = null;
                if (clientModel.SelectedClientTypeId > 0)
                {
                    type = await _context.ClientTypeModel.FindAsync(clientModel.SelectedClientTypeId);
                }
                clientModel.ClientType = type;

                IdentificationType docType = null;
                if(clientModel.SelectedDocumentTypeId > 0)
                {
                    docType = await _IdentificationTypeService.GetByIdAsync(clientModel.SelectedDocumentTypeId);
                }
                clientModel.DocumentType = docType;

                _context.Add(clientModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clientModel);
        }

        // GET: ClientModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientModel = await _context.ClientModel.FindAsync(id);
            if (clientModel == null)
            {
                return NotFound();
            }

            try
            {
                ViewBag.ClientTypes = await _context.ClientTypeModel.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on Create.ClientTypes >> " + ex.ToString());
            }

            try
            {
                ViewBag.DocTypes = await _context.TipoDocumento.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on Create.DocTypes >> " + ex.ToString());
            }

            try
            {
                ViewBag.Departments = await _context.Departamento.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on Create.Departments >> " + ex.ToString());
            }

            if (clientModel.ClientType != null)
            {
                clientModel.SelectedClientTypeId = clientModel.ClientType.Id;
            }

            if(clientModel.DocumentType != null)
            {
                clientModel.SelectedDocumentTypeId = clientModel.DocumentType.Id;
            }

            return View(clientModel);
        }

        // POST: ClientModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DocumentType,Identification,Address,PhoneNumber,ContactName,EmailAddress,IsRegular,LastServiceExecutionDatetime,Name,RegisterStatus,CreationDatetime,ModificationDatetime,SelectedDocumentTypeId,SelectedClientTypeId,PersonType,Extension,DepartmentId,CityId,CellPhone")] ClientModel clientModel)
        {
            if (id != clientModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Obtains the client type instance
                    ClientTypeModel type = null;
                    if (clientModel.SelectedClientTypeId > 0)
                    {
                        type = await _context.ClientTypeModel.FindAsync(clientModel.SelectedClientTypeId);
                    }
                    clientModel.ClientType = type;

                    // Obtains the document type instance
                    IdentificationType docType = null;
                    if (clientModel.SelectedDocumentTypeId > 0)
                    {
                        docType = await _IdentificationTypeService.GetByIdAsync(clientModel.SelectedDocumentTypeId);
                    }
                    clientModel.DocumentType = docType;

                    // Obtains the department instance
                    Department dep = null;
                    if(clientModel.DepartmentId != null && clientModel.DepartmentId.Value > 0)
                    {
                        dep = await _DepartmentService.GetByIdAsync(clientModel.DepartmentId.Value);
                    }
                    clientModel.Department = dep;

                    // Obtains the city instance
                    Municipality city = null;
                    if(clientModel.CityId != null && clientModel.CityId.Value > 0)
                    {
                        city = await _MunicipalityService.GetByIdAsync(clientModel.CityId.Value);
                    }
                    clientModel.City = city;

                    _context.Update(clientModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientModelExists(clientModel.Id))
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
            return View(clientModel);
        }

        // GET: ClientModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientModel = await _context.ClientModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientModel == null)
            {
                return NotFound();
            }

            return View(clientModel);
        }

        // POST: ClientModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientModel = await _context.ClientModel.FindAsync(id);
            _context.ClientModel.Remove(clientModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientModelExists(int id)
        {
            return _context.ClientModel.Any(e => e.Id == id);
        }
    }
}
