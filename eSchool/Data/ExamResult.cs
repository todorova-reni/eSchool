using eSchool.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace eSchool.Data
{
    public class ExamResult
    {
        public int Id { get; set; }

        [Required]
        public int Course_Id { get; set; }

        [Required]
        public int Student_Id { get; set; }

        [Required]
        public int Grade { get; set; }

        [Required]
        public DateTime Created_At { get; set; }

        public string Created_By { get; set; }

        public DateTime Updated_At { get; set; }

        public string Updated_By { get; set; }

        //An examResult record is for a single course (and a single student),
        //so there's a Course_Id (and Student_Id) foreign key property and a Course (Studnet) navigation property:
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
