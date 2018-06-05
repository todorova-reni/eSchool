using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eSchool.Models.StudentViewModels
{
    public class EditStudentViewModel
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
        //public DateTime Updated_At { get; set; }

        //public string Updated_By { get; set; }
    }
}
