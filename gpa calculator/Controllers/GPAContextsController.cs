using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gpa_calculator.Data;
using gpa_calculator.Models;

namespace gpa_calculator.Controllers
{
    [ApiController]
    [Route("gpacontext")]
    public class GPAContextsController : Controller
    {
        private readonly gpa_calculatorContext _context;

        public GPAContextsController(gpa_calculatorContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GPAContext>>> Get()
        {
            return await _context.GPAContext.ToListAsync();
        }


        // GET: GPAContexts
        public async Task<IActionResult> Index()
        {
            return View(await _context.GPAContext.ToListAsync());
        }

        // GET: GPAContexts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gPAContext = await _context.GPAContext
                .FirstOrDefaultAsync(m => m.ID == id);
            if (gPAContext == null)
            {
                return NotFound();
            }

            return View(gPAContext);
        }

        // GET: GPAContexts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GPAContexts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ClassName,Grade,StudentID")] GPAContext gPAContext)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gPAContext);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gPAContext);
        }

        // GET: GPAContexts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gPAContext = await _context.GPAContext.FindAsync(id);
            if (gPAContext == null)
            {
                return NotFound();
            }
            return View(gPAContext);
        }

        // POST: GPAContexts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ClassName,Grade,StudentID")] GPAContext gPAContext)
        {
            if (id != gPAContext.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gPAContext);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GPAContextExists(gPAContext.ID))
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
            return View(gPAContext);
        }

        // GET: GPAContexts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gPAContext = await _context.GPAContext
                .FirstOrDefaultAsync(m => m.ID == id);
            if (gPAContext == null)
            {
                return NotFound();
            }

            return View(gPAContext);
        }

        // POST: GPAContexts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gPAContext = await _context.GPAContext.FindAsync(id);
            if (gPAContext != null)
            {
                _context.GPAContext.Remove(gPAContext);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GPAContextExists(int id)
        {
            return _context.GPAContext.Any(e => e.ID == id);
        }
    }
}
