using IVSoftware.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.Controllers
{
    public class PreguntaController : Controller
    {
        private readonly IVSoftwareContext _context;

        public PreguntaController(IVSoftwareContext context)
        {
            _context = context;
        }

        // GET: Pregunta
        public async Task<IActionResult> Index()
        {
            var iVSoftwareContext = _context.Pregunta
                .Include(p => p.Evaluacion);
            return View(await iVSoftwareContext.ToListAsync());
        }

        // GET: Pregunta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pregunta = await _context.Pregunta
                .Include(p => p.Evaluacion)
                .Include(p => p.ListRespuesta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pregunta == null)
            {
                return NotFound();
            }

            return View(pregunta);
        }

        // GET: Pregunta/Create
        public IActionResult Create(int EvaluacionId)
        {
            ViewData["EvaluacionId"] = EvaluacionId;
            return View();
        }

        // POST: Pregunta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipoPregunta,Enunciado,Orden,EvaluacionId")] Pregunta pregunta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pregunta);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Evaluacion", new { Id = pregunta.EvaluacionId } );
            }
            ViewData["EvaluacionId"] = new SelectList(_context.Evaluacion, "Id", "Id", pregunta.EvaluacionId);
            return View(pregunta);
        }

        // GET: Pregunta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pregunta = await _context.Pregunta.FindAsync(id);
            if (pregunta == null)
            {
                return NotFound();
            }
            ViewData["EvaluacionId"] = id;
            return View(pregunta);
        }

        // POST: Pregunta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipoPregunta,Enunciado,Orden,EvaluacionId")] Pregunta pregunta)
        {
            if (id != pregunta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pregunta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PreguntaExists(pregunta.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Evaluacion", new { Id = pregunta.EvaluacionId });
            }
            ViewData["EvaluacionId"] = new SelectList(_context.Evaluacion, "Id", "Id", pregunta.EvaluacionId);
            return View(pregunta);
        }

        // GET: Pregunta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pregunta = await _context.Pregunta
                .Include(p => p.Evaluacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pregunta == null)
            {
                return NotFound();
            }

            return View(pregunta);
        }

        // POST: Pregunta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pregunta = await _context.Pregunta.FindAsync(id);
            _context.Pregunta.Remove(pregunta);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Evaluacion", new { Id = pregunta.EvaluacionId });
        }

        private bool PreguntaExists(int id)
        {
            return _context.Pregunta.Any(e => e.Id == id);
        }
    }
}
