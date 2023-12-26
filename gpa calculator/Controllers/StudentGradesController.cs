using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gpa_calculator.Models;

namespace gpa_calculator.Controllers
{
    public class StudentGradesController : Controller
    {
        private readonly gpa_calculatorContext _context;

        public StudentGradesController(gpa_calculatorContext context)
        {
            _context = context;
        }

        //public void ConfigureServices(IServiceCollection services)
        //{
        //    var connection = "Server=HALLRULES\\SQLEXPRESS;Database=gpa_calculator;User Id=sa;Password=G-rock911;";
        //    services.AddDbContext<gpa_calculatorContext>(options => options.UseSqlServer(connection));
        //    services.AddControllersWithViews();
        //}

        // GET: StudentGrades
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudentGrades.ToListAsync());
        }

        // GET: StudentGrades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentGrades = await _context.StudentGrades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentGrades == null)
            {
                return NotFound();
            }

            return View(studentGrades);
        }

        // GET: StudentGrades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentGrades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Revision,ClassName,ClassType,Grade,StudentId")] StudentGrades studentGrades)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentGrades);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentGrades);
        }

        // GET: StudentGrades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentGrades = await _context.StudentGrades.FindAsync(id);
            if (studentGrades == null)
            {
                return NotFound();
            }
            return View(studentGrades);
        }

        // POST: StudentGrades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Revision,ClassName,ClassType,Grade,StudentId")] StudentGrades studentGrades)
        {
            if (id != studentGrades.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentGrades);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentGradesExists(studentGrades.Id))
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
            return View(studentGrades);
        }

        // GET: StudentGrades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentGrades = await _context.StudentGrades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentGrades == null)
            {
                return NotFound();
            }

            return View(studentGrades);
        }

        // POST: StudentGrades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentGrades = await _context.StudentGrades.FindAsync(id);
            if (studentGrades != null)
            {
                _context.StudentGrades.Remove(studentGrades);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentGradesExists(int id)
        {
            return _context.StudentGrades.Any(e => e.Id == id);
        }
    }
}
