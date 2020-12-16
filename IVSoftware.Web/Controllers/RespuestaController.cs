using IVSoftware.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.Controllers
{
    public class RespuestaController : Controller
    {
        private readonly IVSoftwareContext _context;

        public RespuestaController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: Respuesta
        public async Task<IActionResult> Index()
        {
            var iVSoftwareContext = _context.Respuesta.Include(r => r.Pregunta);
            return View(await iVSoftwareContext.ToListAsync());
        }

        // GET: Respuesta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var respuesta = await _context.Respuesta
                .Include(r => r.Pregunta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (respuesta == null)
            {
                return NotFound();
            }

            return View(respuesta);
        }

        // GET: Respuesta/Create
        public IActionResult Create(int PreguntaId)
        {
            ViewData["PreguntaId"] = PreguntaId;
            return View();
        }

        // POST: Respuesta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Texto,RespuestaCorrecta,Orden,PreguntaId")] Respuesta respuesta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(respuesta);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Pregunta", new { Id = respuesta.PreguntaId });
            }
            ViewData["PreguntaId"] = new SelectList(_context.Pregunta, "Id", "Id", respuesta.PreguntaId);
            return View(respuesta);
        }

        // GET: Respuesta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var respuesta = await _context.Respuesta.FindAsync(id);
            if (respuesta == null)
            {
                return NotFound();
            }
            ViewData["PreguntaId"] = id;
            return View(respuesta);
        }

        // POST: Respuesta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Texto,RespuestaCorrecta,Orden,PreguntaId")] Respuesta respuesta)
        {
            if (id != respuesta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(respuesta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RespuestaExists(respuesta.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Pregunta", new { Id = respuesta.PreguntaId });
            }
            ViewData["PreguntaId"] = new SelectList(_context.Pregunta, "Id", "Id", respuesta.PreguntaId);
            return View(respuesta);
        }

        // GET: Respuesta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var respuesta = await _context.Respuesta
                .Include(r => r.Pregunta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (respuesta == null)
            {
                return NotFound();
            }

            return View(respuesta);
        }

        // POST: Respuesta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var respuesta = await _context.Respuesta.FindAsync(id);
            _context.Respuesta.Remove(respuesta);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Pregunta", new { Id = respuesta.PreguntaId });
        }

        private bool RespuestaExists(int id)
        {
            return _context.Respuesta.Any(e => e.Id == id);
        }
    }
}
