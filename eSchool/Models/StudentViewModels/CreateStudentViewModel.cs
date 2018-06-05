using System.ComponentModel.DataAnnotations;

namespace eSchool.Models.StudentViewModels
{
    public class CreateStudentViewModel
    {
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
    }
}
