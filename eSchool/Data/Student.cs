
using eSchool.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eSchool.Data
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Stud_Email { get; set; }

        [Required]
        [EmailAddress]
        public string Parent_Email { get; set; }

        [Required]
        public int Class_Id { get; set; }


        public int Active { get; set; }

        [Required]
        public DateTime Created_At { get; set; }

        public string Created_By { get; set; }

        public DateTime Updated_At { get; set; }

        public string Updated_By { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<ExamResult> ExamResult { get; set; }

    }
}
