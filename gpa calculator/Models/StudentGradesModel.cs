﻿using System;
using System.ComponentModel.DataAnnotations;

namespace gpa_calculator
{
    public class StudentGrades
    {
        #region Instance Properties

        [Display(Name = "ID")]
        [Required]
        public Int32 ID { get; set; }

        [Display(Name = "Quarter")]
        [Required]
        public Int32 Quarter { get; set; }

        [Display(Name = "Class Name")]
        [StringLength(50)]
        public String ClassName { get; set; }

        [Display(Name = "Class Type")]
        [StringLength(50)]
        public String ClassType { get; set; }

        [Display(Name = "Grade")]
        [StringLength(10)]
        public String Grade { get; set; }

        [Display(Name = "StudentID")]
        [StringLength(50)]
        public String StudentID { get; set; }

        #endregion Instance Properties
    }

}