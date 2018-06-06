using System.ComponentModel.DataAnnotations;

namespace eSchool.Models.GradeViewModels
{
    public class EditGradeViewModel
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
