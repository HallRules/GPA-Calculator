using System;
using System.Collections.Generic;

namespace gpa_calculator.Models
{
    public partial class StudentGrades
    {
        public int Id { get; set; }
        public int Revision { get; set; }
        public string ClassName { get; set; }
        public string ClassType { get; set; }
        public string Grade { get; set; }
        public string StudentId { get; set; }
    }
}
