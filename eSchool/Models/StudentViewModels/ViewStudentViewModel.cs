using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchool.Models.StudentViewModels
{
    public class ViewStudentViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Stud_Email { get; set; }

        public string Parent_Email { get; set; }

        public int Class_id { get; set; }

        public int Active { get; set; }

        public DateTime Created_At { get; set; }

        public string Created_by { get; set; }

        public DateTime Updated_At { get; set; }

        public string Updated_By { get; set; }
        public object ExamResult { get; internal set; }
    }
}
