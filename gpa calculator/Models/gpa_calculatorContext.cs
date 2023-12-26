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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=HALLRULES\\SQLEXPRESS;Database=gpa_calculator;Integrated Security=True");
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
