using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchool.Models.CourseViewModels
{
    public class ViewCourseViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Active { get; set; }

        public DateTime Created_At { get; set; }

        public string Created_by { get; set; }

        public DateTime Updated_At { get; set; }

        public string Updated_By { get; set; }
    }
}
