using System.ComponentModel.DataAnnotations;

namespace eSchool.Models.ExamResultViewModel
{
    public class AddExamResultViewModel
    {
        [Required]
        public int Course_Id { get; set; }

        [Required]
        public int Student_Id { get; set; }

        [Required]
        public int Grade { get; set; }

    }
}
