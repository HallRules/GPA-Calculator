using gpa_calculator.Models;
using Microsoft.EntityFrameworkCore;

namespace gpa_calculator.Data
{
    public class gpa_calculatorContext : DbContext
    {
        public gpa_calculatorContext(DbContextOptions<gpa_calculatorContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GPAContext>().ToTable("StudentGrades");
        }

        public DbSet<GPAContext> GPAContext { get; set; } = default!;
    }
}
