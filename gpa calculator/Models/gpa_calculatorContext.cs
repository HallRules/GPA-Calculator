using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace gpa_calculator.Models
{
    public partial class gpa_calculatorContext : DbContext
    {
        public gpa_calculatorContext()
        {
        }

        public gpa_calculatorContext(DbContextOptions<gpa_calculatorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<StudentGrades> StudentGrades { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           if (!optionsBuilder.IsConfigured)
          {
               optionsBuilder.UseSqlServer("Server=HALLRULES\\SQLEXPRESS;Database=gpa_calculator;User Id=sa;Password=G-rock911;");
           //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-53bc9b9d-9d6a-45d4-8429-2a2761773502;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentGrades>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClassName).HasMaxLength(50);

                entity.Property(e => e.ClassType).HasMaxLength(50);

                entity.Property(e => e.Grade)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.StudentId)
                    .HasColumnName("StudentID")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
