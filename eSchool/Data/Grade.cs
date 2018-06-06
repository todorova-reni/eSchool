using eSchool.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eSchool.Data
{
    public class Grade
    {
        public int Id { get; set; }

        [Required]
        public int Grade_number { get; set; }

        [Required]
        public string Grade_letter { get; set; }

        [Required]
        public string Teacher_Id { get; set; }

        public int Active { get; set; }

        [Required]
        public DateTime Created_At { get; set; }

        public string Created_By { get; set; }

        public DateTime Updated_At { get; set; }

        public string Updated_By { get; set; }

        public virtual ApplicationUser User { get; set; }

        //An course can have any number of exam results, so ExamResult is defined as a collection of ExamResult entities.
        public virtual ICollection<ExamResult> ExamResult { get; set; }
    }
}
