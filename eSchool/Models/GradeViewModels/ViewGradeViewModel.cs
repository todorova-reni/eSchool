using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchool.Models.GradeViewModels
{
    public class ViewGradeViewModel
    {
        public int Grade_number { get; set; }

        public string Grade_letter { get; set; }

        public string Teacher_Id { get; set; }

        public int Active { get; set; }

        public DateTime Created_At { get; set; }

        public string Created_by { get; set; }

        public DateTime Updated_At { get; set; }

        public string Updated_By { get; set; }
    }
}
