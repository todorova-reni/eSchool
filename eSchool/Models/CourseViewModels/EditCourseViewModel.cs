using System.ComponentModel.DataAnnotations;

namespace eSchool.Models.CourseViewModels
{
    public class EditCourseViewModel
    {

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        public int Active { get; set; }
    }
}
