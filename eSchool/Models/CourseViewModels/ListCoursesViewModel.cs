using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchool.Models.CourseViewModels
{
    public class ListCoursesViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public int Active { get; set; }
    }
}
