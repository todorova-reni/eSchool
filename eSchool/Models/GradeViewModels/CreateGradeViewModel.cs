using System.ComponentModel.DataAnnotations;

namespace eSchool.Models.GradeViewModels
{
    public class CreateGradeViewModel
    {
        [Required]
        public int Grade_number { get; set; }

        [Required]
        public string Grade_letter { get; set; }

        [Required]
        public string Teacher_Id { get; set; }

        public int Active { get; set; }

    }
}
