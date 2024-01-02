using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gpa_calculator;
using gpa_calculator.Data;
using Microsoft.AspNetCore.Authorization;

namespace gpa_calculator.Controllers
{
    [Authorize]
    public class StudentGradesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentGradesController(ApplicationDbContext context)
        {
            _context = context;
        }

        private Dictionary<string, double> gradeValues =  new() {
            {"A+", 4.0}, {"A", 4.0}, {"A-", 3.7},
            {"B+", 3.3}, {"B", 3.0}, {"B-", 2.7},
            {"C+", 2.3}, {"C", 2.0}, {"C-", 1.7},
            {"D+", 1.3}, {"D", 1.0}, {"D-", 0.7},
            {"F+", 0.3}, {"F", 0.0}, {"F-", 0.0}
        };
        
        private Dictionary<string, double> classTypes = new() {
            {"Regular", 0},
            {"Honors", 0.5},
            {"AP", 1.0},
            {"DE", 1.0}
        };

        // GET: StudentGrades
        public async Task<IActionResult> Index()
        {
            var table = await _context.StudentGrades.ToListAsync();
            var studentGrades = table.Where(s => s.StudentID == User.Identity.Name);

            var quarterTables = new List<IEnumerable<gpa_calculator.StudentGrades>> {
                studentGrades.Where(s => s.Quarter == 1),
                studentGrades.Where(s => s.Quarter == 2),
                studentGrades.Where(s => s.Quarter == 3),
                studentGrades.Where(s => s.Quarter == 4)
            };

            double totalWeightedGPA = 0;
            double totalUnweightedGPA = 0;
            int allClasses = 0;

            double[] gpas = new double[8];
            for (int i = 0; i < 4; i++)
            {
                double weightedGPA = 0;
                double unweightedGPA = 0;
                int totalClasses = 0;

                foreach (var classItem in quarterTables[i]){
                    var grade = classItem.Grade.Trim();
                    var classType = classItem.ClassType.Trim();
                    if (gradeValues.ContainsKey(grade) && classTypes.ContainsKey(classType))
                    {
                        unweightedGPA += gradeValues[grade];
                        weightedGPA += gradeValues[grade] + classTypes[classType];
                        totalUnweightedGPA += gradeValues[grade];
                        totalWeightedGPA += gradeValues[grade] + classTypes[classType];
                        totalClasses += 1;
                        allClasses += 1;
                    }
                }

                if (totalClasses > 0)
                {
                    weightedGPA /= totalClasses;
                    unweightedGPA /= totalClasses;
                }

                gpas[i] = weightedGPA;
                gpas[i + 4] = unweightedGPA;
            }

            ViewBag.GPAs = gpas;
            ViewBag.QuarterTables = quarterTables;

            return View(studentGrades);
        }


        // GET: StudentGrades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentGrades = await _context.StudentGrades
                .FirstOrDefaultAsync(m => m.ID == id);
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
        public async Task<IActionResult> Create([Bind("ID,Quarter,ClassName,ClassType,Grade,StudentID")] StudentGrades studentGrades)
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
        public async Task<IActionResult> Edit(int id, [Bind("ID,Quarter,ClassName,ClassType,Grade,StudentID")] StudentGrades studentGrades)
        {
            if (id != studentGrades.ID)
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
                    if (!StudentGradesExists(studentGrades.ID))
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
                .FirstOrDefaultAsync(m => m.ID == id);
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
            return _context.StudentGrades.Any(e => e.ID == id);
        }
    }
}
